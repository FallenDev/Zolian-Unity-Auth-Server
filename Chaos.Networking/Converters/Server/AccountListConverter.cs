using Chaos.IO.Memory;
using Chaos.Networking.Abstractions.Definitions;
using Chaos.Networking.Entities.Server;
using Chaos.Packets.Abstractions;

namespace Chaos.Networking.Converters.Server;

public sealed class AccountListConverter : PacketConverterBase<AccountListArgs>
{
    /// <inheritdoc />
    public override byte OpCode => (byte)ServerOpCode.AccountList;

    /// <inheritdoc />
    public override AccountListArgs Deserialize(ref SpanReader reader) => null;

    /// <inheritdoc />
    public override void Serialize(ref SpanWriter writer, AccountListArgs args)
    {
        writer.WriteByte((byte)args.Players.Count);

        foreach (var player in args.Players)
        {
            writer.WriteString(player.Name);
            writer.WriteInt32(player.Level);
            writer.WriteString(player.BaseClass.ToString());
            writer.WriteString(player.AdvClass.ToString());
            writer.WriteString(player.Job.ToString());
            writer.WriteInt64(player.Health);
            writer.WriteInt64(player.Mana);
        }
    }
}