namespace Zolian.Networking.Abstractions.Definitions;

public enum ServerType : byte
{
    Lobby = 0,
    Login = 1,
    World = 2
}

public enum PopupMessageType : byte
{
    Confirmation = 0,
    System = 3,
    Screen = 5,
    WoodenBoard = 9,
    AdminMessage = 99
}

[Flags]
public enum UpdateType : byte
{
    FullSend = 1,
    HealthManaStaminaRage = 1 << 1,
    Position = 1 << 2,
    Stats = 1 << 3,
    Elements = 1 << 4,
    Visuals = 1 << 5
}

[Flags]
public enum BaseClass : byte
{
    None = 0,
    Berserker = 1,
    Defender = 2,
    Assassin = 3,
    Cleric = 4,
    Arcanus = 5,
    Monk = 6,
    Racial = 9,
    Monster = 10,
    Quest = 11
}

[Flags]
public enum JobClass
{
    None = 0,
    Thief = 1,
    DarkKnight = 1 << 1,
    Templar = 1 << 2,
    Knight = 1 << 3,
    Ninja = 1 << 4,
    SharpShooter = 1 << 5,
    Oracle = 1 << 6,
    Bard = 1 << 7,
    Summoner = 1 << 8,
    Samurai = 1 << 9,
    ShaolinMonk = 1 << 10,
    Necromancer = 1 << 11,
    Dragoon = 1 << 12
}

[Flags]
public enum Race
{
    UnDecided = 0,
    Human = 1,
    HalfElf = 2,
    HighElf = 3,
    DarkElf = 4,
    WoodElf = 5,
    Orc = 6,
    Dwarf = 7,
    Halfling = 8,
    Dragonkin = 9,
    HalfBeast = 10,
    Merfolk = 11
}

[Flags]
public enum Sex
{
    Male = 0,
    Female = 1
}

[Flags]
public enum EntityType
{
    Player = 0,
    NPC = 1,
    Monster = 2,
    Pet = 3,
    Mount = 4,
    Summon = 5,
    Item = 6,
    Unknown = 7
}

public static class TypesResolver
{
    public static bool ServerUpdateTypeFlagIsSet(this UpdateType self, UpdateType flag) => (self & flag) == flag;
    public static bool EntityTypeFlagIsSet(this EntityType self, EntityType flag) => (self & flag) == flag;
}