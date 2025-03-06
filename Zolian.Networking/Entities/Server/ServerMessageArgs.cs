using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.ServerMessage" /> packet
/// </summary>
public sealed record ServerMessageArgs : IPacketSerializable
{
    /// <summary>
    ///     The message to display
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    ///     The type of message to display
    /// </summary>
    public ServerMessageType ServerMessageType { get; set; }
}