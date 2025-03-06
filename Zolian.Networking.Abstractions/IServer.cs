using Microsoft.Extensions.Hosting;
using Zolian.Packets;

namespace Zolian.Networking.Abstractions;

/// <summary>
///     Defines the bare minimum for a server
/// </summary>
public interface IServer<in TClient> : IHostedService where TClient: IConnectedClient
{
    /// <summary>
    ///     A catch-all that will re-route a packet to the correct handler
    /// </summary>
    ValueTask HandlePacketAsync(TClient client, in Packet packet);
}