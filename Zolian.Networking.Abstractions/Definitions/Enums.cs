namespace Zolian.Networking.Abstractions.Definitions;

public enum ServerType : byte
{
    Lobby = 0,
    Login = 1,
    World = 2
}

public enum LoginMessageType : byte
{
    Confirm = 0,
    WrongPassword = 1,
    CheckName = 2,
    CheckPassword = 3
}

public enum ServerMessageType : byte
{
    Whisper = 0,
    Informational = 3,
    Popup = 5,
    WoodenBoard = 9,
    AdminMessage = 99
}