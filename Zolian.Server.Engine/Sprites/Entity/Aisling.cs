using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using Zolian.Enums;
using Zolian.Models;
using Zolian.Network.Client;
using Zolian.Network.Server;
using Zolian.Sprites.Abstractions;

namespace Zolian.Sprites.Entity;

public record KillRecord : IEqualityOperators<KillRecord, KillRecord, bool>
{
    public int TotalKills { get; set; }
    public DateTime TimeKilled { get; set; }
}

public record IgnoredRecord : IEqualityOperators<IgnoredRecord, IgnoredRecord, bool>
{
    public int Serial { get; init; }
    public string PlayerIgnored { get; init; }
}

public sealed class Aisling : Player, IAisling
{
    public WorldClient Client { get; set; }
    public bool ObjectsUpdating { get; set; }
    public readonly ConcurrentDictionary<uint, Sprite> SpritesInView = [];
    public bool GameMasterChaosCancel { get; set; }
    public int EquipmentDamageTaken = 0;
    public ConcurrentDictionary<string, KillRecord> MonsterKillCounters = [];
    public DateTime AislingTracker { get; set; }
    public int LastRingSwap { get; set; }
    public int LastHandSwap { get; set; }
    public Stopwatch LawsOfAosda { get; set; } = new();
    public bool BlessedShield;
    public uint MaximumWeight => GameMaster switch
    {
        true => 999,
        false => (uint)(Math.Round(ExpLevel / 2d) + _Str + ServerSetup.Instance.Config.WeightIncreaseModifer + 200)
    };

    public bool Overburden => CurrentWeight > MaximumWeight;
    public byte MeleeBodyAnimation;
    public byte CastBodyAnimation;
    public bool RegenTimerDisabled;
    public int GroupId { get; set; }
    public string SpellTrainOne;
    public string SpellTrainTwo;
    public string SpellTrainThree;
    public int AreaId => CurrentMapId;

    public Aisling()
    {

    }

    public bool Loading { get; set; }
    public long DamageCounter { get; set; }
    public long ThreatMeter { get; set; }
    public NameDisplayStyle NameStyle { get; set; }
    public ElementManager.Element TempOffensiveHold { get; set; }
    public ElementManager.Element TempDefensiveHold { get; set; }
    public bool IsCastingSpell { get; set; }
    public bool ProfileOpen { get; set; }
    public bool UsingTwoHanded { get; set; }
    public int LastMapId { get; set; }
    public Mail MailFlags { get; set; }
    public string ActionUsed { get; set; }
    public bool ThrewHealingPot { get; set; }
    public byte[] PictureData { get; set; }
    public ComboScroll ComboManager { get; set; }
    public Quests QuestManager { get; set; }
    public List<int> DiscoveredMaps { get; set; }
    public int Styling { get; set; }
    public int Coloring { get; set; }
    public byte OldColor { get; set; }
    public byte OldStyle { get; set; }
    public List<string> IgnoredList { get; set; }
    public ConcurrentDictionary<string, string> ExplorePositions { get; set; }
    public Vector2 DeathLocation { get; set; }
    public int DeathMapId { get; set; }
    public bool HasteFlag { get; set; }
    public bool Lycanisim => Afflictions.AfflictionFlagIsSet(Afflictions.Lycanisim);
    public bool Vampirisim => Afflictions.AfflictionFlagIsSet(Afflictions.Vampirisim);
}