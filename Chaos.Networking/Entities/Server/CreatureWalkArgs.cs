using Zolian.Geometry.Abstractions;
using Zolian.Geometry.Abstractions.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.CreatureWalk" /> packet
/// </summary>
public sealed record CreatureWalkArgs : IPacketSerializable
{
    /// <summary>
    ///     The direction the creature should walk towards
    /// </summary>
    public Direction Direction { get; set; }

    /// <summary>
    ///     The point the creature is supposed to be walking from
    /// </summary>
    public IPoint OldPoint { get; set; } = null!;

    /// <summary>
    ///     The id of the creature
    /// </summary>
    public uint SourceId { get; set; }
}