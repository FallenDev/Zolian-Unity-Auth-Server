using Chaos.IO.Memory;
using Chaos.Networking.Abstractions.Definitions;
using Chaos.Networking.Entities.Client;
using Chaos.Packets.Abstractions;

namespace Chaos.Networking.Converters.Client;

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