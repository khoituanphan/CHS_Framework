using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; 

public class ToggleManager : MonoBehaviour
{
    public GameObject scrollObject; 
    public List<Toggle> toggles; 
    public List<GameObject> toggleObjects; 

    void Update()
    {
        
        if (!scrollObject.activeSelf)
        {
            DeactivateAllToggles();
        }
    }

    private void DeactivateAllToggles()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i] != null)
            {
                toggles[i].isOn = false; 
                if (i < toggleObjects.Count && toggleObjects[i] != null)
                {
                    toggleObjects[i].SetActive(false); 
                }
            }
        }
    }
}
