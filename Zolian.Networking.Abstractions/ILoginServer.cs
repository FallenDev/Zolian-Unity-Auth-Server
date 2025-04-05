using Zolian.Packets;

namespace Zolian.Networking.Abstractions;

/// <summary>
///     Defines a pattern for a server that facilitates character creation, presentation of the EULA, and the ability to
///     log into a character or change a character's password
/// </summary>
public interface ILoginServer<in TClient> : IServer<TClient> where TClient: IConnectedClient
{
    ValueTask OnClientRedirected(TClient client, in Packet packet);
    ValueTask OnCreateChar(TClient client, in Packet packet);
    ValueTask OnDeleteChar(TClient client, in Packet packet);
    ValueTask OnLogin(TClient client, in Packet packet);
    ValueTask OnExitRequest(TClient client, in Packet packet);
    ValueTask OnWorldEnter(TClient client, in Packet packet);
}