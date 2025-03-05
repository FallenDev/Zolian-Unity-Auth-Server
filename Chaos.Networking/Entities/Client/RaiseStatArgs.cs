using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.RaiseStat" /> packet
/// </summary>
public sealed record RaiseStatArgs : IPacketSerializable
{
    /// <summary>
    ///     The stat the client is trying to raise
    /// </summary>
    public required Stat Stat { get; set; }
}