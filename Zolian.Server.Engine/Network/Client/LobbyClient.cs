using JetBrains.Annotations;

using Microsoft.Extensions.Logging;

using System.Net.Sockets;
using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets;
using Zolian.Packets.Abstractions;
using ILobbyClient = Zolian.Network.Client.Abstractions.ILobbyClient;

namespace Zolian.Network.Client;

[UsedImplicitly]
public class LobbyClient([NotNull] ILobbyServer<ILobbyClient> server, [NotNull] Socket socket,
        [NotNull] IPacketSerializer packetSerializer,
        [NotNull] ILogger<LobbyClient> logger)
    : LobbyClientBase(socket, packetSerializer, logger), ILobbyClient
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

            // Pass the fully constructed Packet to the server for handling
            return server.HandlePacketAsync(this, in packet);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error parsing packet from span: {RawBuffer}", BitConverter.ToString(span.ToArray()));
            return default;
        }
    }

    public void SendConnectionInfo(ushort port)
    {
        var args = new ConnectionInfoArgs
        {
            PortNumber = port
        };

        Send(args);

        // After sending the connection info for login client redirection, disconnect the lobby client, recovering resources
        Disconnect();
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
}