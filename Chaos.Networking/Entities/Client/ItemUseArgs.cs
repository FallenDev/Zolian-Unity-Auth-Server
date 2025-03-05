using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.ItemUse" /> packet
/// </summary>
public sealed record ItemUseArgs : IPacketSerializable
{
    /// <summary>
    ///     The slot of the item the client is trying to use
    /// </summary>
    public required byte SourceSlot { get; set; }
}