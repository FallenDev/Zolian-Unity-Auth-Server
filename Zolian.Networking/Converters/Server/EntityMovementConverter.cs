using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Server;

/// <summary>
/// EntityMovementConverter is responsible for serializing Player, NPC, and other moveable Entities
/// </summary>
public sealed class EntityMovementConverter : PacketConverterBase<EntityMovementArgs>
{
    public override byte OpCode => (byte)ServerOpCode.EntityMovement;

    public override EntityMovementArgs Deserialize(ref SpanReader reader) => null;

    public override void Serialize(ref SpanWriter writer, EntityMovementArgs args)
    {
        writer.WriteByte((byte)args.EntityType);
        writer.WriteGuid(args.Serial);
        writer.WriteVector3(args.Position);
        writer.WriteVector3(args.InputDirection);
        writer.WriteFloat(args.CameraYaw);
        writer.WriteFloat(args.Speed);
        writer.WriteFloat(args.VerticalVelocity);
    }
}