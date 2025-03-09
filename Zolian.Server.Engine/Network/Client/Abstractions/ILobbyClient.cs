using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;

namespace Zolian.Network.Client.Abstractions;

public interface ILobbyClient : IConnectedClient
{
    void SendConnectionInfo(ushort port);
    void SendLoginMessage(PopupMessageType loginMessageType, string message = null);
}