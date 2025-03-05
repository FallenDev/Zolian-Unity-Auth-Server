using Darkages.Network.Client;
using Darkages.Network.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using JetBrains.Annotations;
using ServerOptions = Zolian.Networking.Options.ServerOptions;
using IWorldClient = Darkages.Network.Client.Abstractions.IWorldClient;
using Darkages.Network.Client.Abstractions;
using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets;
using Zolian.Packets.Abstractions;

namespace Darkages.Network.Server;

[UsedImplicitly]
public sealed class WorldServer : ServerBase<IWorldClient>, IWorldServer<IWorldClient>
{
    private readonly IClientFactory<WorldClient> _clientProvider;
    public ServerPacketLogger ServerPacketLogger { get; } = new();
    public ClientPacketLogger ClientPacketLogger { get; } = new();
    private static readonly string[] GameMastersIPs = ServerSetup.Instance.GameMastersIPs;
    private ConcurrentDictionary<Type, WorldServerComponent> _serverComponents;
    private const int GameSpeed = 50;
    
    public WorldServer(
        IClientRegistry<IWorldClient> clientRegistry,
        IClientFactory<WorldClient> clientProvider,
        IRedirectManager redirectManager,
        IPacketSerializer packetSerializer,
        ILogger<WorldServer> logger
    )
        : base(
            redirectManager,
            packetSerializer,
            clientRegistry,
            Microsoft.Extensions.Options.Options.Create(new ServerOptions
            {
                Address = ServerSetup.Instance.IpAddress,
                Port = ServerSetup.Instance.Config.SERVER_PORT
            }),
            logger)
    {
        ServerSetup.Instance.Game = this;
        _clientProvider = clientProvider;
        IndexHandlers();
        RegisterServerComponents();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Server is now Online\n");
    }

    #region Server Init

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            ServerSetup.Instance.Running = true;
            Task.Run(UpdateComponentsRoutine, stoppingToken);
        }
        catch (Exception ex)
        {
            ServerSetup.ConnectionLogger(ex.Message, LogLevel.Error);
            ServerSetup.ConnectionLogger(ex.StackTrace, LogLevel.Error);
            SentrySdk.CaptureException(ex);
        }

        return base.ExecuteAsync(stoppingToken);
    }

    private void RegisterServerComponents()
    {
        _serverComponents = new ConcurrentDictionary<Type, WorldServerComponent>
        {
            [typeof(PingComponent)] = new PingComponent(this),
            [typeof(ClientCreationLimit)] = new ClientCreationLimit(this)
        };

        Console.WriteLine();
        ServerSetup.ConnectionLogger($"Server Components Loaded: {_serverComponents.Count}");
    }

    #endregion

    #region Server Loop

    private void UpdateComponentsRoutine()
    {
        foreach (var component in _serverComponents.Values)
            Task.Run(component.Update);
    }
    
    #endregion

    #region OnHandlers
    
    public ValueTask OnClientRedirected(IWorldClient client, in Packet clientPacket)
    {
        var args = PacketSerializer.Deserialize<ClientRedirectedArgs>(in clientPacket);
        return ExecuteHandler(client, args, InnerOnClientRedirected);

        ValueTask InnerOnClientRedirected(IWorldClient localClient, ClientRedirectedArgs localArgs)
        {
            //if (localArgs.Message != null)
            //{
            //    // display welcome message to client.
            //}

            return default;
        }
    }

    #endregion

    #region Connection / Handler

    public override ValueTask HandlePacketAsync(IWorldClient client, in Packet packet)
    {
        var opCode = packet.OpCode;
        var handler = ClientHandlers[packet.OpCode];

        // ToDo: Packet logging
        //ServerSetup.PacketLogger($"{packet.OpCode}");

        try
        {
            if (handler is not null)
            {
                ClientPacketLogger.LogPacket(client.RemoteIp, $"{/*client.Aisling?.Username ?? */client.RemoteIp.ToString()} with Client OpCode: {opCode} ({Enum.GetName(typeof(ClientOpCode), opCode)})");
                return handler(client, in packet);
            }

            ServerSetup.PacketLogger("//////////////// Handled World Server Unknown Packet ////////////////", LogLevel.Error);
            ServerSetup.PacketLogger($"{opCode} from {client.RemoteIp}", LogLevel.Error);
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(new Exception($"Unknown packet {opCode} from {client.RemoteIp} on WorldServer \n {ex}"));
        }

        return default;
    }

    protected override void IndexHandlers()
    {
        base.IndexHandlers();

        ClientHandlers[(byte)ClientOpCode.ClientRedirected] = OnClientRedirected; // 0x10
    }

    protected override void OnConnected(Socket clientSocket)
    {
        ServerSetup.ConnectionLogger($"World connection from {clientSocket.RemoteEndPoint as IPEndPoint}");

        if (clientSocket.RemoteEndPoint is not IPEndPoint ip)
        {
            ServerSetup.ConnectionLogger("Socket not a valid endpoint");
            return;
        }

        try
        {
            var ipAddress = ip.Address;
            var client = _clientProvider.CreateClient(clientSocket);
            client.OnDisconnected += OnDisconnect;
            var safe = false;

            foreach (var _ in ServerSetup.Instance.GlobalKnownGoodActorsCache.Values.Where(savedIp => savedIp == ipAddress.ToString()))
                safe = true;

            if (!safe)
            {
                var badActor = BadActor.ClientOnBlackList(ipAddress.ToString());

                if (badActor)
                {
                    try
                    {
                        client.Disconnect();
                        ServerSetup.ConnectionLogger($"Disconnected Bad Actor from {ip}");
                    }
                    catch
                    {
                        // ignored
                    }

                    return;
                }
            }

            if (!ClientRegistry.TryAdd(client))
            {
                ServerSetup.ConnectionLogger("Two clients ended up with the same id - newest client disconnected");

                try
                {
                    client.Disconnect();
                }
                catch
                {
                    // ignored
                }

                return;
            }

            var lobbyCheck = ServerSetup.Instance.GlobalLobbyConnection.TryGetValue(ipAddress, out _);
            var loginCheck = ServerSetup.Instance.GlobalLoginConnection.TryGetValue(ipAddress, out _);

            if (!lobbyCheck || !loginCheck)
            {
                try
                {
                    client.Disconnect();
                }
                catch
                {
                    // ignored
                }

                ServerSetup.ConnectionLogger("---------World-Server---------");
                var comment = $"{ipAddress} has been blocked for violating security protocols through improper port access.";
                ServerSetup.ConnectionLogger(comment, LogLevel.Warning);
                BadActor.ReportMaliciousEndpoint(ipAddress.ToString(), comment);
                return;
            }

            ServerSetup.Instance.GlobalWorldConnection.TryAdd(ipAddress, ipAddress);
            client.BeginReceive();
        }
        catch (Exception e)
        {
            ServerSetup.ConnectionLogger($"Failed to authenticate worldServer using SSL/TLS.");
            SentrySdk.CaptureException(new Exception($"{ip.Address} - {e}"));
        }
    }

    private async void OnDisconnect(object sender, EventArgs e)
    {

    }

    private static bool IsManualAction(ClientOpCode opCode) => opCode switch
    {
        ClientOpCode.ClientRedirected => true,
        _ => false
    };

    #endregion
}