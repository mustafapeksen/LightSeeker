using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightRayRenderer : MonoBehaviour
{
    LightEmitter lightEmitter;
    LineRenderer lineRenderer;
    readonly List<Vector2> reflectPoints = new List<Vector2>();
    [SerializeField]
    private Material lightMaterial;
    private void Awake()
    {
        lightEmitter = GetComponentInParent<LightEmitter>();
        lightEmitter.OnLightUpdated += ReflectLineRenderer;

        lineRenderer = GetComponent<LineRenderer>();

        CustomizeLineRenderer();
    }

    private void OnDestroy()
    {
        if (lightEmitter != null)
            lightEmitter.OnLightUpdated -= ReflectLineRenderer;
    }

    private void ReflectLineRenderer()
    {
        reflectPoints.Clear();
        reflectPoints.AddRange(lightEmitter.GetLightPoints());
        lineRenderer.positionCount = reflectPoints.Count;
        for (int i = 0; i < reflectPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, reflectPoints[i]);
        }
    }

    private void CustomizeLineRenderer()
    {
        LineColor();
        LineWidth();
    }

    private void LineWidth()
    {
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.01f;
    }

    private void LineColor()
    {

        if (lightMaterial != null)
        {
            lineRenderer.material = lightMaterial;
        }
        else
        {
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }

        Color32 light = new Color32(250, 209, 107, 255);
        Color32 lighter = new Color32(247, 233, 199, 255);

        lineRenderer.startColor = light;
        lineRenderer.endColor = lighter;
    }
}
