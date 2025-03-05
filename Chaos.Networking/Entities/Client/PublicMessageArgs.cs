using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.PublicMessage" /> packet
/// </summary>
public sealed record PublicMessageArgs : IPacketSerializable
{
    /// <summary>
    ///     The message the client is trying to send
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    ///     The type of the public message the client is trying to send
    /// </summary>
    public required PublicMessageType PublicMessageType { get; set; }
}