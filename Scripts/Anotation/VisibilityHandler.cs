using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisibilityHandler : MonoBehaviour
{
    public LinkingHandler linkingHandler; // Reference to LinkingHandler
    public Camera mainCamera;
    public int maxVisibleObjects = 8; // Maximum number of objects to be visible at once
    private bool spheresVisible = true; // Control flag for visibility of spheres
    public Material lineMaterial; // Material for the line, assigned in the Inspector
    public float lineWidth = 0.001f; // Width for the line

    private void Update()
    {
        UpdateVisibility();
    }

    // Removed the toggle method since we're providing explicit control now

    public void TurnOnVisibility()
    {
        spheresVisible = true;
        UpdateVisibility();
    }

    public void TurnOffVisibility()
    {
        spheresVisible = false;
        UpdateVisibility();
    }

    public void UpdateVisibility()
    {
        if (linkingHandler != null)
        {
            var sortedPairs = linkingHandler.planeTargetPairs
                .OrderBy(pair => Vector3.Distance(mainCamera.transform.position, pair.plane.position))
                .ToList();

            for (int i = 0; i < sortedPairs.Count; i++)
            {
                var pair = sortedPairs[i];
                bool shouldBeActive = i < maxVisibleObjects && spheresVisible;

                if (pair.sphereInstance != null)
                {
                    pair.sphereInstance.SetActive(shouldBeActive);
                }
            }
        }
    }        
    public void SetInitialVisibility(GameObject plane, Vector3 position)
    {
        // Assuming that UpdateVisibility will be called after all planes are instantiated
        // We don't do anything here.
        // We'll rely on UpdateVisibility being called right after plane instantiation
    }

    // Call this method at the end of the Start method of the PlanePositionHandler script
    // To initialize the visibility based on the current state
    public void InitializeVisibility()
    {
        UpdateVisibility();
    }
}
