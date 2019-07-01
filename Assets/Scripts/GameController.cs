using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public Text playerSideText;

    private string playerSide;
    private Board board = new Board();
    private bool gameOver = false;

    private int gameMoves = 9;
    private bool ai = false;

    // scores
    public Text xWins;
    public Text oWins;
    public Text dWins;

    public Text aiLvl;

    private bool aiTurn = false;

    void Awake()
    {
        playerSide = "X";
        SetGameControllerReferenceOnButtons();
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
        gameMoves--;
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
        ChangeSides();
        if (!gameOver && playerSide == "O" && ai)
        {
            Debug.Log(aiLvl.text);
            int move = board.Ai(buttonList, playerSide, int.Parse(aiLvl.text));
            Debug.Log("best move: " + move);
            buttonList[move].GetComponentInParent<GridSpace>().SetSpace();
        }
    }

    private bool ai_turn()
    {
        aiTurn = !aiTurn;
        return aiTurn;
    }

    public void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        playerSideText.text = playerSide;
    }

    void GameOver(string player)
    {
        gameOver = true;
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
        if (player == "X")
        {
            xWins.text = (1 + int.Parse(xWins.text)).ToString();
        }
        else if (player == "O")
        {
            oWins.text = (1 + int.Parse(oWins.text)).ToString();
        }
        else
        {
            dWins.text = (1 + int.Parse(dWins.text)).ToString();
        }
    }
    public void Reset()
    {
        Debug.Log("RESET");
        NewGame();
        xWins.text = 0.ToString();
        oWins.text = 0.ToString();
        dWins.text = 0.ToString();
    }

    public void NewGame()
    {
        Debug.Log("NEW GAME");
        gameMoves = 9;
        gameOver = false;
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
        if (playerSide == "O" && ai)
        {
            ChangeSides();
            gameMoves++;
            EndTurn();
        }
    }

    public void HumanVsHuman()
    {
        ai = false;
    }
    public void HumanVsAi()
    {
        ai = true;
    }

    public void AiLvlPluss()
    {
        if (aiLvl.text != "9")
        {
            aiLvl.text = (int.Parse(aiLvl.text) + 1).ToString();
        }
    }
    public void AiLvlMinus()
    {
        if (aiLvl.text != "1")
        {
            aiLvl.text = (int.Parse(aiLvl.text) - 1).ToString();
        }
    }
}
