using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.LoginMessage" /> packet
/// </summary>
public sealed record LoginMessageArgs : IPacketSerializable
{
    /// <summary>
    ///     The type of login message to be used
    /// </summary>
    public PopupMessageType LoginMessageType { get; set; }

    /// <summary>
    ///     If the login message type can have a custom message, this will be the message displayed.
    /// </summary>
    public string? Message { get; set; }
}