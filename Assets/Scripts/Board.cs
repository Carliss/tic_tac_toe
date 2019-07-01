using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board
{
    private int[] board = new int[9];
    private string player_max;
    private string player_min;
    private int current_score;

    void SetBoardFromText(Text[] buttonList)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            if (buttonList[i].text == player_max)
            {
                board[i] = 1;
            }
            else if (buttonList[i].text == player_min)
            {
                board[i] = -1;
            }
            else
            {
                board[i] = 0;
            }
        }
    }

    public int Ai(Text[] list, string player, int lvl)
    {
        if (player == "X")
        {
            player_max = "X";
            player_min = "O";
        }
        else
        {
            player_max = "O";
            player_min = "X";
        }
        SetBoardFromText(list);
        Debug.Log(string.Join(", ", board.ToList()));
        int[] move = minimax(board, lvl, 1, -100, 100);
        return move[0];
    }

    int[] minimax(int[] board_a, int depth, int player, int alpha, int beta)
    {
        
        int vs_player = -player;
        int[] best;
        if (player == -1)
        {
            best = new int[2] { -1, 100 };
        }
        else
        {
            best = new int[2] { -1, -100 };
        }
        if (depth == 0 || GameOver(-player, board_a))
        {
            return new int[2] { -1, current_score };
        }
        // clone? 
        // int[] board_b = (int[])board.Clone();
        for (int i = 0; i < board_a.Length; i++)
        {
            if (board_a[i] == 0)
            {
                board_a[i] = player;
                int[] score = minimax(board_a, depth - 1, -player, alpha, beta);
                board_a[i] = 0;
                if (player == 1)
                {
                    if (score[1] > best[1])
                    {
                        best[0] = i;
                        best[1] = score[1];
                        alpha = Math.Max(alpha, best[1]);
                    }
                }
                else
                {
                    if (score[1] < best[1])
                    {
                        best[0] = i;
                        best[1] = score[1];
                        beta = Math.Min(beta, best[1]);
                    }
                }
                if (alpha >= beta)
                {
                    Debug.Log("prun");
                    break;
                }
            }
        }
        return best;
    }

    bool GameOver(int player, int[] board)
    {
        if (board[4] == player)
        {
            // right diaginal
            if (board[0] == player && board[8] == player)
            {
                current_score = 10 * player;
                return true;
            }
            // left diagonal
            if (board[2] == player && board[6] == player)
            {
                current_score = 10 * player;
                return true;
            }
            // horisonalt
            if (board[3] == player && board[5] == player)
            {
                current_score = 10 * player;
                return true;
            }
            // vertical
            if (board[1] == player && board[7] == player)
            {
                current_score = 10 * player;
                return true;
            }
        }
        if (board[0] == player)
        {
            // top
            if (board[1] == player && board[2] == player)
            {
                current_score = 10 * player;
                return true;
            }
            // left
            if (board[3] == player && board[6] == player)
            {
                current_score = 10 * player;
                return true;
            }
        }
        if (board[8] == player)
        {
            // bottom
            if (board[6] == player && board[7] == player)
            {
                current_score = 10 * player;
                return true;
            }
            // right
            if (board[2] == player && board[5] == player)
            {
                current_score = 10 * player;
                Debug.Log("YES " + player);
                return true;
            }

        }
        current_score = 0;
        // if no one won
        if (!board.Contains(0))
        {
            return true;
        }
        return false;
    }

}
