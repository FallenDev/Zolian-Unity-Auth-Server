using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.ServerTableResponse" /> packet
/// </summary>
public sealed record ServerTableResponseArgs : IPacketSerializable
{
    /// <summary>
    ///     The raw server table data
    /// </summary>
    public byte[] ServerTable { get; set; } = null!;
}