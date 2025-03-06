using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

public sealed record DeleteCharacterArgs : IPacketSerializable
{
    public required long SteamId { get; set; }
    public required string Username { get; set; }
}