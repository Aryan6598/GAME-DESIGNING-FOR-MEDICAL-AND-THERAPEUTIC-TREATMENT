using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void LoadGameWithQuestionnaire(string gameName)
    {
        PlayerPrefs.SetString("SelectedGame", gameName); // Store selected game
        SceneManager.LoadScene("QuestionnaireScene"); // Load questionnaire first
    }

    public void LoadSnakeGame()
    {
        LoadGameWithQuestionnaire("SnakeGame");
    }

    public void LoadWhackAMoleGame()
    {
        LoadGameWithQuestionnaire("WhackAMole");
    }

    public void LoadMemoryPuzzleGame()
    {
        LoadGameWithQuestionnaire("MemoryPuzzle");
    }

    public void LoadTicTacToeGame()
    {
        LoadGameWithQuestionnaire("TicTacToe");
    }
}
