using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    void OnMouseDown() {
        Debug.Log(gameObject.name + " was clicked.");
    }
}
