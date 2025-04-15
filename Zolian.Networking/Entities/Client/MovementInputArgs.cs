using System.Numerics;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Client;

public sealed record MovementInputArgs : IPacketSerializable
{
    public required Guid Serial;
    public required Vector3 Position;
    public required Vector3 InputDirection;
    public required float CameraYaw;
    public required float Speed;
    public required float VerticalVelocity;
}