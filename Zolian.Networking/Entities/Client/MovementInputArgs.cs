using System.Numerics;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

public sealed record MovementInputArgs : IPacketSerializable
{
    public required Guid Serial;
    public required Vector3 MoveDirection;
    public required float CameraYaw;
}