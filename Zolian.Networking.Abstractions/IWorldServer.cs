using Zolian.Packets;

namespace Zolian.Networking.Abstractions;

public interface IWorldServer<in TClient> : IServer<TClient> where TClient: IConnectedClient
{
    ValueTask OnClientRedirected(TClient client, in Packet packet);
    ValueTask OnEnterGame(TClient client, in Packet packet);
    ValueTask OnEntityMovement(TClient client, in Packet packet);
}