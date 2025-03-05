using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

/// <summary>
///     Represents the serialization of the <see cref="ServerOpCode.WorldList" /> packet
/// </summary>
public sealed record WorldListArgs : IPacketSerializable
{
    /// <summary>
    ///     A collection of information about characters in the current country
    /// </summary>
    public ICollection<WorldListMemberInfo> CountryList { get; set; } = [];

    /// <summary>
    ///     The number of players in the world
    /// </summary>
    public ushort WorldMemberCount { get; set; }
}