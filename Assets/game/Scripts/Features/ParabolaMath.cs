﻿using UnityEngine;
using System;

public class ParabolaMath
{
    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -6 * height * x * x + 6 * height * x;
        var mid = Vector3.Lerp(start, end, t / 2);
        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t / 2), mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        var mid = Vector2.Lerp(start, end, t);
        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }
}