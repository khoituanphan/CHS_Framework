using UnityEngine;

public class PlanePositionHandler : MonoBehaviour
{
    public int gridRows = 3;
    public int gridColumns = 3;
    public float spacing = 2.0f;
    public GameObject planePrefab;
    public LinkingHandler linkingHandler; // Reference to the LinkingHandler script

    void Start()
    {
        CreateAndArrangePlanes();
        // After creating all planes, you might want to update visibility or other properties
        // This can depend on what your VisibilityHandler or similar component does
        // For example, FindObjectOfType<VisibilityHandler>()?.InitializeVisibility();
    }

    void CreateAndArrangePlanes()
    {
        for (int row = 0; row < gridRows; row++)
        {
            for (int column = 0; column < gridColumns; column++)
            {
                Vector3 position = CalculatePosition(row, column);
                // Instantiate the plane but start with it deactivated
                GameObject newPlane = Instantiate(planePrefab, position, Quaternion.identity);
                newPlane.SetActive(true); // Activate or deactivate the plane as needed after instantiation
                newPlane.transform.parent = transform;

                // If the LinkingHandler is set, iterate through the planeTargetPairs to find matches
                // and update positions or other properties as needed
                if (linkingHandler != null)
                {
                    linkingHandler.UpdateLinePositions(newPlane.transform, position, spacing);
                }
            }
        }
    }

    public Vector3 CalculatePosition(int row, int column)
    {
        float xOffset = (gridColumns - 1) * spacing * 0.5f;
        float zOffset = (gridRows - 1) * spacing * 0.5f;
        return new Vector3(column * spacing - xOffset, 0, row * spacing - zOffset);
    }
}
