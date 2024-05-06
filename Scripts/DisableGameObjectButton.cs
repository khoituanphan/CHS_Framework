using UnityEngine;
using UnityEngine.UI; // Include the UI namespace

public class DisableGameObjectButton : MonoBehaviour
{
    public Button button; // Assign the UI Button in the Inspector
    public GameObject objectToDisable; // Assign the GameObject you want to disable in the Inspector

    void Start()
    {
        if (button != null)
        {
            // Register the OnClick event listener
            button.onClick.AddListener(DisableObject);
        }
        else
        {
            Debug.LogWarning("Button component not assigned.", this);
        }
    }

    private void DisableObject()
    {
        if (objectToDisable != null)
        {
            objectToDisable.SetActive(false); // Disable the assigned GameObject
            Debug.Log($"{objectToDisable.name} has been disabled.");
        }
        else
        {
            Debug.LogWarning("No GameObject assigned to disable.", this);
        }
    }

    private void OnDestroy()
    {
        // It's good practice to remove event listeners when the object is destroyed
        if (button != null)
        {
            button.onClick.RemoveListener(DisableObject);
        }
    }
}
