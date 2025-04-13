using System.Numerics;
using Zolian.Networking.Abstractions.Definitions;
using Zolian.Packets.Abstractions;

namespace Zolian.Networking.Entities.Server;

public sealed record EntityMovementArgs : IPacketSerializable
{
    public EntityType EntityType;
    public Guid Serial;
    public Vector3 Position;
    public float CameraYaw;
}