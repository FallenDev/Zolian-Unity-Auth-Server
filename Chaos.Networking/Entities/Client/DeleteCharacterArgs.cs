using Chaos.Packets.Abstractions;

namespace Chaos.Networking.Entities.Client;

public sealed record DeleteCharacterArgs : IPacketSerializable
{
    public required long SteamId { get; set; }
    public required string Username { get; set; }
}