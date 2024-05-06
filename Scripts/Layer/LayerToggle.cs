using UnityEngine;
using UnityEngine.UI; // Required for accessing UI components

public class LayerToggle : MonoBehaviour
{
    public Toggle toggle; // Assign your UI Toggle component here in the Inspector

    public GameObject totoggle; // The GameObject to be toggled


    void Start()
    {
        // Initialize the toggle state at start
        HandleToggleChanged(toggle.isOn);
        // Subscribe to toggle value changed event
        toggle.onValueChanged.AddListener(HandleToggleChanged);
    }

    void HandleToggleChanged(bool isToggled)
    {
        if (isToggled)
        {
            // Logic when the toggle is enabled
            totoggle.SetActive(true);

        }
        else
        {
            totoggle.SetActive(false);
        }
    }

    // void OnDestroy()
    // {
    //     toggle.onValueChanged.RemoveListener(HandleToggleChanged);
    // }
}






