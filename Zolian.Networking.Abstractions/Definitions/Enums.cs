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

public static class TypesResolver
{
    public static bool ServerUpdateTypeFlagIsSet(this UpdateType self, UpdateType flag) => (self & flag) == flag;
}