using JetBrains.Annotations;

using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Sprites.Entities;

namespace Zolian.Network.Client.Abstractions;

public interface ILoginClient : IConnectedClient
{
    void SendLoginMessage(LoginMessageType loginMessageType, [CanBeNull] string message = null);
    void SendAccountData(List<Player> players);
}