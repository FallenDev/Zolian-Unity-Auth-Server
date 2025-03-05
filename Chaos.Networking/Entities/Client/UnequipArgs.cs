using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.Unequip" /> packet
/// </summary>
public sealed record UnequipArgs : IPacketSerializable
{
    /// <summary>
    ///     The equipment slot of the item the client is trying to unequip
    /// </summary>
    public required EquipmentSlot EquipmentSlot { get; set; }
}