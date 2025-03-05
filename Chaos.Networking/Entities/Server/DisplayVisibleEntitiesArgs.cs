using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.DisplayVisibleEntities" /> packet
/// </summary>
public sealed record DisplayVisibleEntitiesArgs : IPacketSerializable
{
    /// <summary>
    ///     The (non-aisling) visible entities being sent to the client to display
    /// </summary>
    public ICollection<VisibleEntityInfo> VisibleObjects { get; set; } = [];
}