using Zolian.Networking.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

public sealed record AccountListArgs : IPacketSerializable
{
    public List<PlayerSelection> Players { get; set; }

    public sealed record PlayerSelection
    {
        // Stats
        public Guid Serial { get; set; }
        public bool Disabled { get; set; }
        public string Name { get; set; }
        public uint Level { get; set; }
        public string BaseClass { get; set; }
        public string AdvClass { get; set; }
        public string Job { get; set; }
        public long Health { get; set; }
        public long Mana { get; set; }

        // Visuals
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
}