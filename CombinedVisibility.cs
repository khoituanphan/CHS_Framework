using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedVisibility : MonoBehaviour
{
    public CombinedHandler combinedHandler;
    public Camera mainCamera;
    public int maxVisibleObjects = 8; // Maximum number of objects to be visible at once
    private bool linesAndPlanesVisible = true; // Control flag for lines, planes, and spheres visibility

    private void Update()
    {
        // The visibility updating logic can be removed from Update if you want the change to happen only on button press
    }

    public void ToggleLinesAndPlanesVisibility()
    {
        // Toggle visibility flag
        linesAndPlanesVisible = !linesAndPlanesVisible;

        // Update visibility of all elements (lines, planes, and spheres)
        UpdateVisibility();
    }

    public void UpdateVisibility()
    {
        foreach (var pair in combinedHandler.planeTargetPairs)
        {
            // Update sphere visibility
            // if (pair.sphereInstance != null)
            // {
            //     pair.sphereInstance.SetActive(linesAndPlanesVisible);
            // }

            // Assuming each PlaneTargetPair's plane has a Renderer component to toggle visibility
            if (pair.plane.GetComponent<Renderer>() != null)
            {
                pair.plane.GetComponent<Renderer>().enabled = linesAndPlanesVisible;
            }

            // Assuming the line is a child of the plane and has a LineRenderer component
            LineRenderer lineRenderer = pair.plane.GetComponentInChildren<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.enabled = linesAndPlanesVisible;
            }
        }
    }
}
