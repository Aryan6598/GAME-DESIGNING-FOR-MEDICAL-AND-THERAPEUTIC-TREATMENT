using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public Text buttonText;
            
    private Gamecontroller Gamecontroller;

    public void SetSpace()
    {
        buttonText.text=Gamecontroller.GetPlayerSide();
        button.interactable=false;
        Gamecontroller.EndTurn();
    }

    public void SetGamecontrollerReference(Gamecontroller controller)
    {
        Gamecontroller=controller;
    }
}
    