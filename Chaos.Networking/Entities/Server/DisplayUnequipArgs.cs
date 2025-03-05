using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.DisplayUnequip" /> packet
/// </summary>
public sealed record DisplayUnequipArgs : IPacketSerializable
{
    /// <summary>
    ///     The equipment slot to unequip
    /// </summary>
    public EquipmentSlot EquipmentSlot { get; set; }
}