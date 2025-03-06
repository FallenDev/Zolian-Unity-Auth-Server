using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.ClientRedirected" /> packet
/// </summary>
public sealed record ClientRedirectedArgs : IPacketSerializable
{
    public required string Message { get; set; }
}