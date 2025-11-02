using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectiveSurface : MonoBehaviour
{
    float reflectionMultiplier = 1f;

    public Vector2 GetReflectedDirection(Vector2 incoming, Vector2 normal)
    {
        return Vector2.Reflect(incoming, normal) * reflectionMultiplier;
    }

    public enum SurfaceType
    {
        Reflective,   // Aynalar vb.
        Refractive,   // Cam, su vb.
        Absorbing,    // Iþýðý yutan yüzey
        Diffuse       // Daðýnýk yansýtan yüzey
    }
}
