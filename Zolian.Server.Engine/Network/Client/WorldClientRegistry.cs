using Zolian.Network.Client.Abstractions;
using ServiceStack;
using Zolian.Networking;

namespace Zolian.Network.Client;

public sealed class WorldClientRegistry : ClientRegistry<IWorldClient>
{
    // ToDo: Change to new Player handling
    public override IEnumerator<IWorldClient> GetEnumerator() => Clients.Values/*.Where(c => c.Aisling != null)*/.GetEnumerator();
}