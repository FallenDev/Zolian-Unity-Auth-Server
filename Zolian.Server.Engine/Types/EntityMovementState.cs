using System.Numerics;

namespace Zolian.Types;

public struct EntityMovementState
{
    public Vector3 Position;
    public Vector3 InputDirection;
    public Vector3 Velocity;
    public float Speed;
    public float CameraYaw;
}
