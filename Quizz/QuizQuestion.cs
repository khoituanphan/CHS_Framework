using UnityEngine;

[System.Serializable]
public class QuizQuestion
{
    public string questionText;
    public Transform dotTransform; // The actual dot in the scene
    public bool answeredCorrectly;
}
