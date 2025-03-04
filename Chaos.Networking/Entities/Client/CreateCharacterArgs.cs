using Chaos.DarkAges.Definitions;
using Chaos.Packets.Abstractions;

namespace Chaos.Networking.Entities.Client;

public sealed record CreateCharacterArgs : IPacketSerializable
{
    public required long SteamId { get; set; }
    public required string Username { get; set; }
    public required BaseClass Class { get; set; }
    public required BaseRace Race { get; set; }
    public required Sex Sex { get; set; }
}