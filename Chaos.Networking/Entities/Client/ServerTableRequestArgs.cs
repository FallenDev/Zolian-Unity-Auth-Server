using Chaos.DarkAges.Definitions;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

/// <summary>
///     Represents the serialization of the <see cref="ClientOpCode.ServerTableRequest" /> packet
/// </summary>
public sealed record ServerTableRequestArgs : IPacketSerializable
{
    /// <summary>
    ///     If specified, the id of the login server the client has chosen
    ///     <br />
    ///     Should only be used with the ServerTableRequestType.ServerId
    /// </summary>
    public byte? ServerId { get; set; }

    /// <summary>
    ///     The type of request
    /// </summary>
    public required ServerTableRequestType ServerTableRequestType { get; set; }
}