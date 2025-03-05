using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.RemoveItemFromPane" /> packet
/// </summary>
public sealed record RemoveItemFromPaneArgs : IPacketSerializable
{
    /// <summary>
    ///     The slot of the item to be removed
    /// </summary>
    public byte Slot { get; set; }
}