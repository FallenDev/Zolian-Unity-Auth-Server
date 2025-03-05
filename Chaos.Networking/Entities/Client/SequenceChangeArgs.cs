using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.SequenceChange" /> packet
/// </summary>
public sealed record SequenceChangeArgs : ISequencerPacket
{
    /// <summary>
    ///     The new sequence number (the sequence of the packet that send this)
    /// </summary>
    public byte Sequence { get; set; }
}