using UnityEngine;

public class Planebutton : MonoBehaviour
{
    public GameObject popup; // Drag your Popup GameObject here through the Unity Editor

    void Start()
    {
        // Register this popup with the manager
        PopupManager.Instance.RegisterPopup(popup);
    }

    void OnMouseDown()
    {
        if (popup != null)
        {
            PopupManager.Instance.TogglePopup(popup);
        }
        else
        {
            Debug.LogWarning("Popup GameObject is not assigned in the inspector.");
        }
    }
}
