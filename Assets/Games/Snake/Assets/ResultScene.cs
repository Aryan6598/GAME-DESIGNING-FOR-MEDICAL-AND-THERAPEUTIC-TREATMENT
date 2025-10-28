using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenImprovementScene : MonoBehaviour
{
    public void OpenImprovementResult()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
