using Chaos.Networking.Abstractions;
using Chaos.Networking.Entities.Client;
using Chaos.Packets;
using Chaos.Packets.Abstractions;

using Darkages.Database;
using Darkages.Meta;
using Darkages.Network.Client;
using Darkages.Sprites;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using JetBrains.Annotations;
using ServerOptions = Chaos.Networking.Options.ServerOptions;
using ILoginClient = Darkages.Network.Client.Abstractions.ILoginClient;
using StringExtensions = ServiceStack.StringExtensions;
using Chaos.Networking.Abstractions.Definitions;
using Darkages.Enums;
using Darkages.Managers;
using Darkages.Network.Client.Abstractions;
using Darkages.Types;
using Gender = Chaos.DarkAges.Definitions.Gender;

namespace Darkages.Network.Server;

[UsedImplicitly]
public sealed partial class LoginServer : ServerBase<ILoginClient>, ILoginServer<ILoginClient>
{
    private readonly IClientFactory<LoginClient> _clientProvider;
    private readonly Notification _notification;
    private static readonly string[] GameMastersIPs = ServerSetup.Instance.GameMastersIPs;
    private ConcurrentDictionary<uint, CreateCharInitialArgs> CreateCharRequests { get; }

    public LoginServer(
        IClientRegistry<ILoginClient> clientRegistry,
        IClientFactory<LoginClient> clientProvider,
        IRedirectManager redirectManager,
        IPacketSerializer packetSerializer,
        ILogger<LoginServer> logger
    )
        : base(
            redirectManager,
            packetSerializer,
            clientRegistry,
            Microsoft.Extensions.Options.Options.Create(new ServerOptions
            {
                Address = ServerSetup.Instance.IpAddress,
                Port = ServerSetup.Instance.Config.LOGIN_PORT
            }),
            logger)
    {
        ServerSetup.Instance.LoginServer = this;
        _clientProvider = clientProvider;
        _notification = Notification.FromFile("Notification.txt");
        CreateCharRequests = [];
        IndexHandlers();
    }

    #region OnHandlers

    public ValueTask OnClientRedirected(ILoginClient client, in Packet packet)
    {
        var args = PacketSerializer.Deserialize<ClientRedirectedArgs>(in packet);
        return ExecuteHandler(client, args, InnerOnClientRedirect);

        ValueTask InnerOnClientRedirect(ILoginClient localClient, ClientRedirectedArgs localArgs)
        {
            if (localArgs.Message == "Redirect Successful")
            {
                localClient.SendLoginMessage(LoginMessageType.Confirm, "Redirected.. Welcome!");
            }

            return default;
        }
    }

    /// <summary>
    /// 0x0C - Character Creation
    /// </summary>
    public ValueTask OnCreateChar(ILoginClient client, in Packet packet)
    {
        var args = PacketSerializer.Deserialize<CreateCharacterArgs>(in packet);
        return ExecuteHandler(client, args, InnerOnCreateCharRequest);

        ValueTask InnerOnCreateCharRequest(ILoginClient localClient, CreateCharacterArgs localArgs)
        {
            var maximumHp = Random.Shared.Next(128, 165);
            var maximumMp = Random.Shared.Next(30, 45);
            _ = StorageManager.AislingBucket.Create(new Aisling
            {
                Created = DateTime.UtcNow,
                Username = localArgs.Username,
                CurrentHp = maximumHp,
                BaseHp = maximumHp,
                CurrentMp = maximumMp,
                BaseMp = maximumMp,
                Gender = (Enums.Gender)localArgs.Sex,
                Race = (Race)localArgs.Race,
                Path = (Class)localArgs.Class,
                SkillBook = new SkillBook(),
                SpellBook = new SpellBook(),
                Inventory = new InventoryManager(),
                BankManager = new BankManager(),
                EquipmentManager = new EquipmentManager(null)
            });

            return default;
        }
    }

    /// <summary>
    /// 0x0D - Delete Character
    /// </summary>
    public ValueTask OnDeleteChar(ILoginClient client, in Packet packet)
    {
        var args = PacketSerializer.Deserialize<DeleteCharacterArgs>(in packet);
        return ExecuteHandler(client, args, InnerOnCreateCharFinalize);

        ValueTask InnerOnCreateCharFinalize(ILoginClient localClient, DeleteCharacterArgs localArgs)
        {
            //_ = StorageManager.AislingBucket.Delete(localArgs.SteamId, localArgs.Username);
            return default;
        }
    }

    /// <summary>
    /// 0x03 - Player Login and Redirect
    /// </summary>
    public ValueTask OnLogin(ILoginClient client, in Packet packet)
    {
        var args = PacketSerializer.Deserialize<LoginArgs>(in packet);
        if (ServerSetup.Instance.Running) return ExecuteHandler(client, args, InnerOnLogin);

        client.SendLoginMessage(LoginMessageType.Confirm, "Server is down for maintenance");
        return default;

        async ValueTask InnerOnLogin(ILoginClient localClient, LoginArgs localArgs)
        {
            if (localArgs.SteamId == 0)
            {
                localClient.SendLoginMessage(LoginMessageType.Confirm, "Invalid ID");
                return;
            }

            var characters = await AislingStorage.LoadAccount(localArgs.SteamId);

            // ToDo: Send characters back to client to display them, so they can be picked for loading. 
            // Need to create converter to send the data like a list. Already created it on the client end. 

            localClient.SendAccountData(characters);

            //if (ServerSetup.Instance.GlobalPasswordAttempt.TryGetValue(localClient.RemoteIp, out var attempts))
            //{
            //    if (attempts >= 5)
            //    {
            //        localClient.SendLoginMessage(LoginMessageType.Confirm, "Your IP has been restricted for too many incorrect password attempts.");
            //        ServerSetup.EventsLogger($"{localClient.RemoteIp} has attempted {attempts} and has been restricted.");
            //        SentrySdk.CaptureException(new Exception($"{localClient.RemoteIp} has been restricted due to password attempts."));
            //        return;
            //    }
            //}

            //var result = await AislingStorage.CheckPassword(localArgs.Name);

            //if (localArgs.Password == ServerSetup.Instance.Unlock)
            //{
            //    var unlockIp = IPAddress.Parse(ServerSetup.Instance.InternalAddress);

            //    if (IPAddress.IsLoopback(localClient.RemoteIp) || localClient.RemoteIp.Equals(unlockIp))
            //    {
            //        result.LastAttemptIP = localClient.RemoteIp.ToString();
            //        result.LastIP = localClient.RemoteIp.ToString();
            //        result.PasswordAttempts = 0;
            //        result.Hacked = false;
            //        result.Password = "abc123";
            //        await SavePassword(result);
            //        localClient.SendLoginMessage(LoginMessageType.Confirm, "Account was unlocked!");
            //        return;
            //    }

            //    ServerSetup.Instance.GlobalPasswordAttempt.AddOrUpdate(localClient.RemoteIp, 1, (remoteIp, creations) => creations += 1);
            //    localClient.SendLoginMessage(LoginMessageType.Confirm, "GM Action, denied access and IP logged.");
            //    ServerSetup.EventsLogger($"{localClient.RemoteIp} has attempted to use the unlock command.");
            //    SentrySdk.CaptureException(new Exception($"{localClient.RemoteIp} has attempted to use the unlock command"));
            //    return;
            //}

            //var passed = await OnSecurityCheck(result, localClient, localArgs.Name, localArgs.Password);
            //if (!passed) return;

            //switch (localArgs.Name.ToLowerInvariant())
            //{
            //    case "asdf":
            //        localClient.SendLoginMessage(LoginMessageType.Confirm, "Locked Account, denied access");
            //        return;
            //    case "death":
            //        {
            //            var ipLocal = IPAddress.Parse(ServerSetup.Instance.InternalAddress);

            //            if (IPAddress.IsLoopback(localClient.RemoteIp) || localClient.RemoteIp.Equals(ipLocal))
            //            {
            //                Login(result, localClient);
            //                return;
            //            }

            //            localClient.SendLoginMessage(LoginMessageType.Confirm, "GM Account, denied access");
            //            return;
            //        }
            //    case "scythe":
            //        {
            //            var ipLocal = IPAddress.Parse(ServerSetup.Instance.InternalAddress);

            //            if (GameMastersIPs.Any(ip => localClient.RemoteIp.Equals(IPAddress.Parse(ip)))
            //                || IPAddress.IsLoopback(localClient.RemoteIp) || localClient.RemoteIp.Equals(ipLocal))
            //            {
            //                Login(result, localClient);
            //                return;
            //            }

            //            localClient.SendLoginMessage(LoginMessageType.Confirm, "GM Account, denied access");
            //            return;
            //        }
            //    default:
            //        {
            //            if (result.Hacked)
            //            {
            //                localClient.SendLoginMessage(LoginMessageType.Confirm, "Bruteforce detected, we've locked the account to protect it; If this is your account, please contact the GM.");
            //                return;
            //            }

            //            Login(result, localClient);
            //            break;
            //        }
            //}
        }
    }

    private void Login(Aisling player, ILoginClient loginClient)
    {
        player.LastAttemptIP = loginClient.RemoteIp.ToString();
        player.LastIP = loginClient.RemoteIp.ToString();
        player.PasswordAttempts = 0;
        player.Hacked = false;
        _ = SavePassword(player);
        loginClient.SendLoginMessage(LoginMessageType.Confirm, "Logged in!");
    }

    /// <summary>
    /// 0x0B - Exit Request
    /// </summary>
    public ValueTask OnExitRequest(ILoginClient client, in Packet clientPacket)
    {
        return ExecuteHandler(client, InnerOnExitRequest);

        ValueTask InnerOnExitRequest(ILoginClient localClient)
        {
            ClientRegistry.TryRemove(localClient.Id, out _);
            ServerSetup.ConnectionLogger($"{localClient.RemoteIp} disconnected from Login Server");
            return default;
        }
    }

    #endregion

    #region Connection / Handler

    public override ValueTask HandlePacketAsync(ILoginClient client, in Packet packet)
    {
        var opCode = packet.OpCode;
        var handler = ClientHandlers[opCode];

        try
        {
            if (handler is not null) 
                return handler(client, in packet);

            // Log unknown packet
            ServerSetup.ConnectionLogger($"{opCode} from {client.RemoteIp} - {packet.ToString()}", LogLevel.Error);
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(new Exception($"Unknown packet {opCode} from {client.RemoteIp} on LoginServer \n {ex}"));
        }

        return default;
    }

    protected override void IndexHandlers()
    {
        base.IndexHandlers();
        ClientHandlers[(byte)ClientOpCode.ClientRedirected] = OnClientRedirected;
        ClientHandlers[(byte)ClientOpCode.CreateCharacter] = OnCreateChar;
        ClientHandlers[(byte)ClientOpCode.DeleteCharacter] = OnDeleteChar;
        ClientHandlers[(byte)ClientOpCode.OnClientLogin] = OnLogin;
        ClientHandlers[(byte)ClientOpCode.ExitRequest] = OnExitRequest;
    }

    protected override void OnConnected(Socket clientSocket)
    {
        ServerSetup.ConnectionLogger($"Login connection from {clientSocket.RemoteEndPoint as IPEndPoint}");

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

            foreach (var _ in ServerSetup.Instance.GlobalKnownGoodActorsCache.Values.Where(savedIp =>
                         savedIp == ipAddress.ToString()))
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

            if (!lobbyCheck)
            {
                try
                {
                    client.Disconnect();
                }
                catch
                {
                    // ignored
                }

                ServerSetup.ConnectionLogger("---------Login-Server---------");
                var comment =
                    $"{ipAddress} has been blocked for violating security protocols through improper port access.";
                ServerSetup.ConnectionLogger(comment, LogLevel.Warning);
                BadActor.ReportMaliciousEndpoint(ipAddress.ToString(), comment);
                return;
            }

            ServerSetup.Instance.GlobalLoginConnection.TryAdd(ipAddress, ipAddress);
            client.BeginReceive();
            // 0x7E - Handshake
            client.SendAcceptConnection("Login Connected");
        }
        catch
        {
            ServerSetup.ConnectionLogger($"Failed to authenticate loginServer using SSL/TLS.");
        }
    }

    private void OnDisconnect(object sender, EventArgs e)
    {
        var client = (ILoginClient)sender!;
        ClientRegistry.TryRemove(client.Id, out _);
    }

    private static async Task<bool> SavePassword(Aisling aisling)
    {
        if (aisling == null) return false;

        try
        {
            return await AislingStorage.PasswordSave(aisling);
        }
        catch (Exception ex)
        {
            ServerSetup.ConnectionLogger(ex.Message, LogLevel.Error);
            ServerSetup.ConnectionLogger(ex.StackTrace, LogLevel.Error);
            SentrySdk.CaptureException(ex);
        }

        return false;
    }

    private static async Task<bool> SavePasswordAttempt(Aisling aisling)
    {
        if (aisling == null) return false;

        try
        {
            return await AislingStorage.PasswordSaveAttempt(aisling);
        }
        catch (Exception ex)
        {
            ServerSetup.ConnectionLogger(ex.Message, LogLevel.Error);
            ServerSetup.ConnectionLogger(ex.StackTrace, LogLevel.Error);
            SentrySdk.CaptureException(ex);
        }

        return false;
    }

    #endregion
}