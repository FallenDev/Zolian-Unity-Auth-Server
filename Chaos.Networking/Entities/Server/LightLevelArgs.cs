using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="Chaos.DarkAges.Definitions.LightLevel" /> packet
/// </summary>
public sealed record LightLevelArgs : IPacketSerializable
{
    /// <summary>
    ///     The light level to be used for the current map. This is basically like time of day.
    /// </summary>
    public LightLevel LightLevel { get; set; }
}