using Zolian.Networking.Entities.Server;

namespace Zolian.Networking.Abstractions;

/// <summary>
///     Represents a client that is connected to the world server.
/// </summary>
public interface IWorldClient : IConnectedClient
{
    /// <summary>
    ///     Sends a packet to remove an entity from the client.
    /// </summary>
    void SendRemoveEntity(RemoveEntityArgs args);

    /// <summary>
    ///     Sends a packet to display a server message.
    /// </summary>
    void SendServerMessage(ServerMessageArgs args);

    /// <summary>
    ///     Sends a packet to play a sound.
    /// </summary>
    void SendSound(SoundArgs args);
}
