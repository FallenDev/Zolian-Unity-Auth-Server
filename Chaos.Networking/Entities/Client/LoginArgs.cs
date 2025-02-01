using Chaos.Packets.Abstractions;

namespace Chaos.Networking.Entities.Client;

public sealed record LoginArgs : IPacketSerializable
{
    public required long SteamId { get; set; }
}