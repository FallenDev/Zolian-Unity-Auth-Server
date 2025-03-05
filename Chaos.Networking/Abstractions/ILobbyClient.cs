using Zolian.Networking.Entities.Server;

namespace Zolian.Networking.Abstractions;

/// <summary>
///     Defines a pattern for an object that represents a client connected to a lobby server.
/// </summary>
public interface ILobbyClient : IConnectedClient
{
    /// <summary>
    ///     Sends the connection information to the client.
    /// </summary>
    void SendConnectionInfo(ConnectionInfoArgs args);

    /// <summary>
    ///     Sends the server table response to the client.
    /// </summary>
    void SendServerTableResponse(ServerTableResponseArgs args);
}