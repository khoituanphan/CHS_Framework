using UnityEngine;
using UnityEngine.SceneManagement; // This namespace is required for loading scenes

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Debug.Log("Changing scene to: " + sceneName); // Print a message to the console
        SceneManager.LoadScene(sceneName); // Load the scene with the specified name
    }
}
