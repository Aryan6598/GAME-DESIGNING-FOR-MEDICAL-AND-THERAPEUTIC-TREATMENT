using UnityEngine;
using TMPro;

public class RandomImprovementDisplay : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    void Start()
    {
        int randomPercentage = Random.Range(60, 81); // 60 to 80 inclusive
        resultText.text = "Improvement: " + randomPercentage + "%";
    }
}
