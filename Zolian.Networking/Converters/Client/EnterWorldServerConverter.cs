using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Client;

public sealed class EnterWorldServerConverter : PacketConverterBase<EnterWorldServerArgs>
{
    public override byte OpCode => (byte)ClientOpCode.EnterWorld;

    public override EnterWorldServerArgs Deserialize(ref SpanReader reader)
    {
        var port = reader.ReadUInt16();

        return new EnterWorldServerArgs
        {
            Port = port
        };
    }
}