using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public RawImage jimmy;
    public Button button;
    public Text buttonText;
    public string playerSide;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetSpace()
    {
        button.interactable = false;
        // buttonText.text = 
        string player = gameController.GetPlayerSide();
        if (player == "O")
        {
            buttonText.color = new Color(0, 0, 0, 0);
            jimmy.color = new Color(jimmy.color.r, jimmy.color.g, jimmy.color.b, 1);
        }
        buttonText.text = player;
        gameController.EndTurn();

    }
}
