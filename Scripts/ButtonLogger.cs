using UnityEngine;

public class ButtonLogger : MonoBehaviour
{
    // This method logs a custom message to the console
    public void LogButtonPress(string message)
    {
        Debug.Log("Button Pressed: " + message);
    }
}
