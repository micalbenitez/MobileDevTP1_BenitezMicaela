using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Toolbox;

public class GameConfiguration : MonoBehaviourSingleton<GameConfiguration>
{
    public enum GAME_MODE
    {
        SINGLEPLAYER,
        MULTIPLAYER
    }

    public enum GAME_DIFFICULT
    {
        EASY,
        MEDIUM,
        DIFFICULT
    }

    [Header("Buttons data")]
    [SerializeField] private Color unpressButton = Color.white;
    [SerializeField] private Color pressButton = Color.white;

    [Header("Configuration")]
    public GAME_MODE playersQuantity;
    public GAME_DIFFICULT difficult;

    private void Start()
    {
        playersQuantity = GAME_MODE.SINGLEPLAYER;
        difficult = GAME_DIFFICULT.EASY;
    }

    public void ChangeButtonColor(Button[] buttons, int index)
    {
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i == index) buttons[i].image.color = pressButton;
                else buttons[i].image.color = unpressButton;
            }
        }
    }

    public void SetPlayers(int gameMode)
    {
        playersQuantity = (GAME_MODE)gameMode;
    }

    public void SetDifficult(int gameDifficult)
    {
        difficult = (GAME_DIFFICULT)gameDifficult;
    }

    public GAME_MODE GetPlayers() => playersQuantity;
    public GAME_DIFFICULT GetDifficult() => difficult;
}