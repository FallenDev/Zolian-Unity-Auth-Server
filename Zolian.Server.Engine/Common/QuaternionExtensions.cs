using System.Numerics;

namespace Zolian.Common;

public static class QuaternionExtensions
{
    /// <summary>
    /// Creates a rotation looking in a direction, constrained to the Y-axis.
    /// </summary>
    public static Quaternion LookRotationFlat(Vector3 forward)
    {
        forward.Y = 0;
        if (forward.LengthSquared() == 0)
            return Quaternion.Identity;

        forward = Vector3.Normalize(forward);
        return Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathF.Atan2(forward.X, forward.Z));
    }

    /// <summary>
    /// Returns the angle difference in degrees between two quaternions.
    /// </summary>
    public static float AngleTo(this Quaternion from, Quaternion to)
    {
        var dot = Quaternion.Dot(from, to);
        return MathF.Acos(MathF.Min(MathF.Abs(dot), 1f)) * 2f * (180f / MathF.PI);
    }

    /// <summary>
    /// Performs a smooth step between two rotations based on t (0-1).
    /// </summary>
    public static Quaternion SmoothLerp(this Quaternion from, Quaternion to, float t)
    {
        t = t * t * (3f - 2f * t); // SmoothStep easing
        return Quaternion.Slerp(from, to, t);
    }

    /// <summary>
    /// Converts a Y-axis-only rotation (yaw) to a quaternion.
    /// </summary>
    public static Quaternion FromYaw(float yawDegrees)
    {
        return Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathExtensions.ToRadians(yawDegrees));
    }

    /// <summary>
    /// Extracts the yaw (Y-axis rotation) in degrees from a quaternion.
    /// </summary>
    public static float ToYawDegrees(this Quaternion rotation)
    {
        // Converts Quaternion → Euler Yaw (Y axis only)
        var forward = Vector3.Transform(Vector3.UnitZ, rotation);
        return MathF.Atan2(forward.X, forward.Z) * (180f / MathF.PI);
    }
}
