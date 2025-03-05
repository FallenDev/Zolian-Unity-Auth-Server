using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.MapChangePending" /> packet
/// </summary>
public sealed record MapChangePendingArgs : IPacketSerializable;