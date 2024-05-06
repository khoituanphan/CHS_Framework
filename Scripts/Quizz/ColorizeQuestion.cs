using System.Collections.Generic;
using UnityEngine;

public class ColorizeQuestion : MonoBehaviour
{
    [System.Serializable]
    public class TargetColorPair
    {
        public GameObject targetObject;
        // You might want to store a reference to the created sphere here if it's directly related
        public GameObject sphereInstance; // Added to keep track of the created sphere
    }

    public List<TargetColorPair> targetColorPairs;
    public GameObject spherePrefab; // Reference to the sphere prefab

    private void Start()
    {
        foreach (var pair in targetColorPairs)
        {
            if (spherePrefab != null)
            {
                Vector3 targetPosition = pair.targetObject.transform.position;
                Quaternion targetRotation = Quaternion.identity;
                GameObject sphereInstance = Instantiate(spherePrefab, targetPosition, targetRotation);
                sphereInstance.transform.SetParent(pair.targetObject.transform);

                // Store the reference to the created sphere in the pair
                pair.sphereInstance = sphereInstance;
            }
        }
    }

    // Add a method to return all created spheres for visibility control
    public List<GameObject> GetAllCreatedSpheres()
    {
        List<GameObject> spheres = new List<GameObject>();
        foreach (var pair in targetColorPairs)
        {
            if (pair.sphereInstance != null)
                spheres.Add(pair.sphereInstance);
        }
        return spheres;
    }
}
