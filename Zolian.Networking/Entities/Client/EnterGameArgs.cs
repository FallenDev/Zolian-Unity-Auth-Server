using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

public sealed record EnterGameArgs : IPacketSerializable
{
    public required Guid Serial { get; set; }
    public required long SteamId { get; set; }
    public required string UserName { get; set; }
}