using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Abstractions;

/// <summary>
///     Represents a client connected to a lobby server.
/// </summary>
public abstract class LobbyClientBase : ConnectedClientBase, ILobbyClient
{
    /// <inheritdoc />
    protected LobbyClientBase(
        Socket socket,
        IPacketSerializer packetSerializer,
        ILogger<ConnectedClientBase> logger)
        : base(
            socket,
            packetSerializer,
            logger) { }

    /// <inheritdoc />
    public virtual void SendConnectionInfo(ConnectionInfoArgs args) => Send(args);
}