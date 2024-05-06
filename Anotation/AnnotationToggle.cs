using UnityEngine;
using UnityEngine.UI; // Required for accessing UI components

public class AnnotationToggle : MonoBehaviour
{
    public string[] customTexts; // Texts representing each stage
    public Text linkedText; // Display current stage as text
    public GameObject VSH; // GameObjects to toggle visibility
    public GameObject OriVSH; // GameObjects to toggle visibility
    public OriVisibilityHandler oriLinkingHandler; // Reference to the OriLinkingHandler
    public VisibilityHandler visibilityHandler; // Reference to the VisibilityHandler

    //private int currentState = 0; // Tracks the current visibility state
    public int currentState { get; private set; } // Now publicly accessible but only settable internally

    public void SetState(int newState)
{
    currentState = newState;
    UpdateVisibilityState();
    UpdateLinkedText();
}

    void Start()
    {   
        VSH.SetActive(false);
        OriVSH.SetActive(true);
        oriLinkingHandler.TurnOffVisibility();
        visibilityHandler.TurnOffVisibility();
        UpdateLinkedText();
    }

    public void OnButtonClick()
    {
        currentState = (currentState + 1) % 3; // Cycle through states 0, 1, 2
        UpdateVisibilityState();
        UpdateLinkedText();
    }

    private void UpdateVisibilityState()
    {
        switch (currentState)
        {
            case 0: // Turn off both lines and spheres
                oriLinkingHandler.TurnOffVisibility();
                visibilityHandler.TurnOffVisibility();
                VSH.SetActive(false);
                OriVSH.SetActive(true);
                break;
            case 1: // Turn on lines only
                oriLinkingHandler.TurnOnVisibility();
                visibilityHandler.TurnOffVisibility();
                VSH.SetActive(false);
                OriVSH.SetActive(true);
                break;
            case 2: // Turn on spheres toggle
                oriLinkingHandler.TurnOffVisibility(); // Assuming you want to turn off lines here; adjust if otherwise
                visibilityHandler.TurnOnVisibility();
                VSH.SetActive(true);
                OriVSH.SetActive(false);
                break;
        }
    }

    private void UpdateLinkedText()
    {
        // Update the linked text based on the current state for clarity
        if (customTexts.Length > currentState)
        {
            linkedText.text = customTexts[currentState];
        }
        else
        {
            Debug.LogWarning("Custom text for this state does not exist.");
        }
    }
}
