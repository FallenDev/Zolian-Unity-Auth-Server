using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.ConnectionInfo" /> packet
/// </summary>
public sealed record ConnectionInfoArgs : IPacketSerializable
{
    public ushort PortNumber { get; set; }
}