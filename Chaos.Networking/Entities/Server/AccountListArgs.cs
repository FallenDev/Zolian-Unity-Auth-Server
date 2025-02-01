using Chaos.DarkAges.Definitions;
using Chaos.Packets.Abstractions;

namespace Chaos.Networking.Entities.Server;

public sealed record AccountListArgs : IPacketSerializable
{
    public List<PlayerSelection> Players { get; set; }

    public sealed record PlayerSelection
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string BaseClass { get; set; }
        public string AdvClass { get; set; }
        public string Job { get; set; }
        public long Health { get; set; }
        public long Mana { get; set; }
    }
}