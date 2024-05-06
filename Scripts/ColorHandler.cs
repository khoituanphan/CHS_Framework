using UnityEngine;

public class ColorHandler
{
    // Update the color of the LineRenderer and the materials of the plane and target object
    public void UpdateColor(LineRenderer lineRenderer, Transform plane, GameObject targetObject, Color newColor)
    {


        // Update plane material color
        Renderer planeRenderer = plane.GetComponent<Renderer>();
        if (planeRenderer != null)
        {
            planeRenderer.material.color = newColor;
            Debug.Log($"Updating colors: LineRenderer of {targetObject.name} to color {newColor}");
            // Update line color
            lineRenderer.startColor = newColor;
            lineRenderer.endColor = newColor;
        }

        // Update target object material color
        Renderer targetRenderer = targetObject.GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            targetRenderer.material.color = newColor;
        }
    }
}
