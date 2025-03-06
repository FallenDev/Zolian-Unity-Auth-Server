using Darkages.Sprites;
using Darkages.Sprites.Entity;
using JetBrains.Annotations;
using Zolian.Networking.Abstractions;

namespace Darkages.Network.Client.Abstractions;

public interface ILoginClient : IConnectedClient
{
    void SendLoginMessage(LoginMessageType loginMessageType, [CanBeNull] string message = null);
    void SendAccountData(List<Aisling> players);
}