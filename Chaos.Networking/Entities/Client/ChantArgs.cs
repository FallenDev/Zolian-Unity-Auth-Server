using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.Chant" /> packet
/// </summary>
public sealed record ChantArgs : IPacketSerializable
{
    /// <summary>
    ///     The chant to be displayed
    /// </summary>
    public required string ChantMessage { get; set; }
}