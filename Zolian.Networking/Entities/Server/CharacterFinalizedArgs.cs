using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

public sealed record CharacterFinalizedArgs : IPacketSerializable
{
    public bool Finalized { get; set; }
}