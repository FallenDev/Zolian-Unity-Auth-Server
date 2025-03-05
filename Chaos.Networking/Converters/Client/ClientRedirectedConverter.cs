using Zolian.Networking.Abstractions.Definitions;
using Zolian.Networking.Entities.Client;
using Zolian.Packets.Abstractions;
using Zolian.Packets.Abstractions.Memory;

namespace Zolian.Networking.Converters.Client;

/// <summary>
///     Provides packet serialization and deserialization logic for <see cref="ClientRedirectedArgs" />
/// </summary>
public sealed class ClientRedirectedConverter : PacketConverterBase<ClientRedirectedArgs>
{
    /// <inheritdoc />
    public override byte OpCode => (byte)ClientOpCode.ClientRedirected;

    /// <inheritdoc />
    public override ClientRedirectedArgs Deserialize(ref SpanReader reader)
    {
        var message = reader.ReadString();

        return new ClientRedirectedArgs
        {
            Message = message
        };
    }
}