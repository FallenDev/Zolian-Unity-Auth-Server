using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;
using BaseClass = Chaos.DarkAges.Definitions.BaseClass;

namespace Zolian.Networking.Converters.Client;

public sealed class CreateCharacterConverter : PacketConverterBase<CreateCharacterArgs>
{
    public override byte OpCode => (byte)ClientOpCode.OnClientLogin;

    public override CreateCharacterArgs Deserialize(ref SpanReader reader)
    {
        var id = reader.ReadInt64();
        var username = reader.ReadString();
        var className = (BaseClass)reader.ReadByte();
        var race = (Race)reader.ReadByte();
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