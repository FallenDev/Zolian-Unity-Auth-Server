using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="Chaos.DarkAges.Definitions.SocialStatus" /> packet
/// </summary>
public sealed record SocialStatusArgs : IPacketSerializable
{
    /// <summary>
    ///     The social status the client is trying to change their status to
    /// </summary>
    public required SocialStatus SocialStatus { get; set; }
}