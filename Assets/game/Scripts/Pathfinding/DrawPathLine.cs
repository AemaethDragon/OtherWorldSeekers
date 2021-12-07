using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DrawPathLine
{
    public static void DrawLine(LineRenderer lineRenderer, Vector3[] path)
    {
        lineRenderer.positionCount = path.Length;
        lineRenderer.SetPositions(path);
    }
}
