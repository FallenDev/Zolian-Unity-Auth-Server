using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

public sealed record LoginArgs : IPacketSerializable
{
    public required long SteamId { get; set; }
}