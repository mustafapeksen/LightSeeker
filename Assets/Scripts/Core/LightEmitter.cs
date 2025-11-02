using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEmitter : MonoBehaviour
{
    Vector2 currentPos;
    Vector2 direction;
    [SerializeField]
    List<Vector2> points = new List<Vector2>();
    int maxBounce = 5;
    RaycastHit2D hit;
    float lightRange = 50f;
    [SerializeField]
    LayerMask reflectionMask;
    [SerializeField]
    ReflectiveSurface reflectiveSurface;
    public event System.Action OnLightUpdated;

    private void Update()
    {
        EmitLight();
    }

    void EmitLight()
    {
        points.Clear();
        currentPos = transform.position;
        points.Add(currentPos);

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = (mouseWorld - (Vector2)transform.position).normalized;

        CalculateLightPath();
    }

    private void CalculateLightPath()
    {
        for (int i = 0; i <= maxBounce; i++)
        {
            hit = Physics2D.Raycast(currentPos, direction, lightRange, reflectionMask);

            if (hit.collider != null)
            {
                points.Add(hit.point);
                reflectiveSurface = hit.collider.GetComponent<ReflectiveSurface>();
                if (reflectiveSurface != null)
                {
                    direction = reflectiveSurface.GetReflectedDirection(direction, hit.normal);
                }
                currentPos = hit.point + direction * 0.01f;
            }
            else
            {
                points.Add(currentPos + direction * lightRange);
                break;
            }
        }
        OnLightUpdated?.Invoke();
    }

    public List<Vector2> GetLightPoints()
    {
        return points;
    }
}
