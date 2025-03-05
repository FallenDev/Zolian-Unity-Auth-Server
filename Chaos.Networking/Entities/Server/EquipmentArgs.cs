using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.Equipment" /> packet
/// </summary>
public sealed record EquipmentArgs : IPacketSerializable
{
    /// <summary>
    ///     Details about the item being added to the equipment pane
    /// </summary>
    public ItemInfo Item { get; set; } = null!;

    /// <summary>
    ///     The equipment slot the item is being added to
    /// </summary>
    public EquipmentSlot Slot { get; set; }
}