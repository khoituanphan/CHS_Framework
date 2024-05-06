using System.Collections.Generic;
using UnityEngine;

public class InteractiveColorizeQuestion : MonoBehaviour
{
    [System.Serializable]
    public class TargetColorPair
    {
        public GameObject targetObject;
        public Color color; // Desired color for the target object
    }

    public List<TargetColorPair> targetColorPairs;
    public GameObject spherePrefab; // Reference to the sphere prefab

    private void Start()
    {
        foreach (var pair in targetColorPairs)
        {
            // Set the color of the target object
            Renderer targetRenderer = pair.targetObject.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = pair.color;
            }

            // Instantiate a sphere at the target object's position
            if (spherePrefab != null)
            {
                Vector3 targetPosition = pair.targetObject.transform.position;
                GameObject sphereInstance = Instantiate(spherePrefab, targetPosition, Quaternion.identity);

                sphereInstance.transform.SetParent(pair.targetObject.transform);

                // Add a component to the sphere that listens for mouse clicks
                SphereClicker clicker = sphereInstance.AddComponent<SphereClicker>();
                clicker.targetName = pair.targetObject.name; // Pass the target object's name to the clicker
            }
        }
    }
}

// This script should be added to the sphere prefab or added dynamically as done above
public class SphereClicker : MonoBehaviour
{
    public string targetName; // Name of the target object

    void OnMouseDown()
    {
        // Assuming QuizManager is accessible, for example via Singleton pattern or directly set in the inspector
        FindObjectOfType<QuizManager>().ReceiveAnswer(targetName);
        Debug.Log(targetName);
    }
}