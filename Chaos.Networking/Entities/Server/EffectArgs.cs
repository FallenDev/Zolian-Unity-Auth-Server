using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.Effect" /> packet
/// </summary>
public sealed record EffectArgs : IPacketSerializable
{
    /// <summary>
    ///     The color of the effect
    /// </summary>
    public EffectColor EffectColor { get; set; }

    /// <summary>
    ///     The icon of the effect. This acts like a key, you can only have 1 effect with the same icon
    /// </summary>
    public byte EffectIcon { get; set; }
}