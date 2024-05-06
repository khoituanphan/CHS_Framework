using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkingHandler : MonoBehaviour
{
    public static LinkingHandler Instance; // Singleton instance
    public AnnotationToggle annotationToggle;
    void Awake()
    {
        Instance = this; // Initialize the singleton instance
    }

    // Make PlaneTargetPair a public class
    [System.Serializable]
    public class PlaneTargetPair
    {
        public Transform plane;
        public GameObject targetObject;
        public GameObject additionalObject;
        public Color color; // Desired color for the line
        public Color originalColor; // Store the original color
        public GameObject sphereInstance;
        public LineRenderer lineRenderer; // Reference to the LineRenderer component
    }
    private PlaneTargetPair currentActivePair = null;
    public List<PlaneTargetPair> planeTargetPairs;

    private ColorHandler colorHandler; // Assuming this is a class you have defined elsewhere
    public GameObject spherePrefab; // Reference to the sphere prefab
    public VisibilityHandler visibilityHandler; // Reference to the VisibilityHandler


    private void Start()
    {
        colorHandler = new ColorHandler(); // Initialize ColorHandler

        foreach (var pair in planeTargetPairs)
        {
            // Set up the target object's color
            if (pair.targetObject.GetComponent<Renderer>() != null)
            {
                pair.originalColor = pair.targetObject.GetComponent<Renderer>().material.color;
            }
            if (pair.targetObject.GetComponent<Renderer>() != null)
            {
                pair.targetObject.GetComponent<Renderer>().material.color = pair.color;
            }

            // Initially hide the plane
            pair.plane.gameObject.SetActive(false);

            // Set up and place spheres
            if (spherePrefab != null)
            {
                Vector3 targetPosition = pair.targetObject.transform.position;
                GameObject sphereInstance = Instantiate(spherePrefab, targetPosition, Quaternion.identity);
                sphereInstance.transform.SetParent(pair.targetObject.transform);

                SphereClicker clicker = sphereInstance.AddComponent<SphereClicker>();
                //clicker.Initialize(pair.plane, visibilityHandler, pair,this); // Adjusted to not pass the line reference
                pair.sphereInstance = sphereInstance; // Store the reference
            }
        }
    }

    public void ResetColorsExcept(PlaneTargetPair excludePair)
    {
        foreach (var pair in planeTargetPairs)
        {
            if (pair != excludePair)
            {
                if (pair.targetObject.GetComponent<Renderer>() != null)
                {
                    pair.targetObject.GetComponent<Renderer>().material.color = pair.originalColor;
                }
                if (pair.plane.GetComponent<Renderer>() != null)
                {
                    pair.plane.GetComponent<Renderer>().material.color = pair.originalColor;
                }
                // Optionally reset lineRenderer color if needed
            }
        }
    }

    // MeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMeshMesh
    public void HandleTargetObjectClick(PlaneTargetPair clickedPair)
    {
        // Ensure functionality only operates in case 2
        if (annotationToggle.currentState == 2)
        {
            if (currentActivePair == clickedPair && !clickedPair.plane.gameObject.activeSelf)
            {
                ResetColorsAndVisibility(clickedPair);
                currentActivePair = null;
            }
            else
            {
                if (currentActivePair != null && currentActivePair != clickedPair)
                {
                    ResetColorsExceptLine(currentActivePair);
                }
                TogglePlaneAndLine(clickedPair);
                currentActivePair = clickedPair.plane.gameObject.activeSelf ? clickedPair : null;
            }
        }
    }

    // Resets the colors to original without hiding the line
    void ResetColorsExceptLine(PlaneTargetPair pair)
    {
        if (pair.targetObject.GetComponent<Renderer>() != null)
        {
            pair.targetObject.GetComponent<Renderer>().material.color = pair.originalColor;
        }
        if (pair.plane.GetComponent<Renderer>() != null)
        {
            pair.plane.GetComponent<Renderer>().material.color = pair.originalColor;
        }
        // Do not deactivate the line here
    }

    // Use the existing methods for ResetColorsAndVisibility, ChangeColor, ShowOrSetupLine, SetupLineRenderer as defined before


    // This method toggles the visibility of the plane and the line, and changes colors accordingly
    void TogglePlaneAndLine(PlaneTargetPair pair)
    {
        pair.plane.gameObject.SetActive(!pair.plane.gameObject.activeSelf);

        if (pair.plane.gameObject.activeSelf)
        {
            // Highlight in red and show the line
            ChangeColor(pair, Color.red);
            ShowOrSetupLine(pair);
        }
        else
        {
            // Reset to original color and hide the line
            ResetColorsAndVisibility(pair);
        }
    }

    // Resets the colors of the target object and its plane to original, and hides the line
    void ResetColorsAndVisibility(PlaneTargetPair pair)
    {
        if (pair.targetObject.GetComponent<Renderer>() != null)
        {
            pair.targetObject.GetComponent<Renderer>().material.color = pair.originalColor;
        }
        if (pair.plane.GetComponent<Renderer>() != null)
        {
            pair.plane.GetComponent<Renderer>().material.color = pair.originalColor;
        }
        if (pair.lineRenderer != null)
        {
            pair.lineRenderer.gameObject.SetActive(false);
        }
    }

    // Changes the color of the plane and target object to red
    void ChangeColor(PlaneTargetPair pair, Color color)
    {
        if (pair.plane.GetComponent<Renderer>() != null)
        {
            pair.plane.GetComponent<Renderer>().material.color = color;
        }
        if (pair.targetObject.GetComponent<Renderer>() != null)
        {
            pair.targetObject.GetComponent<Renderer>().material.color = color;
        }
    }

    // Shows or sets up the line between the target object and its plane
    void ShowOrSetupLine(PlaneTargetPair pair)
    {
        if (pair.lineRenderer == null)
        {
            GameObject lineObj = new GameObject($"Line_{pair.plane.name}_to_{pair.targetObject.name}");
            pair.lineRenderer = lineObj.AddComponent<LineRenderer>();
            SetupLineRenderer(pair.lineRenderer, pair);
        }
        else
        {
            pair.lineRenderer.gameObject.SetActive(true);
        }
    }



    // Method to set up the line renderer
    private void SetupLineRenderer(LineRenderer lineRenderer, PlaneTargetPair pair)
    {
        lineRenderer.material = new Material(visibilityHandler.lineMaterial);
        lineRenderer.startWidth = visibilityHandler.lineWidth;
        lineRenderer.endWidth = visibilityHandler.lineWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, pair.plane.position);
        lineRenderer.SetPosition(1, pair.targetObject.transform.position);
        lineRenderer.useWorldSpace = true;
        lineRenderer.material.color = pair.color;
    }


    public void UpdateLinePositions(Transform planeTransform, Vector3 newPosition, float spacing)
    {
        foreach (var pair in planeTargetPairs)
        {
            if (pair.plane == planeTransform)
            {
                // Update the plane's position
                pair.plane.position = newPosition;
                // Example: move the target object away based on the spacing variable
                Vector3 directionToTarget = (pair.targetObject.transform.position - newPosition).normalized;
                pair.targetObject.transform.position = newPosition + directionToTarget * spacing;

                // Update the line positions if the line is already created
                LineRenderer lr = pair.plane.GetComponentInChildren<LineRenderer>();
                if (lr != null)
                {
                    lr.SetPosition(0, pair.plane.position);
                    lr.SetPosition(1, pair.targetObject.transform.position);
                }
            }
        }
    }
}