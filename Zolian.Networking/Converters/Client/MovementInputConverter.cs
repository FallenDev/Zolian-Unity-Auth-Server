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
        var pos = reader.ReadVector3();
        var inputDir = reader.ReadVector3();
        var cameraYaw = reader.ReadFloat();
        var speed = reader.ReadFloat();
        var verticalVelocity = reader.ReadFloat();

        return new MovementInputArgs
        {
            Serial = serial,
            Position = pos,
            InputDirection = inputDir,
            CameraYaw = cameraYaw,
            Speed = speed,
            VerticalVelocity = verticalVelocity
        };
    }
}