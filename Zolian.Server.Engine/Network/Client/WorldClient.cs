using System.Diagnostics;
using System.Net.Sockets;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using IWorldClient = Zolian.Network.Client.Abstractions.IWorldClient;
using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;
using Zolian.Packets;

namespace Zolian.Network.Client;

[UsedImplicitly]
public class WorldClient([NotNull] IWorldServer<IWorldClient> server, [NotNull] Socket socket,
    [NotNull] IPacketSerializer packetSerializer,
    [NotNull] ILogger<WorldClient> logger)
    : WorldClientBase(socket, packetSerializer, logger), IWorldClient
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

    public Stopwatch Latency { get; set; }
    public void SendConfirmExit()
    {
        throw new NotImplementedException();
    }

    public void SendServerMessage(PopupMessageType serverMessageType, string message)
    {
        throw new NotImplementedException();
    }

    public void SendSound(byte sound, bool isMusic)
    {
        throw new NotImplementedException();
    }

    public WorldClient SystemMessage(string message)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Save()
    {
        throw new NotImplementedException();
    }

    public WorldClient LoggedIn(bool state)
    {
        throw new NotImplementedException();
    }
}