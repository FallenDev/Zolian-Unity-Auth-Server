using Darkages.Database;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System.Collections.Concurrent;
using System.Collections.Frozen;
using System.Data;
using System.Net;
using Darkages.Network.Server.Abstractions;
using Microsoft.Data.SqlClient;
using RestSharp;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Zolian.Common.Identity;
using Zolian.Networking.Options;

namespace Darkages.Network.Server;

public class ServerSetup : IServerContext
{
    public static ServerSetup Instance { get; private set; }
    private static ILogger<ServerSetup> _eventsLogger;
    private static Logger _packetLogger;
    public static IOptions<ServerOptions> ServerOptions;
    public readonly RestClient RestClient;
    public readonly RestClient RestReport;
    public bool Running { get; set; }
    public SqlConnection ServerSaveConnection { get; set; }
    public IServerConstants Config { get; set; }
    public WorldServer Game { get; set; }
    public LoginServer LoginServer { get; set; }
    public LobbyServer LobbyServer { get; set; }
    public string StoragePath { get; set; }
    public string MoonPhase { get; set; }
    public byte LightPhase { get; set; }
    public byte LightLevel { get; set; }
    public string KeyCode { get; set; }
    public string Unlock { get; set; }
    public IPAddress IpAddress { get; set; }
    public string[] GameMastersIPs { get; set; }
    public string InternalAddress { get; set; }

    // Templates
    public FrozenDictionary<uint, string> GlobalKnownGoodActorsCache { get; set; }
    public Dictionary<uint, string> TempGlobalKnownGoodActorsCache { get; set; } = [];
    
    // Live
    public ConcurrentDictionary<IPAddress, IPAddress> GlobalLobbyConnection { get; set; } = [];
    public ConcurrentDictionary<IPAddress, IPAddress> GlobalLoginConnection { get; set; } = [];
    public ConcurrentDictionary<IPAddress, IPAddress> GlobalWorldConnection { get; set; } = [];
    public ConcurrentDictionary<IPAddress, byte> GlobalCreationCount { get; set; } = [];
    public ConcurrentDictionary<IPAddress, byte> GlobalPasswordAttempt { get; set; } = [];

    public ServerSetup(IOptions<ServerOptions> options)
    {
        Instance = this;
        ServerOptions = options;
        var restSettings = SetupRestClients();
        RestClient = new RestClient(restSettings.Item1);
        RestReport = new RestClient(restSettings.Item2);
        BadActor.StartProcessingQueue();

        const string logTemplate = "[{Timestamp:MMM-dd HH:mm:ss} {Level:u3}] {Message}{NewLine}{Exception}";
        _packetLogger = new LoggerConfiguration()
            .WriteTo.File("_Zolian_packets_.txt", LogEventLevel.Verbose, logTemplate, rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    public static void ConnectionLogger(string logMessage, LogLevel logLevel = LogLevel.Information)
    {
        _eventsLogger?.Log(logLevel, "{logMessage}", logMessage);
    }

    public static void PacketLogger(string logMessage, LogLevel logLevel = LogLevel.Critical)
    {
        _packetLogger.Write(LogEventLevel.Error, logMessage);
    }

    public static void EventsLogger(string logMessage, LogLevel logLevel = LogLevel.Information)
    {
        _eventsLogger?.Log(logLevel, "{logMessage}", logMessage);
    }

    private static (RestClientOptions, RestClientOptions) SetupRestClients()
    {
        var optionsCheck = new RestClientOptions("https://api.abuseipdb.com/api/v2/check")
        {
            ThrowOnAnyError = true,
            Timeout = new TimeSpan(0, 0, 0, 5)
        };
        var optionsReport = new RestClientOptions("https://api.abuseipdb.com/api/v2/report")
        {
            ThrowOnAnyError = true,
            Timeout = new TimeSpan(0, 0, 0, 5)
        };

        return (optionsCheck, optionsReport);
    }

    public void InitFromConfig(string storagePath, string ipAddress)
    {
        IpAddress = IPAddress.Parse(ipAddress);
        StoragePath = storagePath;

        if (StoragePath != null && !Directory.Exists(StoragePath))
            Directory.CreateDirectory(StoragePath);
    }

    public void Start(IServerConstants config, ILogger<ServerSetup> logger)
    {
        Config = config;
        _eventsLogger = logger;
        DatabaseSaveConnection();
    }

    public void DatabaseSaveConnection()
    {
        ServerSaveConnection = new SqlConnection(AislingStorage.ConnectionString);
        ServerSaveConnection.Open();

        if (ServerSaveConnection.State == ConnectionState.Open)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Player Save-State Connected");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Issue connecting to database");
        }

        SetGoodActors();
    }

    public void SetGoodActors()
    {
        const string sql = "SELECT LastIP FROM ZolianPlayers.dbo.Players";
        var cmd = new SqlCommand(sql, ServerSaveConnection);
        cmd.CommandTimeout = 5;
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var iP = reader["LastIP"].ToString();
            TempGlobalKnownGoodActorsCache.TryAdd(EphemeralRandomIdGenerator<uint>.Shared.NextId, iP);
        }

        GlobalKnownGoodActorsCache = TempGlobalKnownGoodActorsCache.ToFrozenDictionary();
        TempGlobalKnownGoodActorsCache.Clear();
    }
}