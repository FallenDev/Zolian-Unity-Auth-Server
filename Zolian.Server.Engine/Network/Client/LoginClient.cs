using JetBrains.Annotations;

using Microsoft.Extensions.Logging;

using System.Net.Sockets;

using Zolian.Enums;
using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets;
using Zolian.Packets.Abstractions;
using Zolian.Sprites.Entities;

using ILoginClient = Zolian.Network.Client.Abstractions.ILoginClient;

namespace Zolian.Network.Client;

[UsedImplicitly]
public class LoginClient([NotNull] ILoginServer<ILoginClient> server, [NotNull] Socket socket,
        [NotNull] IPacketSerializer packetSerializer,
        [NotNull] ILogger<LoginClient> logger)
    : LoginClientBase(socket, packetSerializer, logger), ILoginClient
{
    protected override ValueTask HandlePacketAsync(Span<byte> span)
    {
        try
        {
            // Fully parse the Packet from the span
            var packet = new Packet(ref span);

            if (packet.Payload.Length == 0)
            {
                Logger.LogWarning("Received packet with empty payload. OpCode={OpCode}", packet.OpCode);
            }

            // Pass the packet to the server for further handling
            return server.HandlePacketAsync(this, in packet);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error parsing packet from span: {RawBuffer}", BitConverter.ToString(span.ToArray()));
            return default;
        }
    }
    
    public void SendLoginMessage(PopupMessageType loginMessageType, string message = null)
    {
        var args = new LoginMessageArgs
        {
            LoginMessageType = loginMessageType,
            Message = message
        };

        Send(args);
    }

    public void SendAccountData(List<Player> players)
    {
        
        var args = new AccountListArgs
        {
            Players = []
        };

        foreach (var player in players)
        {
            args.Players.Add(new AccountListArgs.PlayerSelection
            {
                Serial = player.Serial,
                Name = player.Username,
                Level = player.EntityLevel + player.JobLevel,
                BaseClass = ClassStrings.BaseClassValue(player.FirstClass),
                AdvClass = ClassStrings.BaseClassValue(player.SecondClass),
                Job = ClassStrings.JobValue(player.JobClass),
                Health = player.BaseHp,
                Mana = player.BaseMp
            });
        }

        Send(args);
    }

    public void SendCharacterFinalized()
    {
        var args = new CharacterFinalizedArgs();
        Send(args);
    }
}