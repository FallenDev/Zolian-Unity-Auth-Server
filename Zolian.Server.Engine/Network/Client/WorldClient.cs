using System.Diagnostics;
using System.Net.Sockets;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using IWorldClient = Darkages.Network.Client.Abstractions.IWorldClient;
using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Darkages.Network.Client;

[UsedImplicitly]
public class WorldClient : WorldClientBase, IWorldClient
{
    public WorldClient(Socket socket, IPacketSerializer packetSerializer, ILogger<ConnectedClientBase> logger) : base(socket, packetSerializer, logger)
    {
    }

    protected override ValueTask HandlePacketAsync(Span<byte> span)
    {
        throw new NotImplementedException();
    }

    public Stopwatch Latency { get; set; }
    public void SendConfirmExit()
    {
        throw new NotImplementedException();
    }

    public void SendServerMessage(ServerMessageType serverMessageType, string message)
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