using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Server;

/// <summary>
///     Provides serialization and deserialization logic for <see cref="LoginMessageArgs" />
/// </summary>
public sealed class LoginMessageConverter : PacketConverterBase<LoginMessageArgs>
{
    /// <inheritdoc />
    public override byte OpCode => (byte)ServerOpCode.LoginMessage;

    /// <inheritdoc />
    public override LoginMessageArgs Deserialize(ref SpanReader reader) => null;

    /// <inheritdoc />
    public override void Serialize(ref SpanWriter writer, LoginMessageArgs args)
    {
        writer.WriteByte((byte)args.LoginMessageType);
        writer.WriteString(args.Message);
    }
}