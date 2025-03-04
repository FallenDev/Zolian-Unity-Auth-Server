using Chaos.IO.Memory;
using Chaos.Networking.Abstractions.Definitions;
using Chaos.Networking.Entities.Client;
using Chaos.Packets.Abstractions;

namespace Chaos.Networking.Converters.Client;

public sealed class DeleteCharacterConverter : PacketConverterBase<DeleteCharacterArgs>
{
    public override byte OpCode => (byte)ClientOpCode.OnClientLogin;

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