using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

public sealed record CreateCharacterArgs : IPacketSerializable
{
    public required long SteamId { get; set; }
    public required string Username { get; set; }
    public required BaseClass Class { get; set; }
    public required Race Race { get; set; }
    public required Sex Sex { get; set; }
    public required short Hair { get; set; }
    public required short HairColor { get; set; }
    public required short HairHighlightColor { get; set; }
    public required short SkinColor { get; set; }
    public required short EyeColor { get; set; }
    public required short Beard { get; set; }
    public required short Mustache { get; set; }
    public required short Bangs { get; set; }
}