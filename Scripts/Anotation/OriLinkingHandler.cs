using System.Collections.Generic;
using UnityEngine;

public class OriLinkingHandler : MonoBehaviour
{
    [System.Serializable]
    public class PlaneTargetPair
    {
        public Transform plane;
        public GameObject targetObject;
        public GameObject additionalObject; // New field for additional game object
        public Color color; // Desired color for the line

        public GameObject sphereInstance;
    }

    public List<PlaneTargetPair> planeTargetPairs;
    public Material lineMaterial; // Material for the line, assigned in the Inspector
    public float lineWidth = 0.1f; // Width for the line
    private ColorHandler colorHandler; // Add this line

    public GameObject spherePrefab; // Reference to the sphere prefab

    private void Start()
    {
        colorHandler = new ColorHandler(); // Initialize ColorHandler

        foreach (var pair in planeTargetPairs)
        {
            // Create a new GameObject for the line
            GameObject lineObj = new GameObject($"Line_{pair.plane.name}_to_{pair.targetObject.name}");
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();

            // Set up the LineRenderer component with the color and width
            lr.material = new Material(lineMaterial);
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, pair.plane.position);
            lr.SetPosition(1, pair.targetObject.transform.position);
            lr.useWorldSpace = true;

            // Use the ColorHandler to apply colors to the line, plane, and target object
            colorHandler.UpdateColor(lr, pair.plane, pair.targetObject, pair.color);
            lr.material.color = pair.color;

            // Colorize additional object with the chosen color

            Renderer additionalRenderer = pair.additionalObject.GetComponent<Renderer>();

            additionalRenderer.material.color = pair.color;

            if (spherePrefab != null)
            {
                Vector3 targetPosition = pair.targetObject.transform.position;
                Quaternion targetRotation = Quaternion.identity;

                GameObject sphereInstance = Instantiate(spherePrefab, targetPosition, targetRotation);
                
                sphereInstance.transform.SetParent(pair.targetObject.transform);

                SphereClicker clicker = sphereInstance.AddComponent<SphereClicker>();
                clicker.targetName = pair.targetObject.name; // Pass the target object's name to the clicker
                pair.sphereInstance = sphereInstance; // Store the reference
                
            }
        }

        
    }
    public class SphereClicker : MonoBehaviour
{
    public string targetName; // Name of the target object

    void OnMouseDown()
    {
        Debug.Log(targetName);
    }
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