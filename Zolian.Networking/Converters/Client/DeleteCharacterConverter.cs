using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Client;

public sealed class DeleteCharacterConverter : PacketConverterBase<DeleteCharacterArgs>
{
    public override byte OpCode => (byte)ClientOpCode.DeleteCharacter;

    public override DeleteCharacterArgs Deserialize(ref SpanReader reader)
    {
        var id = reader.ReadInt64();
        var username = reader.ReadString();

        return new DeleteCharacterArgs
        {
            SteamId = id,
            Username = username
        };
    }
}