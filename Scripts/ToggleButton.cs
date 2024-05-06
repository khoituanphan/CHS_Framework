using UnityEngine;
using UnityEngine.UI; // Required for the Toggle component

public class ToggleButton : MonoBehaviour
{
    public ToggleCounter toggleCounter; // Reference to the ToggleCounter script

    void Start()
    {
        // Get the Toggle component on this GameObject and add a listener to its value changed event
        Toggle toggle = GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(delegate {
                ToggleStateChanged(toggle);
            });
        }
        else
        {
            Debug.LogError("Toggle component not found on " + gameObject.name);
        }
    }

    // This method is called whenever the Toggle's value changes
    void ToggleStateChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            toggleCounter.IncrementCount();
        }
        else
        {
            toggleCounter.DecrementCount();
        }
    }
}
