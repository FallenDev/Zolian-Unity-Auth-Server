using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.AddSpellToPane" /> packet
/// </summary>
public sealed record AddSpellToPaneArgs : IPacketSerializable
{
    /// <summary>
    ///     Details about the spell being added to the pane
    /// </summary>
    public SpellInfo Spell { get; set; } = null!;
}