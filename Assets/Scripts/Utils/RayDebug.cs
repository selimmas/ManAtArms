using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayDebug 
{
    public static void DrawRay(Vector3 origin, Vector3 direction, Color color)
    {
        origin.y += .25f;

        Debug.DrawRay(origin, direction, color);
    }
}
