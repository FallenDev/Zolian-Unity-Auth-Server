using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

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
        var hair = reader.ReadInt16();
        var hairColor = reader.ReadInt16();
        var hairHighlightColor = reader.ReadInt16();
        var skinTone = reader.ReadInt16();
        var eyeColor = reader.ReadInt16();
        var beard = reader.ReadInt16();
        var mustache = reader.ReadInt16();
        var bangs = reader.ReadInt16();

        return new CreateCharacterArgs
        {
            SteamId = id,
            Username = username,
            Class = className,
            Race = race,
            Sex = sex,
            Hair = hair,
            HairColor = hairColor,
            HairHighlightColor = hairHighlightColor,
            SkinColor = skinTone,
            EyeColor = eyeColor,
            Beard = beard,
            Mustache = mustache,
            Bangs = bangs
        };
    }
}