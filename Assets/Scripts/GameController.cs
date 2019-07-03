using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;

    public Text scoreText;
    public Button scoreButton;

    private string playerSide;
    private Board board = new Board();
    private bool gameOver = false;

    private int gameMoves = 8;
    int move;

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        NewGame();
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gameMoves == 0)
        {
            GameOver("D");
        }
        gameMoves--;
        ChangeSides();
        if (isAiTurn())
        {
            AiTurn();
        }
    }
    private bool isAiTurn()
    {
        return (!gameOver && playerSide == "O");
    }
    private void AiTurn()
    {
        // do random move if first or second try
        if (gameMoves > 7)
        {
            List<int> moves = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
            System.Random random = new System.Random();
            move = random.Next(0, moves.Count);
            while (buttonList[move].text != "")
            {
                moves.RemoveAt(move);
                move = random.Next(0, moves.Count);
            }
        }
        else
        {
            move = board.Ai(buttonList, playerSide, 9);
        }
        buttonList[move].GetComponentInParent<GridSpace>().SetSpace();
    }

    public void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }

    void GameOver(string player)
    {
        scoreButton.gameObject.SetActive(true);
        gameOver = true;
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
        // TODO add new game
        // add butten to start new game
        if (player == "X")
        {
            scoreText.text = "Win";
        }
        else if (player == "O")
        {
            scoreText.text = "Lose";
        }
        else
        {
            scoreText.text = "Draw";
        }
    }

    public void NewGame()
    {
        scoreButton.gameObject.SetActive(false);
        Debug.Log("NEW GAME");
        gameMoves = 8;
        gameOver = false;
        playerSide = "X";
        for (int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].text == "O")
            {
                RawImage jimmy = buttonList[i].GetComponentInParent<Button>().GetComponentInChildren<RawImage>();
                jimmy.color = new Color(jimmy.color.r, jimmy.color.g, jimmy.color.b, 0);
                buttonList[i].color = new Color(1, 0, 0.4f, 1f);
            }
            buttonList[i].text = "";
            buttonList[i].GetComponentInParent<Button>().interactable = true;
        }
    }

    public void JimmiStart()
    {
        if (gameMoves == 8)
        {
            ChangeSides();
            AiTurn();
        }
    }
}
