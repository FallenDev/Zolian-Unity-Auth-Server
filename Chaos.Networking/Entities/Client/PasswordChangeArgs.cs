using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.PasswordChange" /> packet
/// </summary>
public sealed record PasswordChangeArgs : IPacketSerializable
{
    /// <summary>
    ///     The current password of the character
    /// </summary>
    public required string CurrentPassword { get; set; }

    /// <summary>
    ///     The name of the character for which the client is trying to change the password
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     The intended new password for the character
    /// </summary>
    public required string NewPassword { get; set; }
}