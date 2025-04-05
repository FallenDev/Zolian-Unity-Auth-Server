using System.Diagnostics;
using JetBrains.Annotations;

using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Sprites.Entities;

namespace Zolian.Network.Client.Abstractions;

public interface IWorldClient : IConnectedClient
{
    Stopwatch Latency { get; set; }
    void SendCharacterData(Player player, UpdateType type);
    void SendConfirmExit();
    void SendLoginMessage(PopupMessageType loginMessageType, [CanBeNull] string message = null);
    void SendServerMessage(PopupMessageType serverMessageType, string message);
    void SendSound(byte sound, bool isMusic);
    WorldClient SystemMessage(string message);
    Task<bool> Save();
    WorldClient LoggedIn(bool state);
}