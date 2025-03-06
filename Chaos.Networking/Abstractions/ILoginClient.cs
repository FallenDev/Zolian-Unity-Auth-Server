using Zolian.Networking.Entities.Server;

namespace Zolian.Networking.Abstractions;

/// <summary>
///     Represents a client that is connected to the login server.
/// </summary>
public interface ILoginClient : IConnectedClient
{
    /// <summary>
    ///     Sends a login message to the client.
    /// </summary>
    void SendLoginMessage(LoginMessageArgs args);
}