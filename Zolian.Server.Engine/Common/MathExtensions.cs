using System.Numerics;

namespace Zolian.Common;

public static class MathExtensions
{
    public const float Pi = MathF.PI;

    public static float ToRadians(float degrees)
    {
        return degrees * (Pi / 180f);
    }

    public static float ToDegrees(float radians)
    {
        return radians * (180f / Pi);
    }

    /// <summary>
    /// Keeps rotation between -180 and 180 degrees
    /// </summary>
    public static float ClampAngle(float angle)
    {
        angle %= 360f;
        if (angle > 180f) angle -= 360f;
        if (angle < -180f) angle += 360f;
        return angle;
    }

    /// <summary>
    /// Wraps angles to 0-360 range
    /// </summary>
    public static float NormalizeAngle360(float angle)
    {
        angle %= 360f;
        return angle < 0f ? angle + 360f : angle;
    }

    /// <summary>
    /// Simple linear interpolation use for smoothing
    /// </summary>
    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }

    /// <summary>
    /// For smoothing velocity or camera movement 
    /// </summary>
    public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float deltaTime)
    {
        var omega = 2f / smoothTime;
        var x = omega * deltaTime;
        var exp = 1f / (1f + x + 0.48f * x * x + 0.235f * x * x * x);
        var change = current - target;
        var temp = (currentVelocity + omega * change) * deltaTime;
        currentVelocity = (currentVelocity - omega * temp) * exp;
        return target + (change + temp) * exp;
    }

    /// <summary>
    /// Gets a normalized XZ direction between two points
    /// </summary>
    public static Vector3 FlatDirection(Vector3 from, Vector3 to)
    {
        var dir = to - from;
        dir.Y = 0;
        return Vector3.Normalize(dir);
    }
}