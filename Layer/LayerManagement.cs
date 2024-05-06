using UnityEngine;
using UnityEngine.UI;
public class LayerManagement : MonoBehaviour
{
    public GameObject scrollObject; // Assign the Scroll GameObject in the inspector
    public GameObject explore21Object; // Assign the Explore21 GameObject in the inspector
    public GameObject explodeObject; // Assign the Explode GameObject in the inspector
    public GameObject skullObject; // Assign the Skull GameObject in the inspector
    public AnnotationToggle annotationToggle; // Reference to the AnnotationToggle

    public Button annotationButton; // Assign the Annotation Button in the inspector
    public Button playButton; // Assign the Play Button in the inspector

    private bool isSpecialConditionActive = false; // Tracks whether the special condition is active

    // This method is called when the button is clicked
    public void OnButtonClick()
    {
        // If the button is clicked and the scrollObject is enabled, or if the special condition is already active
        if (scrollObject.activeSelf || isSpecialConditionActive)
        {
            ToggleSpecialCondition();
        }
    }

    // This method toggles the special condition based on the current state
    private void ToggleSpecialCondition()
    {
        if (!isSpecialConditionActive && scrollObject.activeSelf)
        {
            isSpecialConditionActive = true;
            SetObjectsAndButtonsState(false, false, true, false, false);
            annotationToggle.SetState(0);
        }
        else if (isSpecialConditionActive && !scrollObject.activeSelf)
        {
            isSpecialConditionActive = false;
            SetObjectsAndButtonsState(true, true, false, true, true);
        }
    }

    // Helper method to set the active state of the objects and the interactable state of buttons
    private void SetObjectsAndButtonsState(bool explore21Active, bool explodeActive, bool skullActive, bool annotationInteractable, bool playInteractable)
    {
        explore21Object.SetActive(explore21Active);
        explodeObject.SetActive(explodeActive);
        skullObject.SetActive(skullActive);

        annotationButton.interactable = annotationInteractable;
        playButton.interactable = playInteractable;

        Debug.Log($"Explore21: {explore21Active}, Explode: {explodeActive}, Skull: {skullActive}, Annotation Button: {annotationInteractable}, Play Button: {playInteractable}");
    }
}
