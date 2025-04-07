using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Server;

public sealed class CharacterDataConverter : PacketConverterBase<CharacterDataArgs>
{
    /// <inheritdoc />
    public override byte OpCode => (byte)ServerOpCode.CharacterData;

    /// <inheritdoc />
    public override CharacterDataArgs Deserialize(ref SpanReader reader) => null;

    /// <inheritdoc />
    public override void Serialize(ref SpanWriter writer, CharacterDataArgs args)
    {
        writer.WriteString(args.Type.ToString());

        // Full Send
        if (args.Type.ServerUpdateTypeFlagIsSet(UpdateType.FullSend))
        {
            writer.WriteGuid(args.Serial);
            writer.WriteBoolean(args.Disabled);
            writer.WriteString(args.UserName);
            writer.WriteString(args.Stage);
            writer.WriteString(args.Job);
            writer.WriteString(args.FirstClass);
            writer.WriteString(args.SecondClass);
            writer.WriteUInt32(args.EntityLevel);
            writer.WriteUInt32(args.JobLevel);
            writer.WriteBoolean(args.GameMaster);
            writer.WriteVector3(args.Position);
            writer.WriteFloat(args.CameraYaw);
            writer.WriteInt64(args.CurrentHealth);
            writer.WriteInt64(args.MaxHealth);
            writer.WriteInt64(args.CurrentMana);
            writer.WriteInt64(args.MaxMana);
            writer.WriteUInt32(args.CurrentStamina);
            writer.WriteUInt32(args.MaxStamina);
            writer.WriteUInt32(args.CurrentRage);
            writer.WriteUInt32(args.MaxRage);
            writer.WriteUInt32(args.Regen);
            writer.WriteUInt32(args.Dmg);
            writer.WriteDouble(args.Reflex);
            writer.WriteDouble(args.Fortitude);
            writer.WriteDouble(args.Will);
            writer.WriteInt32(args.ArmorClass);
            writer.WriteString(args.OffenseElement);
            writer.WriteString(args.DefenseElement);
            writer.WriteString(args.SecondaryOffenseElement);
            writer.WriteString(args.SecondaryDefenseElement);
            writer.WriteInt32(args.Str);
            writer.WriteInt32(args.Int);
            writer.WriteInt32(args.Wis);
            writer.WriteInt32(args.Con);
            writer.WriteInt32(args.Dex);
            writer.WriteInt32(args.Luck);
            writer.WriteString(args.Race.ToString());
            writer.WriteString(args.Sex.ToString());
            writer.WriteInt16(args.Hair);
            writer.WriteInt16(args.HairColor);
            writer.WriteInt16(args.HairHighlightColor);
            writer.WriteInt16(args.SkinColor);
            writer.WriteInt16(args.EyeColor);
            writer.WriteInt16(args.Beard);
            writer.WriteInt16(args.Mustache);
            writer.WriteInt16(args.Bangs);
        }

        // Health/Mana/Stamina/Rage
        if (args.Type.ServerUpdateTypeFlagIsSet(UpdateType.HealthManaStaminaRage))
        {
            writer.WriteInt64(args.CurrentHealth);
            writer.WriteInt64(args.MaxHealth);
            writer.WriteInt64(args.CurrentMana);
            writer.WriteInt64(args.MaxMana);
            writer.WriteUInt32(args.CurrentStamina);
            writer.WriteUInt32(args.MaxStamina);
            writer.WriteUInt32(args.CurrentRage);
            writer.WriteUInt32(args.MaxRage);
        }

        //Position
        if (args.Type.ServerUpdateTypeFlagIsSet(UpdateType.Position))
        {
            writer.WriteVector3(args.Position);
            writer.WriteFloat(args.CameraYaw);
        }

        // Stats
        if (args.Type.ServerUpdateTypeFlagIsSet(UpdateType.Stats))
        {
            writer.WriteUInt32(args.Regen);
            writer.WriteUInt32(args.Dmg);
            writer.WriteDouble(args.Reflex);
            writer.WriteDouble(args.Fortitude);
            writer.WriteDouble(args.Will);
            writer.WriteInt32(args.ArmorClass);
            writer.WriteInt32(args.Str);
            writer.WriteInt32(args.Int);
            writer.WriteInt32(args.Wis);
            writer.WriteInt32(args.Con);
            writer.WriteInt32(args.Dex);
            writer.WriteInt32(args.Luck);
        }

        // Elements
        if (args.Type.ServerUpdateTypeFlagIsSet(UpdateType.Elements))
        {
            writer.WriteString(args.OffenseElement);
            writer.WriteString(args.DefenseElement);
            writer.WriteString(args.SecondaryOffenseElement);
            writer.WriteString(args.SecondaryDefenseElement);
        }

        // Visuals
        if (args.Type.ServerUpdateTypeFlagIsSet(UpdateType.Visuals))
        {
            writer.WriteString(args.Race.ToString());
            writer.WriteString(args.Sex.ToString());
            writer.WriteInt16(args.Hair);
            writer.WriteInt16(args.HairColor);
            writer.WriteInt16(args.HairHighlightColor);
            writer.WriteInt16(args.SkinColor);
            writer.WriteInt16(args.EyeColor);
            writer.WriteInt16(args.Beard);
            writer.WriteInt16(args.Mustache);
            writer.WriteInt16(args.Bangs);
        }
    }
}