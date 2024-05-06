using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import this to use UI components like Toggle

public class ToggleGameObjectLinker : MonoBehaviour
{
    [System.Serializable]
    public class ToggleGameObjectPair
    {
        public Toggle toggle;
        public GameObject gameObject;
    }

    public List<ToggleGameObjectPair> toggleGameObjectPairs;

    void Start()
    {
        // Initialize all GameObjects to inactive
        foreach (var pair in toggleGameObjectPairs)
        {
            if (pair.gameObject != null)
            {
                pair.gameObject.SetActive(false);
            }
        }

        // Iterate over each pair and subscribe to the toggle's onValueChanged event
        foreach (var pair in toggleGameObjectPairs)
        {
            // Ensure the toggle and gameObject are not null
            if (pair.toggle != null && pair.gameObject != null)
            {
                // Add listener for toggle's onValueChanged
                pair.toggle.onValueChanged.AddListener(delegate {
                    ToggleValueChanged(pair.toggle);
                });

                // Set the linked GameObject active if its toggle is on
                if (pair.toggle.isOn)
                {
                    pair.gameObject.SetActive(true);
                }
            }
        }
    }

    // Method called whenever a toggle value changes
    void ToggleValueChanged(Toggle changedToggle)
    {
        // Find the pair that contains the changed toggle
        foreach (var pair in toggleGameObjectPairs)
        {
            if (pair.toggle == changedToggle && pair.gameObject != null)
            {
                // Toggle the active state of the GameObject linked to the changed toggle
                pair.gameObject.SetActive(!pair.gameObject.activeSelf);
                break; 
            }
        }
    }

}
