using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of a ground item in the <see cref="ServerOpCode.DisplayVisibleEntities" /> packet
/// </summary>
public sealed record GroundItemInfo : VisibleEntityInfo
{
    /// <summary>
    ///     The color of the ground item
    /// </summary>
    public DisplayColor Color { get; set; }
}