namespace Zolian.Networking.Abstractions.Definitions;

public enum ServerType : byte
{
    Lobby = 0,
    Login = 1,
    World = 2
}

public enum PopupMessageType : byte
{
    Login = 0,
    System = 3,
    Screen = 5,
    WoodenBoard = 9,
    AdminMessage = 99
}