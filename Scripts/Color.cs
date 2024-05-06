using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for accessing UI components

public class ToggleColorChanger : MonoBehaviour
{
    public List<Toggle> toggles; // Assign these in the inspector
    public Color onColor = Color.green; // Color when a toggle is on
    public Color offColor = Color.red; // Color when a toggle is off

    // Start is called before the first frame update
    void Start()
    {
        // Iterate through each toggle in the list and add a listener
        foreach (var toggle in toggles)
        {
            // Add a listener to each Toggle's value changed event
            toggle.onValueChanged.AddListener(delegate { OnToggleChanged(toggle); });

            // Initialize each toggle with the correct color based on its current state
            OnToggleChanged(toggle);
        }
    }

    // This method is called whenever any Toggle's value changes
    void OnToggleChanged(Toggle toggle)
    {
        bool isOn = toggle.isOn; // Get the current state of the toggle
        ColorBlock cb = toggle.colors; // Get the current color block
        cb.normalColor =        isOn ? onColor : offColor; // Change the normal color based on the toggle state
        cb.highlightedColor =   isOn ? onColor : offColor; // Change the normal color based on the toggle state
        cb.selectedColor =      isOn ? onColor : offColor; // Change the normal color based on the toggle state       
        cb.disabledColor =      isOn ? onColor : offColor; // Change the highlighted color based on the toggle state, optional
        toggle.colors = cb; // Apply the changed color block back to the toggle
    }

    // Ensure to remove listeners when the script is destroyed
    void OnDestroy()
    {
        foreach (var toggle in toggles)
        {
            toggle.onValueChanged.RemoveListener(delegate { OnToggleChanged(toggle); });
        }
    }
}
