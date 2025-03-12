using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

public sealed record AccountListArgs : IPacketSerializable
{
    public List<PlayerSelection> Players { get; set; }

    public sealed record PlayerSelection
    {
        public Guid Serial { get; set; }
        public bool Disabled { get; set; }
        public string Name { get; set; }
        public uint Level { get; set; }
        public string BaseClass { get; set; }
        public string AdvClass { get; set; }
        public string Job { get; set; }
        public long Health { get; set; }
        public long Mana { get; set; }
    }
}


// Next steps are to pull the entity values into the database and do a full load to the server
// then send that to the client OnLogin. This will allow the client to display the character selection screen.
// The client will then send the selected character to the server and the server will load the player entity