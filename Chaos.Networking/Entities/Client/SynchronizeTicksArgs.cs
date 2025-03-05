using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.SynchronizeTicks" /> packet
/// </summary>
public sealed record SynchronizeTicksArgs : IPacketSerializable
{
    /// <summary>
    ///     The ticks the client is using
    /// </summary>
    public required uint ClientTicks { get; set; }

    /// <summary>
    ///     The ticks the client thinks the server is using
    /// </summary>
    public required uint ServerTicks { get; set; }
}