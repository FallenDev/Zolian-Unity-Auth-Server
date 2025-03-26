using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Server;

public sealed class PlayerListConverter : PacketConverterBase<AccountListArgs>
{
    /// <inheritdoc />
    public override byte OpCode => (byte)ServerOpCode.PlayerList;

    /// <inheritdoc />
    public override AccountListArgs Deserialize(ref SpanReader reader) => null;

    /// <inheritdoc />
    public override void Serialize(ref SpanWriter writer, AccountListArgs args)
    {
        writer.WriteByte((byte)args.Players.Count);

        foreach (var player in args.Players)
        {
            writer.WriteGuid(player.Serial);
            writer.WriteBoolean(player.Disabled);
            writer.WriteString(player.Name);
            writer.WriteUInt32(player.Level);
            writer.WriteString(player.BaseClass);
            writer.WriteString(player.AdvClass);
            writer.WriteString(player.Job);
            writer.WriteInt64(player.Health);
            writer.WriteInt64(player.Mana);
            // Visuals
            writer.WriteString(player.Race.ToString());
            writer.WriteString(player.Sex.ToString());
            writer.WriteInt16(player.Hair);
            writer.WriteInt16(player.HairColor);
            writer.WriteInt16(player.HairHighlightColor);
            writer.WriteInt16(player.SkinColor);
            writer.WriteInt16(player.EyeColor);
            writer.WriteInt16(player.Beard);
            writer.WriteInt16(player.Mustache);
            writer.WriteInt16(player.Bangs);
        }
    }
}