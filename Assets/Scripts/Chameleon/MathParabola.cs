using UnityEngine;
using System;

public abstract class MathParabola
{
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float time)
    {
        Func<float, float> parabolaFunc = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, time);
        var yOffset = parabolaFunc(time);

        return new Vector3(mid.x, yOffset + Mathf.Lerp(start.y, end.y, time), mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float time)
    {
        Func<float, float> parabolaFunc = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, time);
        var yOffset = parabolaFunc(time);

        return new Vector2(mid.x, yOffset + Mathf.Lerp(start.y, end.y, time));
    }
}