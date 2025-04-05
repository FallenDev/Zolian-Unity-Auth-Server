using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

public sealed record EnterWorldServerArgs : IPacketSerializable
{
    public required ushort Port { get; set; }
}