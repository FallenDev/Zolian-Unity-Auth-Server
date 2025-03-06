using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Abstractions;

/// <summary>
///     Represents a client that is connected to an <see cref="IServer{T}" />. This class defines the methods used to
///     communicate with the client.
/// </summary>
public abstract class ConnectedClientBase : SocketClientBase, IConnectedClient
{
    /// <inheritdoc />
    protected ConnectedClientBase(
        Socket socket,
        IPacketSerializer packetSerializer,
        ILogger<ConnectedClientBase> logger)
        : base(
            socket,
            packetSerializer,
            logger) { }

    /// <inheritdoc />
    public virtual void SendAcceptConnection(string message)
    {
        var args = new AcceptConnectionArgs
        {
            Message = message
        };

        Send(args);
    }
}