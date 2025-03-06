using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;

namespace Darkages.Network.Client.Abstractions;

public interface ILobbyClient : IConnectedClient
{
    void SendConnectionInfo(ushort port);
    void SendLoginMessage(LoginMessageType loginMessageType, string message = null);
}