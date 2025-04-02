using System.Numerics;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

public sealed record CharacterDataArgs : IPacketSerializable
{
    public UpdateType Type { get; set; }
    // Main
    public Guid Serial { get; set; }
    public bool Disabled { get; set; }
    public string UserName { get; set; }
    public string Stage { get; set; }
    public string Job { get; set; }
    public string FirstClass { get; set; }
    public string SecondClass { get; set; }
    public uint EntityLevel { get; set; }
    public uint JobLevel { get; set; }
    public bool GameMaster { get; set; }
    public Vector3 Position { get; set; }

    // Stats
    public long CurrentHealth { get; set; }
    public long MaxHealth { get; set; }
    public long CurrentMana { get; set; }
    public long MaxMana { get; set; }
    public uint CurrentStamina { get; set; }
    public uint MaxStamina { get; set; }
    public uint CurrentRage { get; set; }
    public uint MaxRage { get; set; }
    public uint Regen { get; set; }
    public uint Dmg { get; set; }
    public double Reflex { get; set; }
    public double Fortitude { get; set; }
    public double Will { get; set; }
    public int ArmorClass { get; set; }
    public string OffenseElement { get; set; }
    public string DefenseElement { get; set; }
    public string SecondaryOffenseElement { get; set; }
    public string SecondaryDefenseElement { get; set; }
    public int Str { get; set; }
    public int Int { get; set; }
    public int Wis { get; set; }
    public int Con { get; set; }
    public int Dex { get; set; }
    public int Luck { get; set; }

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