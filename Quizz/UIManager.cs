using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text questionText; // Used for displaying the current question text
    public Image resultIndicator; // Used for displaying the result (correct or incorrect) as a color change
    public Text scoreText; // Used for displaying the current score
    public Text finalScore; // Used for displaying the current score

    // Add this line to declare the questionNumberText variable
    public Text questionNumberText; // Used for displaying the current question number and total questions

    public GameObject referenceObject; // Reference to the game object for dimensions and location

    public void UpdateQuestionText(string newText)
    {
        questionText.text = newText; // Update the question text UI element
    }

    public void UpdateResultColor(Color color)
    {
        resultIndicator.color = color; // Update the result indicator's color
    }

    public void ShowFinalScore(int score)
    {
        finalScore.text = "Final Score: " + score; // Update the score text to show the final score
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString(); // Update the score text UI element
    }

    public void UpdateQuestionNumber(int currentQuestionIndex, int totalQuestions)
    {
        // Update the question number UI element to show the current question index and the total number of questions
        questionNumberText.text = (currentQuestionIndex + 1).ToString() + "/" + totalQuestions.ToString();
    }
}
