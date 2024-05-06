using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for accessing UI components

public class AnnotationColor : MonoBehaviour
{
    public List<Toggle> toggles; // Assign these in the inspector
    public Color onColor = Color.green; // Color when a toggle is on
    public Color offColor = Color.red; // Color when a toggle is off

    void Start()
    {
        // Iterate through each toggle in the list and add a listener
        foreach (var toggle in toggles)
        {
            // Using a local variable to avoid the closure problem in loops
            Toggle currentToggle = toggle;
            currentToggle.onValueChanged.AddListener(isOn => OnToggleChanged(currentToggle, isOn));
            
            // Initialize each toggle with the correct color based on its current state
            UpdateToggleColor(currentToggle);
        }
    }

    // Modified to accept the isOn parameter to directly check the toggle's state
    void OnToggleChanged(Toggle changedToggle, bool isOn)
    {
        if (isOn)
        {
            // Turn off all other toggles
            foreach (var toggle in toggles)
            {
                if (toggle != changedToggle)
                {
                    toggle.isOn = false;
                    UpdateToggleColor(toggle); // Update the colors of toggles turned off
                }
            }
        }

        // Update the color of the changed toggle
        UpdateToggleColor(changedToggle);
    }

    void UpdateToggleColor(Toggle toggle)
    {
        ColorBlock cb = toggle.colors; // Get the current color block
        cb.normalColor = cb.highlightedColor = cb.selectedColor = cb.disabledColor = toggle.isOn ? onColor : offColor;
        toggle.colors = cb; // Apply the changed color block back to the toggle
    }

    void OnDestroy()
    {
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.RemoveAllListeners();
        }
    }
}
