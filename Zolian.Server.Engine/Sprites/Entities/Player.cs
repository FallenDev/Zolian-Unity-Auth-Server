using Zolian.Enums;
using Zolian.Networking.Abstractions.Definitions;

namespace Zolian.Sprites.Entities;

/// <summary>
/// Represents a player entity in the game.
/// </summary>
public class Player : Damageable
{
    public long SteamId { get; set; }
    public bool Disabled { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastLogged { get; set; }
    public string UserName { get; set; }
    public ClassStage Stage { get; set; }
    public Job Job { get; set; }
    public uint JobLevel { get; set; }
    public BaseClass FirstClass { get; set; }
    public BaseClass SecondClass { get; set; }
    public bool GameMaster { get; set; }
    
    // Character Customized Looks
    public Race Race { get; set; }
    public Sex Gender { get; set; }
    public short Hair { get; set; }
    public short HairColor { get; set; }
    public short HairHighlightColor { get; set; }
    public short SkinColor { get; set; }
    public short EyeColor { get; set; }
    public short Beard { get; set; }
    public short Mustache { get; set; }
    public short Bangs { get; set; }

    public void SyncFromMovementState()
    {
        Position = MovementState.Position;
        InputDirection = MovementState.InputDirection;
        Velocity = MovementState.Velocity;
        Speed = MovementState.Speed;
        CameraYaw = MovementState.CameraYaw;
    }
}

public class Equipment
{

}

public abstract class ComboScroll
{
    public string Combo1 { get; set; } = string.Empty;
    public string Combo2 { get; set; } = string.Empty;
    public string Combo3 { get; set; } = string.Empty;
    public string Combo4 { get; set; } = string.Empty;
    public string Combo5 { get; set; } = string.Empty;
    public string Combo6 { get; set; } = string.Empty;
    public string Combo7 { get; set; } = string.Empty;
    public string Combo8 { get; set; } = string.Empty;
    public string Combo9 { get; set; } = string.Empty;
    public string Combo10 { get; set; } = string.Empty;
    public string Combo11 { get; set; } = string.Empty;
    public string Combo12 { get; set; } = string.Empty;
    public string Combo13 { get; set; } = string.Empty;
    public string Combo14 { get; set; } = string.Empty;
    public string Combo15 { get; set; } = string.Empty;
}

public abstract class Quests
{
    public int MailBoxNumber { get; set; }
    public bool TutorialCompleted { get; set; }
    public bool BetaReset { get; set; }
    public int BlackSmithing { get; set; }
    public string BlackSmithingTier { get; set; } = "Novice";
    public int ArmorSmithing { get; set; }
    public string ArmorSmithingTier { get; set; } = "Novice";
    public int JewelCrafting { get; set; }
    public string JewelCraftingTier { get; set; } = "Novice";
    public int StoneSmithing { get; set; }
    public string StoneSmithingTier { get; set; } = "Novice";
    public int MilethReputation { get; set; }
    public int AbelReputation { get; set; }
    public int RucesionReputation { get; set; }
    public int SuomiReputation { get; set; }
    public int RionnagReputation { get; set; }
    public int OrenReputation { get; set; }
    public int PietReputation { get; set; }
    public int LouresReputation { get; set; }
    public int UndineReputation { get; set; }
    public int TagorReputation { get; set; }
    public int ThievesGuildReputation { get; set; }
    public int AssassinsGuildReputation { get; set; }
    public int AdventuresGuildReputation { get; set; }
    public int ArtursGift { get; set; }
    public bool CamilleGreetingComplete { get; set; }
    public bool ConnPotions { get; set; }
    public bool CryptTerror { get; set; }
    public bool CryptTerrorSlayed { get; set; }
    public bool CryptTerrorContinued { get; set; }
    public bool CryptTerrorContSlayed { get; set; }
    public bool NightTerror { get; set; }
    public bool NightTerrorSlayed { get; set; }
    public bool DreamWalking { get; set; }
    public bool DreamWalkingSlayed { get; set; }
    public int Dar { get; set; }
    public string DarItem { get; set; } = string.Empty;
    public bool ReleasedTodesbaum { get; set; }
    public bool DrunkenHabit { get; set; }
    public bool EternalLove { get; set; }
    public bool EternalLoveStarted { get; set; }
    public bool UnhappyEnding { get; set; }
    public bool HonoringTheFallen { get; set; }
    public bool ReadTheFallenNotes { get; set; }
    public bool GivenTarnishedBreastplate { get; set; }
    public bool FionaDance { get; set; }
    public int Keela { get; set; }
    public int KeelaCount { get; set; }
    public string KeelaKill { get; set; } = string.Empty;
    public bool KeelaQuesting { get; set; }
    public bool KillerBee { get; set; }
    public int Neal { get; set; }
    public int NealCount { get; set; }
    public string NealKill { get; set; } = string.Empty;
    public bool AbelShopAccess { get; set; }
    public int PeteKill { get; set; }
    public bool PeteComplete { get; set; }
    public bool SwampAccess { get; set; }
    public int SwampCount { get; set; }
    public bool TagorDungeonAccess { get; set; }
    public int Lau { get; set; }
    public string BeltDegree { get; set; } = string.Empty;
    public string BeltQuest { get; set; } = string.Empty;
    public bool SavedChristmas { get; set; }
    public bool RescuedReindeer { get; set; }
    public bool YetiKilled { get; set; }
    public bool UnknownStart { get; set; }
    public bool PirateShipAccess { get; set; }
    public bool ScubaSchematics { get; set; }
    public bool ScubaMaterialsQuest { get; set; }
    public bool ScubaGearCrafted { get; set; }
    public string EternalBond { get; set; } = string.Empty;
    public bool ArmorCraftingCodex { get; set; }
    public bool ArmorApothecaryAccepted { get; set; }
    public bool ArmorCodexDeciphered { get; set; }
    public bool ArmorCraftingCodexLearned { get; set; }
    public bool ArmorCraftingAdvancedCodexLearned { get; set; }
    public string CthonicKillTarget { get; set; } = string.Empty;
    public string CthonicFindTarget { get; set; } = string.Empty;
    public int CthonicKillCompletions { get; set; }
    public bool CthonicCleansingOne { get; set; }
    public bool CthonicCleansingTwo { get; set; }
    public bool CthonicDepthsCleansing { get; set; }
    public bool CthonicRuinsAccess { get; set; }
    public int CthonicRemainsExplorationLevel { get; set; }
    public bool EndedOmegasRein { get; set; }
    public bool CraftedMoonArmor { get; set; }
}