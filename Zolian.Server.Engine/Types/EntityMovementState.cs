using System.Numerics;

using Zolian.Common;

namespace Zolian.Types;

public struct EntityMovementState
{
    public Vector3 Position;
    public Vector3 InputDirection; // XZ plane
    public Vector3 Velocity;       // Only Y is used
    public float Speed;            // Magnitude of movement
    public float CameraYaw;

    private const float Gravity = -9.81f; // ToDo: Check if this actually matches the game engine

    public void Simulate(float deltaTime)
    {
        // Rotate input to align with camera yaw
        var rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathExtensions.ToRadians(CameraYaw));
        var moveDir = Vector3.Transform(InputDirection, rotation);

        // Calculate horizontal movement
        var movement = moveDir * Speed;

        // Apply gravity to vertical velocity
        Velocity.Y += Gravity * deltaTime;

        // Final position update
        Position += new Vector3(movement.X, Velocity.Y, movement.Z) * deltaTime;

        // Ground collision detection
        if (Position.Y <= 0f)
        {
            Position.Y = 0;
            Velocity.Y = 0;
        }
    }
}
