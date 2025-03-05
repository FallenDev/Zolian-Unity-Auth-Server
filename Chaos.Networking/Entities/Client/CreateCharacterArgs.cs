using Zolian.Networking.Definitions;
using Zolian.Packets.Abstractions;
using BaseClass = Chaos.DarkAges.Definitions.BaseClass;

namespace Zolian.Networking.Entities.Client;

public sealed record CreateCharacterArgs : IPacketSerializable
{
    public required long SteamId { get; set; }
    public required string Username { get; set; }
    public required BaseClass Class { get; set; }
    public required Race Race { get; set; }
    public required Sex Sex { get; set; }
}