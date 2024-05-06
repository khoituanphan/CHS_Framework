using UnityEngine;

public class Clickable : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) // Cast a ray from the camera to detect hits
            {
                // Iterate through the list of planeTargetPairs in the LinkingHandler
                foreach (var pair in LinkingHandler.Instance.planeTargetPairs)
                {
                    // Check if the hit object matches the targetObject of the current pair
                    if (hit.collider.gameObject == pair.targetObject)
                    {
                        // Handle click event here
                        LinkingHandler.Instance.HandleTargetObjectClick(pair);
                        break; // Exit the loop once we find the matching pair
                    }
                }
            }
        }
    }
}
