using System.Diagnostics;
using JetBrains.Annotations;

using Zolian.Networking.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Sprites;
using Zolian.Sprites.Entities;

namespace Zolian.Network.Client.Abstractions;

public interface IWorldClient : IConnectedClient
{
    public Guid PlayerSerial { get; set; }
    Stopwatch Latency { get; set; }
    WorldClient LoggedIn(Player player);
    void SendCharacterData(Player player, UpdateType type);
    void SendConfirmExit();
    void SendLoginMessage(PopupMessageType loginMessageType, [CanBeNull] string message = null);
    void SendServerMessage(PopupMessageType serverMessageType, string message);
    void SendSound(byte sound, bool isMusic);
    void SendEntityPlayerSpawn(Player player);
    
    void SendEntityDespawn(Guid entity);
    void SendPlayerPositionUpdate(Player entity);
    void SendEntityPositionUpdate(Movable entity);
}