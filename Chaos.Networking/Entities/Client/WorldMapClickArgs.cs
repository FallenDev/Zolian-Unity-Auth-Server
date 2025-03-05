using Zolian.Geometry.Abstractions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.WorldMapClick" /> packet
/// </summary>
public sealed record WorldMapClickArgs : IPacketSerializable
{
    /// <summary>
    ///     The checksum or unique id of the node the player has clicked on
    /// </summary>
    public required ushort CheckSum { get; set; }

    /// <summary>
    ///     The id of the map the node leads to
    /// </summary>
    public required ushort MapId { get; set; }

    /// <summary>
    ///     The point on the map the node leads to
    /// </summary>
    public required IPoint Point { get; set; }
}