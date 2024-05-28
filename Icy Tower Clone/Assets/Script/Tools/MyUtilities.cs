using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class MyUtilities 
{
    public static float DistanceVector3(Vector3 a, Vector3 b)
    {
        return (a - b).sqrMagnitude;
    }

    public static float DistanceVector2(Vector2 a, Vector2 b)
    {
        return (a - b).sqrMagnitude;
    }

    public static float Square(float a)
    {
        return a * a;
    }
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }
}
