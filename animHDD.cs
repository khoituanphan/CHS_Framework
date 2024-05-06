using System.Collections;
using UnityEngine;

public class animHDD : MonoBehaviour
{
    public Animator boneAnimator;
    public AnnotationToggle annotationToggle; // Reference to the AnnotationToggle
    public VisibilityHandler visibilityHandler; // Reference to the VisibilityHandler
    public GameObject selectedGameObject; // The GameObject to be toggled
    public GameObject enableGameObject; // The GameObject to enable temporarily

    void Start()
    {
        //selectedGameObject.SetActive(true); // Ensure it's initially enabled
        //enableGameObject.SetActive(false); // Ensure it's initially disabled
        if (boneAnimator == null)
        {
            Debug.LogError("Animator not set in the inspector");
        }
        if (visibilityHandler == null)
        {
            Debug.LogError("VisibilityHandler not set in the inspector");
        }
    }

    public IEnumerator PlayAnimationsAndAdjustVisibility()
    {
        if (boneAnimator != null && visibilityHandler != null)
        {
            if (boneAnimator.gameObject.activeInHierarchy && AnimationExists(boneAnimator, "MandibleAction") && AnimationExists(boneAnimator, "Lower_teethAction"))
            {

                // Adjust visibility of GameObjects
                selectedGameObject.SetActive(false); // Disable the selected GameObject
                enableGameObject.SetActive(true); // Enable the temporary GameObject

                // Ensure the layers are correctly configured and weighted
                boneAnimator.SetLayerWeight(1, 1);

                // Start playing the animations on their respective layers
                boneAnimator.Play("MandibleAction", 1, 0.0f);
                boneAnimator.Play("Lower_teethAction", 0, 0.0f);

                // Wait for the longer of the two animations to finish
                float animationLength = Mathf.Max(
                    boneAnimator.GetCurrentAnimatorStateInfo(0).length,
                    boneAnimator.GetCurrentAnimatorStateInfo(1).length
                );
                yield return new WaitForSeconds(animationLength);

                // Reset visibility to original state
                selectedGameObject.SetActive(true); // Re-enable the selected GameObject
                enableGameObject.SetActive(false); // Re-disable the temporary GameObject

            }
            else
            {
                Debug.LogError("Animator is not active or does not have the specified animations");
            }
        }
        else
        {
            Debug.LogError("Animator or VisibilityHandler is not set.");
        }
    }

    private bool AnimationExists(Animator animator, string animationName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return true;
            }
        }
        return false;
    }

    // Method to be called by the button to start the coroutine
    public void OnButtonClick()
    {
        // Ensure the GameObject that contains the Animator is enabled
    boneAnimator.gameObject.SetActive(true);

    // Set AnnotationToggle state to 0 and update its state before playing the animation
    if (annotationToggle != null)
    {
        annotationToggle.SetState(0); // You'll need to implement SetState method in AnnotationToggle
    }
    
    // Then start the coroutine to play the animations
    StartCoroutine(PlayAnimationsAndAdjustVisibility());
    }
}
