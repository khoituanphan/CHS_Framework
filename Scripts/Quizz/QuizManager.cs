using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    public QuizTargets quizTargets;
    public UIManager uiManager;
    public GameObject endOfQuizPanel; // Reference to the GameObject to enable at the end of the quiz
    public int numberOfQuestions = 10;
    private List<QuizQuestion> _questions = new List<QuizQuestion>();
    private int currentQuestionIndex = 0;
    private int score = 0;

    public static QuizManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private List<QuizQuestion> Questions
    {
        get { return _questions; }
        set { _questions = value; }
    }

    void Start()
    {
        InitializeQuiz();
        endOfQuizPanel.SetActive(false); // Ensure the panel is disabled at the start of the quiz
    }

    void InitializeQuiz()
    {
        List<TargetObject> randomTargets = quizTargets.GetRandomTargets(numberOfQuestions);
        var initialQuestions = randomTargets.Select(target => new QuizQuestion
        {
            questionText = target.objectName,
            dotTransform = target.gameObject.transform,
            answeredCorrectly = false
        }).ToList();

        SetQuestions(initialQuestions);
        UpdateUIForCurrentQuestion();
        uiManager.UpdateScoreText(score);
    }

    public void ReceiveAnswer(string selectedTargetName)
    {
        var currentQuestion = Questions[currentQuestionIndex];

        Debug.Log($"Received Answer: {selectedTargetName}, Expected Answer: {currentQuestion.questionText}");

        if (selectedTargetName == currentQuestion.questionText)
        {
            currentQuestion.answeredCorrectly = true;
            uiManager.UpdateResultColor(Color.green);
            score++;
        }
        else
        {
            currentQuestion.answeredCorrectly = false;
            uiManager.UpdateResultColor(Color.red);
        }

        uiManager.UpdateScoreText(score);
        AdvanceToNextQuestion();
    }

    private void AdvanceToNextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex >= Questions.Count)
        {
            EndQuiz();
        }
        else
        {
            UpdateUIForCurrentQuestion();
        }
    }

    private void SetQuestions(List<QuizQuestion> newQuestions)
    {
        Questions = newQuestions.Take(numberOfQuestions).ToList();
    }

    private void UpdateUIForCurrentQuestion()
    {
        uiManager.UpdateQuestionText(Questions[currentQuestionIndex].questionText);
        uiManager.UpdateScoreText(score);
        uiManager.UpdateQuestionNumber(currentQuestionIndex , Questions.Count);
    }

    private void EndQuiz()
    {
        uiManager.ShowFinalScore(score);
        Debug.Log($"Quiz Ended. Final Score: {score}");
        endOfQuizPanel.SetActive(true); // Enable the panel at the end of the quiz
    }
}
