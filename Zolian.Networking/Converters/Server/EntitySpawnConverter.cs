using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Server;

/// <summary>
/// EntitySpawnConverter is responsible for instantiating Player, NPC, and other Entities that are viewable to the Player
/// </summary>
public sealed class EntitySpawnConverter : PacketConverterBase<EntitySpawnArgs>
{
    public override byte OpCode => (byte)ServerOpCode.AddEntity;

    public override EntitySpawnArgs Deserialize(ref SpanReader reader) => null;

    public override void Serialize(ref SpanWriter writer, EntitySpawnArgs args)
    {
        writer.WriteString(args.EntityType.ToString());
        writer.WriteGuid(args.Serial);
        writer.WriteVector3(args.Position);
        writer.WriteFloat(args.CameraYaw);
        writer.WriteUInt32(args.EntityLevel);
        writer.WriteInt64(args.CurrentHealth);
        writer.WriteInt64(args.MaxHealth);
        writer.WriteInt64(args.CurrentMana);
        writer.WriteInt64(args.MaxMana);

        if (args.EntityType.EntityTypeFlagIsSet(EntityType.Player))
        {
            writer.WriteString(args.UserName);
            writer.WriteString(args.Job);
            writer.WriteString(args.FirstClass);
            writer.WriteString(args.SecondClass);
            writer.WriteUInt32(args.JobLevel);
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