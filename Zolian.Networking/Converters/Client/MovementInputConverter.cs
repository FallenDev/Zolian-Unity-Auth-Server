using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Client;

public sealed class MovementInputConverter : PacketConverterBase<MovementInputArgs>
{
    public override byte OpCode => (byte)ClientOpCode.MovementInput;

    public override MovementInputArgs Deserialize(ref SpanReader reader)
    {
        var serial = reader.ReadGuid();
        var unPackedVector3 = reader.ReadVector3();
        var cameraYaw = reader.ReadFloat();

        return new MovementInputArgs
        {
            Serial = serial,
            MoveDirection = unPackedVector3,
            CameraYaw = cameraYaw
        };
    }
}