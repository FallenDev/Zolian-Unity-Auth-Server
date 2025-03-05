using Zolian.Geometry.Abstractions;
using Zolian.Geometry.Abstractions.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.ClientWalkResponse" /> packet
/// </summary>
public sealed record ClientWalkResponseArgs : IPacketSerializable
{
    /// <summary>
    ///     The direction for the client to walk
    /// </summary>
    public Direction Direction { get; set; }

    /// <summary>
    ///     The point the client is supposed to be walking from
    /// </summary>
    public IPoint OldPoint { get; set; } = null!;
}