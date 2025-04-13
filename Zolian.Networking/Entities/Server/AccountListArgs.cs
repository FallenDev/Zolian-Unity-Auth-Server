using Zolian.Networking.Abstractions.Definitions;
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
        public Race Race { get; set; }
        public Sex Sex { get; set; }
        public short Hair { get; set; }
        public short HairColor { get; set; }
        public short HairHighlightColor { get; set; }
        public short SkinColor { get; set; }
        public short EyeColor { get; set; }
        public short Beard { get; set; }
        public short Mustache { get; set; }
        public short Bangs { get; set; }
    }
}