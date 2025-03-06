using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Server;

/// <summary>
///     Provides serialization and deserialization logic for <see cref="ServerMessageArgs" />
/// </summary>
public sealed class ServerMessageConverter : PacketConverterBase<ServerMessageArgs>
{
    /// <inheritdoc />
    public override byte OpCode => (byte)ServerOpCode.ServerMessage;

    /// <inheritdoc />
    public override ServerMessageArgs Deserialize(ref SpanReader reader) => null;

    /// <inheritdoc />
    public override void Serialize(ref SpanWriter writer, ServerMessageArgs args)
    {
        writer.WriteByte((byte)args.ServerMessageType);
        writer.WriteString(args.Message);
    }
}