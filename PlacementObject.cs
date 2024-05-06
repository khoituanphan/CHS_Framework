using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementObject : MonoBehaviour
{
    [SerializeField]
    private bool IsSelected;

    [SerializeField]
    private bool ShouldICreateOverlay = false;

    [SerializeField]
    private TextMesh OverlayText;

    [SerializeField]
    private string OverlayDisplayText;

    public bool Selected
    {
        get { return IsSelected; }
        set
        {
            IsSelected = value;
            // Check if we should create an overlay and if the OverlayText component exists
            if (ShouldICreateOverlay && OverlayText != null)
            {
                // If the object is selected, enable the overlay and set its text
                if (IsSelected)
                {
                    OverlayText.gameObject.SetActive(true);
                    OverlayText.text = OverlayDisplayText;
                }
                else // If the object is not selected, disable the overlay
                {
                    OverlayText.gameObject.SetActive(false);
                }
            }
        }
    }

    void Awake()
    {
        // Try to find the TextMesh component in children
        OverlayText = GetComponentInChildren<TextMesh>();
        // Initially disable the overlay text if it exists
        if (OverlayText != null)
        {
            OverlayText.gameObject.SetActive(false);
        }
    }
}
