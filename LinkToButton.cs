using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkToButton : MonoBehaviour
{
    public GameObject lineRendererPrefab; // Assign a prefab with LineRenderer
    public GameObject[] popupPrefabs; // Assign your popup prefabs, make sure order matches with buttons
    public Button[] buttons; // Assign buttons
    public GameObject[] targetObjects; // Assign target objects, make sure order matches with buttons

    private List<GameObject> popups = new List<GameObject>(); // To keep track of popups
    private Dictionary<Button, GameObject> buttonToPopupMap = new Dictionary<Button, GameObject>(); // To map buttons to popups

    void Start()
    {
        if (buttons.Length != targetObjects.Length || buttons.Length != popupPrefabs.Length)
        {
            Debug.LogError("The arrays of buttons, target objects, and popups must have the same length!");
            return;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            // Create lines
            GameObject lineObj = Instantiate(lineRendererPrefab, transform);
            LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
            Vector3[] positions = { targetObjects[i].transform.position, buttons[i].transform.position }; // Adjust this if you are working in a different space
            lineRenderer.SetPositions(positions);

            // Create popups but keep them disabled
            GameObject popup = Instantiate(popupPrefabs[i], targetObjects[i].transform.position, Quaternion.identity);
            popup.SetActive(false); // Ensure popups are initially inactive
            popups.Add(popup);
            buttonToPopupMap[buttons[i]] = popup;

            // Local copy of the loop variable to avoid closure issues
            int index = i;
            buttons[i].onClick.AddListener(() => TogglePopup(buttons[index]));
        }
    }

    void TogglePopup(Button button)
    {
        // Check if this button has a popup mapped to it
        if (buttonToPopupMap.TryGetValue(button, out GameObject popup))
        {
            // Toggle the active state of the popup
            popup.SetActive(!popup.activeSelf);
        }
    }
}