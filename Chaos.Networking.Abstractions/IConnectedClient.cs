namespace Zolian.Networking.Abstractions;

/// <summary>
///     Defines a pattern for an object that represents a client connected to an <see cref="IServer{T}" />. This interface
///     contains the definitions used to communicate with the client.
/// </summary>
public interface IConnectedClient : ISocketClient
{
    /// <summary>
    ///     Used when a client connects to respond with a string.
    /// </summary>
    void SendAcceptConnection(string message);
}