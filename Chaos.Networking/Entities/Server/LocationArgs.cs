using Zolian.Geometry;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="Location" /> packet
/// </summary>
public sealed record LocationArgs : IPacketSerializable
{
    /// <summary>
    ///     The X coordinate of the player
    /// </summary>
    public int X { get; set; }

    /// <summary>
    ///     The Y coordinate of the player
    /// </summary>
    public int Y { get; set; }
}