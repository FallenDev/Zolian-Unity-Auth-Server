using System.Numerics;
using Zolian.Common;

namespace Zolian.Types
{
    public class EntityMovementState
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public bool IsGrounded;

        private const float Gravity = -9.81f;
        private const float Acceleration = 20f;
        private const float MaxSpeed = 5f;
        private const float JumpForce = 7f;
        private const float GroundLevel = 0f;

        public void Simulate(Vector3 inputDirection, float cameraYaw, float deltaTime)
        {
            // Align input with camera yaw
            var rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathExtensions.ToRadians(cameraYaw));
            var alignedInput = Vector3.Transform(inputDirection, rotation);

            // Apply acceleration
            var acceleration = alignedInput * Acceleration;
            Velocity.X += acceleration.X * deltaTime;
            Velocity.Z += acceleration.Z * deltaTime;

            // Clamp horizontal speed
            var horizontal = new Vector2(Velocity.X, Velocity.Z);
            if (horizontal.LengthSquared() > MaxSpeed * MaxSpeed)
            {
                horizontal = Vector2.Normalize(horizontal) * MaxSpeed;
                Velocity.X = horizontal.X;
                Velocity.Z = horizontal.Y;
            }

            // Apply gravity
            Velocity.Y += Gravity * deltaTime;

            // Apply velocity to position
            Position += Velocity * deltaTime;

            // Simple ground detection
            if (Position.Y <= GroundLevel)
            {
                Position.Y = GroundLevel;
                Velocity.Y = 0;
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }
    }

}
