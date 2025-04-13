﻿namespace Zolian.Networking.Abstractions.Definitions;

public enum ClientOpCode : byte
{
    OnClientLogin = 0x01,
    EnterGame = 0x02,
    EnterWorld = 0x03,
    MovementInput = 0x04,
    Version = 0x0A,
    ClientRedirected = 0x0B,
    CreateCharacter = 0x0C,
    DeleteCharacter = 0x0D,
}

public enum ServerOpCode : byte
{
    ConnectionInfo = 0x00,
    PlayerList = 0x01,
    LoginMessage = 0x02,
    CreateCharacterFinalized = 0x03,
    CharacterData = 0x04,
    EntityMovement = 0x05,
    PlayAnimation = 0x06,
    EntityAnimation = 0x07,
    ServerMessage = 0x0A,
    AddEntity = 0x0D,
    RemoveEntity = 0x0E,
    Sound = 0x19,
    AcceptConnection = 0x7E
}