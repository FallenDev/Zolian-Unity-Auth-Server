using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Client;

/// <summary>
///     Provides packet serialization and deserialization logic for <see cref="VersionArgs" />
/// </summary>
public sealed class VersionConverter : PacketConverterBase<VersionArgs>
{
    /// <inheritdoc />
    public override byte OpCode => (byte)ClientOpCode.Version;

    /// <inheritdoc />
    public override VersionArgs Deserialize(ref SpanReader reader)
    {
        var version = reader.ReadString();

        return new VersionArgs
        {
            Version = version
        };
    }
}