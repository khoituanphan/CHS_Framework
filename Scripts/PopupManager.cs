using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance; // Singleton instance

    private List<GameObject> allPopups = new List<GameObject>();

    private void Awake()
    {
        // Ensure there's only one instance of this manager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPopup(GameObject popup)
    {
        if (!allPopups.Contains(popup))
        {
            allPopups.Add(popup);
        }
    }

    public void TogglePopup(GameObject popup)
    {
        bool isCurrentlyActive = popup.activeSelf;

        // First, deactivate all popups
        foreach (GameObject p in allPopups)
        {
            p.SetActive(false);
        }

        // If the popup was not already active, activate it.
        if (!isCurrentlyActive)
        {
            popup.SetActive(true);
        }
    }

    // Add this method to your PopupManager
    public void HidePopup(GameObject popup)
    {
        if (popup != null)
        {
            popup.SetActive(false); // Deactivate the specified popup
        }
    }
}
