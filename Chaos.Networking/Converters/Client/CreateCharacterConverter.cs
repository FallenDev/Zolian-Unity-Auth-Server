using Chaos.DarkAges.Definitions;
using Chaos.IO.Memory;
using Chaos.Networking.Abstractions.Definitions;
using Chaos.Networking.Entities.Client;
using Chaos.Packets.Abstractions;

namespace Chaos.Networking.Converters.Client;

public sealed class CreateCharacterConverter : PacketConverterBase<CreateCharacterArgs>
{
    public override byte OpCode => (byte)ClientOpCode.OnClientLogin;

    public override CreateCharacterArgs Deserialize(ref SpanReader reader)
    {
        var id = reader.ReadInt64();
        var username = reader.ReadString();
        var className = (BaseClass)reader.ReadByte();
        var race = (BaseRace)reader.ReadByte();
        var sex = (Sex)reader.ReadByte();

        return new CreateCharacterArgs
        {
            SteamId = id,
            Username = username,
            Class = className,
            Race = race,
            Sex = sex
        };
    }
}