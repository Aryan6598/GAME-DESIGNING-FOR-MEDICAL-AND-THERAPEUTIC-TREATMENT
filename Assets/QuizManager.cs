using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] options;
        public int correctAnswerIndex;
    }

    public Question[] questions; // Array of Questions
    private int currentQuestionIndex = 0;
    private int score = 0;
    private int totalQuestions = 5;

    public Text questionText;
    public Text[] optionTexts;
    public Button[] optionButtons;
    public Button nextButton;
    public Button submitButton;
    
    void Start()
    {
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex < totalQuestions)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < optionTexts.Length; i++)
            {
                optionTexts[i].text = currentQuestion.options[i];
            }

            nextButton.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(false);

            for (int i = 0; i < optionButtons.Length; i++)
            {
                int index = i; // Capture index for delegate
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectAnswer(index));
            }
        }
        else
        {
            SubmitQuiz();
        }
    }

    void SelectAnswer(int selectedIndex)
    {
        if (selectedIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            score++;
        }

        nextButton.gameObject.SetActive(currentQuestionIndex < totalQuestions - 1);
        submitButton.gameObject.SetActive(currentQuestionIndex == totalQuestions - 1);
    }

    public void NextQuestion()
    {
        currentQuestionIndex++;
        LoadQuestion();
    }

    public void SubmitQuiz()
    {
        PlayerPrefs.SetInt("QuizScore", score);
        PlayerPrefs.Save();
        SceneManager.LoadScene(PlayerPrefs.GetString("SelectedGame")); // Load the game
    }
}
