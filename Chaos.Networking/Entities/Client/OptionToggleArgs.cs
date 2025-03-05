using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.OptionToggle" /> packet
/// </summary>
public sealed record OptionToggleArgs : IPacketSerializable
{
    /// <summary>
    ///     The option the client is trying to toggle
    /// </summary>
    public required UserOption UserOption { get; set; }
}