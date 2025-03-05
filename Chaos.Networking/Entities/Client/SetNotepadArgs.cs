using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.SetNotepad" /> packet
/// </summary>
public sealed record SetNotepadArgs : IPacketSerializable
{
    /// <summary>
    ///     The message the client is trying to write to the object
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    ///     The slot of the object of which text is being written to
    /// </summary>
    public required byte Slot { get; set; }
}