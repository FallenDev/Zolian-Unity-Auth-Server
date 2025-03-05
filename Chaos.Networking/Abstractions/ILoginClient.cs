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

    /// <summary>
    ///     Sends a login notice to the client.
    /// </summary>
    void SendLoginNotice(LoginNoticeArgs args);

    /// <summary>
    ///     Sends metadata to the client.
    /// </summary>
    void SendMetaData(MetaDataArgs args);
}