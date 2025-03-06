using Darkages.Enums;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Darkages.Sprites;

public abstract class Sprite : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public readonly Stopwatch MonsterBuffAndDebuffStopWatch = new();
    private readonly Stopwatch _threatControl = new();
    private readonly Lock _aislingsNearbyLock = new();
    private readonly Lock _aislingsEarShotNearbyLock = new();
    private readonly Lock _aislingsOnMapLock = new();
    private readonly Lock _monstersNearbyLock = new();
    private readonly Lock _monstersOnMapLock = new();
    private readonly Lock _mundanesNearbyLock = new();
    private readonly Lock _spritesNearbyLock = new();
    private readonly Lock _spritesWithinRangeLock = new();
    private readonly Lock _getSpritesLock = new();
    private readonly Lock _getAislingDamageLock = new();
    private readonly Lock _getMovableLock = new();

    public bool Alive => CurrentHp > 1;
    public bool Summoned;
    private long CheckHp => Math.Clamp(BaseHp + BonusHp, 0, long.MaxValue);
    public long MaximumHp => Math.Clamp(CheckHp, 0, long.MaxValue);
    private long CheckMp => Math.Clamp(BaseMp + BonusMp, 0, long.MaxValue);
    public long MaximumMp => Math.Clamp(CheckMp, 0, long.MaxValue);
    public int Regen => (_Regen + BonusRegen);
    public int Dmg => _Dmg + BonusDmg;
    public double SealedModifier { get; set; }
    public int SealedAc
    {
        get
        {
            switch (Ac)
            {
                case > 0:
                    if (SealedModifier == 0) return Ac;
                    return (int)(Ac * SealedModifier);
                case <= 0:
                    if (SealedModifier == 0) return Ac;
                    return Ac - (int)(Math.Abs(Ac) * SealedModifier);
            }
        }
    }

    private int AcFromDex => (Dex / 15);
    private int Ac => (_ac + BonusAc + AcFromDex);
    private double _fortitude => (Con * 0.1);
    public double Fortitude => Math.Min(Math.Round(_fortitude + BonusFortitude, 2), 85);
    private double _reflex => (Hit * 0.1);
    public double Reflex => Math.Min(Math.Round(_reflex, 2), 70);
    private double _will => (Mr * 0.14);
    public double Will => Math.Min(Math.Round(_will, 2), 80);
    public int Hit => _Hit + BonusHit;
    private int Mr => _Mr + BonusMr;
    public int Str => (_Str + BonusStr);
    public int Int => (_Int + BonusInt);
    public int Wis => (_Wis + BonusWis);
    public int Con => (_Con + BonusCon);
    public int Dex => (_Dex + BonusDex);
    public int Luck => _Luck + BonusLuck;

    protected Sprite() { }

    public uint Serial { get; set; }
    public int CurrentMapId { get; set; }
    public double Amplified { get; set; }
    public ElementManager.Element OffenseElement { get; set; }
    public ElementManager.Element SecondaryOffensiveElement { get; set; }
    public ElementManager.Element DefenseElement { get; set; }
    public ElementManager.Element SecondaryDefensiveElement { get; set; }

    #region Stats

    public long CurrentHp { get; set; }
    public long BaseHp { get; set; }
    public long BonusHp { get; set; }

    public long CurrentMp { get; set; }
    public long BaseMp { get; set; }
    public long BonusMp { get; set; }

    public int _Regen { get; set; }
    public int BonusRegen { get; set; }

    public int _Dmg { get; set; }
    public int BonusDmg { get; set; }

    public int BonusAc { get; set; }
    public int _ac { get; set; }

    public int BonusFortitude { get; set; }

    public int _Hit { get; set; }
    public int BonusHit { get; set; }

    public int _Mr { get; set; }
    public int BonusMr { get; set; }

    public int _Str { get; set; }
    public int BonusStr { get; set; }

    public int _Int { get; set; }
    public int BonusInt { get; set; }

    public int _Wis { get; set; }
    public int BonusWis { get; set; }

    public int _Con { get; set; }
    public int BonusCon { get; set; }

    public int _Dex { get; set; }
    public int BonusDex { get; set; }

    public int _Luck { get; set; }
    public int BonusLuck { get; set; }

    #endregion
    
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}