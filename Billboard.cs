using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Reference to the AR Camera
    public Camera arCamera;

    void Update()
    {
        if (arCamera != null)
        {
            // Rotate the plane to face the camera. Only rotate around the Y axis to keep it vertical.
            Vector3 direction = arCamera.transform.position - transform.position;

            direction.z = 90; // This ensures the rotation only happens in the X axis.
            direction.y = 90;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.LookRotation(-direction);
        }
    }
}