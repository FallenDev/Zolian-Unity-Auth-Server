using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Client;

public sealed class LoginConverter : PacketConverterBase<LoginArgs>
{
    public override byte OpCode => (byte)ClientOpCode.OnClientLogin;

    public override LoginArgs Deserialize(ref SpanReader reader)
    {
        var id = reader.ReadInt64();

        return new LoginArgs
        {
            SteamId = id
        };
    }
}