using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonlinker : MonoBehaviour
{
    public List<GameObject> objectsToToggle = new List<GameObject>(); // Assign these in the inspector

    // This method toggles the active state of all GameObjects in the list
    public void ToggleActiveState()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null)
            {
                // Toggle the active state
                obj.SetActive(!obj.activeSelf);
            }
        }

        // Debug log to show when the button is pressed
        Debug.Log("Button pressed. Toggled active state of linked objects.");
    }
}
