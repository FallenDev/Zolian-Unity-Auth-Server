using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Server;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Server;

public sealed class CharacterFinalizedConverter : PacketConverterBase<CharacterFinalizedArgs>
{
    /// <inheritdoc />
    public override byte OpCode => (byte)ServerOpCode.CreateCharacterFinalized;

    /// <inheritdoc />
    public override CharacterFinalizedArgs Deserialize(ref SpanReader reader) => null;

    /// <inheritdoc />
    public override void Serialize(ref SpanWriter writer, CharacterFinalizedArgs args) => writer.WriteBoolean(true);
}