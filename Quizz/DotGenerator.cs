using UnityEngine;

public class DotGenerator : MonoBehaviour
{
    [System.Serializable]
    public class TargetObject
    {
        public GameObject target; // The target object where the dot will be instantiated
        public string targetName; // The name of the target object for the quiz question
    }

    public GameObject spherePrefab; // Use a 3D sphere prefab instead of an Image
    public QuizManager quizManager; // Reference to the QuizManager
    private QuizTargets quizTargets; // Reference to the QuizTargets script

    void Start()
    {

    }
}
