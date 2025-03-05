using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.BeginChant" /> packet
/// </summary>
public sealed record BeginChantArgs : IPacketSerializable
{
    /// <summary>
    ///     The number of cast lines for the spell being chanted
    /// </summary>
    public required byte CastLineCount { get; set; }
}