using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraBounds
{
    public static float GetXMin(Camera camera)
    {
        return camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane)).x;
    }

    public static float GetXMax(Camera camera)
    {
        return camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane)).x;
    }

    public static float GetYMax(Camera camera)
    {
        return camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane)).y;
    }
}