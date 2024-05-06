using System.Collections.Generic;
using UnityEngine;

public class CombinedHandler : MonoBehaviour
{
    [System.Serializable]
    public class PlaneTargetPair
    {
        public Transform plane;
        public GameObject targetObject;
        public GameObject additionalObject; // Field for an additional game object
        public Color color; // Desired color for the line
        public GameObject sphereInstance; // To keep track of the created sphere
    }

    public List<PlaneTargetPair> planeTargetPairs;
    public Material lineMaterial; // Material for the line, assigned in the Inspector
    public GameObject spherePrefab; // Reference to the sphere prefab
    public float lineWidth = 0.1f; // Width for the line

    private ColorHandler colorHandler; // Assume this is a class you have defined elsewhere

    private void Start()
    {
        colorHandler = new ColorHandler(); // Initialize ColorHandler

        foreach (var pair in planeTargetPairs)
        {
            // Handle line creation and coloring
            GameObject lineObj = new GameObject($"Line_{pair.plane.name}_to_{pair.targetObject.name}");
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();

            lr.material = new Material(lineMaterial);
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, pair.plane.position);
            lr.SetPosition(1, pair.targetObject.transform.position);
            lr.useWorldSpace = true;

            colorHandler.UpdateColor(lr, pair.plane, pair.targetObject, pair.color); // Assuming this method colors the line and objects
            lr.material.color = pair.color;

            // Colorize and handle additional object
                Renderer additionalRenderer = pair.additionalObject.GetComponent<Renderer>();

                    additionalRenderer.material.color = pair.color;


            // Sphere creation and positioning
            if (spherePrefab != null)
            {
                Vector3 targetPosition = pair.targetObject.transform.position;
                Quaternion targetRotation = Quaternion.identity;

                GameObject sphereInstance = Instantiate(spherePrefab, targetPosition, targetRotation);
                
                sphereInstance.transform.SetParent(pair.targetObject.transform);

                SphereClicker clicker = sphereInstance.AddComponent<SphereClicker>();
                clicker.targetName = pair.targetObject.name; // Pass the target object's name to the clicker

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
                pair.plane.position = newPosition;
                Vector3 directionToTarget = (pair.targetObject.transform.position - newPosition).normalized;
                pair.targetObject.transform.position = newPosition + directionToTarget * spacing;

                LineRenderer lr = pair.plane.GetComponentInChildren<LineRenderer>();
                if (lr != null)
                {
                    lr.SetPosition(0, pair.plane.position);
                    lr.SetPosition(1, pair.targetObject.transform.position);
                }
            }
        }
    }

    // Method to return all created spheres for visibility control
    public List<GameObject> GetAllCreatedSpheres()
    {
        List<GameObject> spheres = new List<GameObject>();
        foreach (var pair in planeTargetPairs)
        {
            if (pair.sphereInstance != null)
                spheres.Add(pair.sphereInstance);
        }
        return spheres;
    }
}