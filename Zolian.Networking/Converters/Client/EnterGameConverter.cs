using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Client;

public sealed class EnterGameConverter : PacketConverterBase<EnterGameArgs>
{
    public override byte OpCode => (byte)ClientOpCode.EnterGame;

    public override EnterGameArgs Deserialize(ref SpanReader reader)
    {
        var serial = reader.ReadGuid();
        var steamId = reader.ReadInt64();
        var userName = reader.ReadString();

        return new EnterGameArgs
        {
            Serial = serial,
            SteamId = steamId,
            UserName = userName
        };
    }
}