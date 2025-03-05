using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.DisplayEntityRequest" /> packet
///     <br />
///     I suggest not responding to this packet
/// </summary>
public sealed record DisplayEntityRequestArgs : IPacketSerializable
{
    /// <summary>
    ///     The id of the object the client is requesting the server to display
    /// </summary>
    public required uint TargetId { get; set; }
}