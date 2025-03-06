using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Abstractions;

/// <summary>
///     Represents a client connected to the world server
/// </summary>
public abstract class WorldClientBase : ConnectedClientBase, IWorldClient
{
    /// <inheritdoc />
    protected WorldClientBase(
        Socket socket,
        IPacketSerializer packetSerializer,
        ILogger<ConnectedClientBase> logger)
        : base(
            socket,
            packetSerializer,
            logger) { }
    
    /// <inheritdoc />
    public virtual void SendRemoveEntity(RemoveEntityArgs args) => Send(args);

    /// <inheritdoc />
    public virtual void SendServerMessage(ServerMessageArgs args) => Send(args);

    /// <inheritdoc />
    public virtual void SendSound(SoundArgs args) => Send(args);
}
