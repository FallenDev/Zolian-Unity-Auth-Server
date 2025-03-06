using Zolian.Sprites.Entity;
using JetBrains.Annotations;
using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;

namespace Zolian.Network.Client.Abstractions;

public interface ILoginClient : IConnectedClient
{
    void SendLoginMessage(LoginMessageType loginMessageType, [CanBeNull] string message = null);
    void SendAccountData(List<Aisling> players);
}