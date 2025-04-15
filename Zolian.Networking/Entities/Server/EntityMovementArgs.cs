using System.Numerics;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

public sealed record EntityMovementArgs : IPacketSerializable
{
    public EntityType EntityType;
    public Guid Serial;
    // Server-authoritative state
    public Vector3 Position;
    public Vector3 InputDirection;
    public float CameraYaw;
    public float Speed;
    public float VerticalVelocity;
}