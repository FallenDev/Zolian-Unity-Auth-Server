using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.Version" /> packet
/// </summary>
public sealed record VersionArgs : IPacketSerializable
{
    /// <summary>
    ///     The client version as a single number
    /// </summary>
    public required string Version { get; set; }
}