using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OriVisibilityHandler : MonoBehaviour
{
    public OriLinkingHandler OriLinkingHandler; // Reference to OriLinkingHandler
    public Camera mainCamera;
    public int maxVisibleObjects = 8; // Maximum number of objects to be visible at once
    private bool linesAndPlanesVisible = true; // Control flag for visibility

    private void Update()
    {
        UpdateVisibility();
    }

    // Existing Toggle method can be kept for convenience or other use cases
    // public void ToggleLinesAndPlanesVisibility()
    // {
    //     linesAndPlanesVisible = !linesAndPlanesVisible;
    //     UpdateVisibility(); // Ensure visibility is updated immediately after toggling
    // }
    public void TurnOnVisibility()
    {
        linesAndPlanesVisible = true;
        UpdateVisibility(); // Update visibility based on the new flag state
    }

    // New method to explicitly turn off visibility
    public void TurnOffVisibility()
    {
        linesAndPlanesVisible = false;
        UpdateVisibility(); // Update visibility based on the new flag state
    }

    public void UpdateVisibility()
    {
        if (OriLinkingHandler != null)
        {
            var sortedPairs = OriLinkingHandler.planeTargetPairs.OrderBy(pair => Vector3.Distance(mainCamera.transform.position, pair.plane.position)).ToList();
            List<LineRenderer> lineRenderers = FindObjectsOfType<LineRenderer>().ToList(); // Assuming each link has exactly one LineRenderer

            for (int i = 0; i < sortedPairs.Count; i++)
            {
                var pair = sortedPairs[i];
                bool shouldBeActive = i < maxVisibleObjects && linesAndPlanesVisible;

                pair.plane.gameObject.SetActive(shouldBeActive);
                if (pair.sphereInstance != null)
                {
                    pair.sphereInstance.SetActive(shouldBeActive);
                }

                if (i < lineRenderers.Count)
                {
                    lineRenderers[i].enabled = shouldBeActive;
                    if (shouldBeActive)
                    {
                        lineRenderers[i].SetPosition(0, pair.plane.position);
                        lineRenderers[i].SetPosition(1, pair.targetObject.transform.position);
                    }
                }
            }
        }
    }
}
