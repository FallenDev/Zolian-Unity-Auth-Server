using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.Emote" /> packet
/// </summary>
public sealed record EmoteArgs : IPacketSerializable
{
    /// <summary>
    ///     The body animation the client is requesting to be displayed.
    /// </summary>
    public required BodyAnimation BodyAnimation { get; set; }
}