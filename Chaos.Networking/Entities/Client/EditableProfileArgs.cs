using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.EditableProfile" /> packet
/// </summary>
public sealed record EditableProfileArgs : IPacketSerializable
{
    /// <summary>
    ///     The data of the client's custom portrait
    /// </summary>
    public required byte[] PortraitData { get; set; }

    /// <summary>
    ///     The text in the client's custom profile
    /// </summary>
    public required string ProfileMessage { get; set; }
}