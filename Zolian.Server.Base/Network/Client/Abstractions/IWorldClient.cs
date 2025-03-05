using System.Diagnostics;
using Zolian.Networking.Abstractions;

namespace Darkages.Network.Client.Abstractions;

public interface IWorldClient : IConnectedClient
{
    Stopwatch Latency { get; set; }
    void SendConfirmExit();
    void SendServerMessage(ServerMessageType serverMessageType, string message);
    void SendSound(byte sound, bool isMusic);
    WorldClient SystemMessage(string message);
    Task<bool> Save();
    WorldClient LoggedIn(bool state);
}