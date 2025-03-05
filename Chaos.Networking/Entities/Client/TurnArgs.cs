using Zolian.Geometry.Abstractions.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.Turn" /> packet
/// </summary>
public sealed record TurnArgs : IPacketSerializable
{
    /// <summary>
    ///     The direction the client is trying to turn
    /// </summary>
    public required Direction Direction { get; set; }
}