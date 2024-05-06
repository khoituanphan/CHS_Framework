using UnityEngine;
using UnityEngine.UI; // Include to work with UI elements, such as Text, Toggle, and Button

public class ToggleCounter : MonoBehaviour
{
    public Text counterDisplay; // Assign in the inspector
    public Button playButton; // Assign the button in the inspector that you want to make interactable/uninteractable
    public Button annotationButton;
    private int activeToggleCount = 0;
    // public GameObject selectedGameObject; // The GameObject to be toggled
    // public GameObject selectedGameObject2; // The GameObject to be toggled
    //public GameObject skull; // The GameObject to be toggled

    void Start()
    {
        // Initialize otherButton's and selectedGameObject's state at the start
        UpdateplayButtonInteractable();
        UpdateAnnotationButtonInteractable();
    }

    public void IncrementCount()
    {
        activeToggleCount++;
        UpdateDisplayAndInteraction();
    }

    public void DecrementCount()
    {
        activeToggleCount--;
        UpdateDisplayAndInteraction();
    }

    private void UpdateDisplayAndInteraction()
    {
        UpdateDisplay();
        UpdateplayButtonInteractable();
        UpdateAnnotationButtonInteractable();
        // Additionally, update the active state of selectedGameObject based on activeToggleCount
        // UpdateSelectedGameObjectActiveState();
        //Updateskull();
    }

    private void UpdateDisplay()
    {
        if (counterDisplay != null)
            counterDisplay.text = "Active Toggles: " + activeToggleCount.ToString();
    }

    private void UpdateplayButtonInteractable()
    {
        if (playButton != null)
        {
            // For example, make the otherButton interactable only when no toggles are active
            playButton.interactable = activeToggleCount == 0;
        }
    }
        private void UpdateAnnotationButtonInteractable()
    {
        if (annotationButton != null)
        {
            // For example, make the otherButton interactable only when no toggles are active
            annotationButton.interactable = activeToggleCount == 0;
        }
    }

//     private void UpdateSelectedGameObjectActiveState()
//     {
//         if (selectedGameObject != null)
//         {
//             // Here you can adjust the logic based on your needs
//             // For example, disable the GameObject when any toggle is active
//             selectedGameObject.SetActive(activeToggleCount == 0);
//             selectedGameObject2.SetActive(activeToggleCount == 0);
//         }
//     }
//     //     private void Updateskull()
//     // {
//     //     if (skull != null)
//     //     {

//     //         skull.SetActive(activeToggleCount == 0);
//     //     }
//     // }
 }

