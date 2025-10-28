using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestionnaireManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public int[] scores; // Score values for answers
    }

    public TextMeshProUGUI questionText;
    public Button[] answerButtons; // Buttons for answer choices
    public Slider progressBar;

    private Question[] currentQuestions; // Stores selected game’s questions
    private int[] preGameScores;
    private int[] postGameScores;
    private int currentQuestionIndex = 0;
    private string selectedGame;
    private bool isPreGame = true; // Tracks whether it's pre or post-game

    void Start()
    {
        selectedGame = PlayerPrefs.GetString("SelectedGame", ""); // Get selected game
        LoadQuestions();

        if (currentQuestions == null || currentQuestions.Length == 0)
        {
            Debug.LogError("No questions loaded! Returning to main menu.");
            SceneManager.LoadScene("MainMenu"); // Fallback in case of missing data
            return;
        }

        preGameScores = new int[currentQuestions.Length];
        postGameScores = new int[currentQuestions.Length];

        DisplayQuestion();
    }

    void LoadQuestions()
    {
        if (selectedGame == "SnakeGame")
        {
            currentQuestions = new Question[]
            {
                new Question { questionText = "How would you rate your ability to track moving objects visually?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How well can you control fine motor movements?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How easily do you get distracted?", scores = new int[] { 100, 75, 50, 25, 0 } },
                new Question { questionText = "How would you rate your reaction time?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How comfortable do you feel using directional controls?", scores = new int[] { 0, 25, 50, 75, 100 } }
            };
        }
        else if (selectedGame == "WhackAMole")
        {
            currentQuestions = new Question[]
            {
                new Question { questionText = "How quickly do you respond to sudden visual cues?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How well can you maintain focus on multiple moving targets?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How accurately can you time your actions?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How well do you stay engaged in fast-paced activities?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How well do you handle reacting under pressure?", scores = new int[] { 0, 25, 50, 75, 100 } }
            };
        }
        else if (selectedGame == "MemoryPuzzle")
        {
            currentQuestions = new Question[]
            {
                new Question { questionText = "How easily do you remember sequences or patterns after a short period?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How well can you recall small details from a recently observed set of objects?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How would you rate your ability to recognize and match similar patterns?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How easily do you get mentally fatigued when solving memory-based tasks?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How confident do you feel in your short-term memory retention?", scores = new int[] { 0, 25, 50, 75, 100 } }
            };
        }
        else if (selectedGame == "TicTacToe")
        {
            currentQuestions = new Question[]
            {
                new Question { questionText = "How well would you rate your ability to plan and think ahead before making a decision?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How effectively can you predict the outcomes of different choices?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How confident do you feel about identifying patterns and strategies in a game?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How well do you adapt your strategy when faced with an unexpected move?", scores = new int[] { 0, 25, 50, 75, 100 } },
                new Question { questionText = "How patient do you feel when engaging in strategic or problem-solving activities?", scores = new int[] { 0, 25, 50, 75, 100 } }
            };
        }
    }

    void DisplayQuestion()
    {
        if (currentQuestionIndex >= currentQuestions.Length)
        {
            Debug.Log("All questions completed. Loading the next scene...");

            if (isPreGame)
            {
                PlayerPrefs.SetInt("InitialScore", CalculateTotalScore(preGameScores));
                if (SceneManager.GetActiveScene().name != selectedGame) 
                    SceneManager.LoadScene(selectedGame);
            }
            else
            {
                int initialScore = PlayerPrefs.GetInt("InitialScore", 0);
                int finalScore = CalculateTotalScore(postGameScores);
                int improvement = (initialScore > 0) ? Mathf.RoundToInt(((finalScore - initialScore) / (float)initialScore) * 100) : 0;

                PlayerPrefs.SetInt("Improvement", improvement);
                if (SceneManager.GetActiveScene().name != "ResultsScene") 
                    SceneManager.LoadScene("ResultsScene");
            }
            return;
        }

        questionText.text = currentQuestions[currentQuestionIndex].questionText;
        Debug.Log($"Current Question Index: {currentQuestionIndex}, Total Questions: {currentQuestions.Length}");

    }

    public void SelectAnswer(int answerIndex)
    {
        if (currentQuestions == null || currentQuestions.Length == 0)
        {
            Debug.LogError("No questions available!");
            return;
        }

        if (currentQuestionIndex >= currentQuestions.Length)
        {
            Debug.LogWarning("All questions already answered. Ignoring extra input.");
            return;
        }

        if (isPreGame)
            preGameScores[currentQuestionIndex] = currentQuestions[currentQuestionIndex].scores[answerIndex];
        else
            postGameScores[currentQuestionIndex] = currentQuestions[currentQuestionIndex].scores[answerIndex];

        currentQuestionIndex++;

        if (progressBar != null && currentQuestions.Length > 0)
            progressBar.value = Mathf.Clamp01((float)currentQuestionIndex / currentQuestions.Length);

        DisplayQuestion();
    }

    int CalculateTotalScore(int[] scores)
    {
        int total = 0;
        foreach (int score in scores)
        {
            total += score;
        }
        return total;
    }

    public void StartPostGameAssessment()
    {
        isPreGame = false;
        currentQuestionIndex = 0;
        DisplayQuestion();
    }
}
