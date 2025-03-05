using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Abstractions;

/// <summary>
///     Represents a client connected to a login server.
/// </summary>
public abstract class LoginClientBase : ConnectedClientBase, ILoginClient
{
    /// <inheritdoc />
    protected LoginClientBase(
        Socket socket,
        IPacketSerializer packetSerializer,
        ILogger<ConnectedClientBase> logger)
        : base(
            socket,
            packetSerializer,
            logger) { }
    
    /// <inheritdoc />
    public virtual void SendLoginMessage(LoginMessageArgs args) => Send(args);

    /// <inheritdoc />
    public virtual void SendLoginNotice(LoginNoticeArgs args) => Send(args);

    /// <inheritdoc />
    public virtual void SendMetaData(MetaDataArgs args) => Send(args);
}