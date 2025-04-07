using System.Numerics;

namespace Zolian.Common;

public static class VectorExtensions
{
    /// <summary>
    /// Replaces the Y component of a Vector3.
    /// </summary>
    public static Vector3 WithY(this Vector3 vector, float newY)
    {
        return new Vector3(vector.X, newY, vector.Z);
    }

    /// <summary>
    /// Removes verticality (Y axis) from a Vector3, returning the XZ direction only.
    /// </summary>
    public static Vector3 FlattenY(this Vector3 vector)
    {
        return new Vector3(vector.X, 0f, vector.Z);
    }

    /// <summary>
    /// Removes X and Z components, keeping Y only.
    /// </summary>
    public static Vector3 FlattenXZ(this Vector3 vector)
    {
        return new Vector3(0f, vector.Y, 0f);
    }

    /// <summary>
    /// Returns true if the vectors are roughly equal within a given tolerance.
    /// </summary>
    public static bool IsRoughlyEqual(this Vector3 a, Vector3 b, float tolerance = 0.001f)
    {
        return Vector3.DistanceSquared(a, b) < tolerance * tolerance;
    }

    /// <summary>
    /// Gets a normalized XZ direction from one point to another.
    /// </summary>
    public static Vector3 FlatDirectionTo(this Vector3 from, Vector3 to)
    {
        return Vector3.Normalize((to - from).FlattenY());
    }

    /// <summary>
    /// Returns the signed angle difference between two forward vectors on the XZ plane.
    /// </summary>
    public static float SignedAngleXZ(this Vector3 from, Vector3 to)
    {
        var cross = from.X * to.Z - from.Z * to.X;
        var dot = Vector3.Dot(from.FlattenY(), to.FlattenY());
        return (float)Math.Atan2(cross, dot) * (180f / MathF.PI);
    }

    /// <summary>
    /// Projects a vector onto a plane defined by a normal.
    /// </summary>
    public static Vector3 ProjectOnPlane(this Vector3 vector, Vector3 planeNormal)
    {
        return vector - Vector3.Dot(vector, planeNormal) * planeNormal;
    }

    /// <summary>
    /// Clamps a vector’s length to a maximum magnitude.
    /// </summary>
    public static Vector3 ClampMagnitude(this Vector3 vector, float maxLength)
    {
        var lengthSquared = vector.LengthSquared();
        if (lengthSquared > maxLength * maxLength)
        {
            return Vector3.Normalize(vector) * maxLength;
        }
        return vector;
    }

    /// <summary>
    /// Checks if this direction vector is behind the reference forward vector.
    /// </summary>
    public static bool IsBehind(this Vector3 direction, Vector3 forward)
    {
        return Vector3.Dot(direction.FlattenY(), forward.FlattenY()) < 0f;
    }

    /// <summary>
    /// Checks if a target is within a cone in front of the origin.
    /// </summary>
    public static bool IsInCone(this Vector3 origin, Vector3 target, Vector3 forward, float coneAngleDegrees)
    {
        var toTarget = (target - origin).FlattenY();
        if (toTarget == Vector3.Zero)
            return true;

        var normalizedToTarget = Vector3.Normalize(toTarget);
        var normalizedForward = Vector3.Normalize(forward.FlattenY());

        var angle = MathF.Acos(Vector3.Dot(normalizedForward, normalizedToTarget)) * (180f / MathF.PI);
        return angle <= coneAngleDegrees * 0.5f;
    }
}