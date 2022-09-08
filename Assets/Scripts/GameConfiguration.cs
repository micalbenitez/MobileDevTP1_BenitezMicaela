using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Toolbox;

public class GameConfiguration : MonoBehaviourSingleton<GameConfiguration>
{
    [Header("Players")]
    [SerializeField] private Button[] playersButtons = null;

    [Header("Difficult")]
    [SerializeField] private Button[] difficultButtons = null;

    [Header("Buttons data")]
    [SerializeField] private Color unpressButton = Color.white;
    [SerializeField] private Color pressButton = Color.white;

    [Header("Configuration")]
    [SerializeField] private int playersQuantity;
    [SerializeField] private int difficult;

    private void Start()
    {
        playersQuantity = 1;
        difficult = 1;
        ChangeButtonColor(playersButtons, playersQuantity - 1);
        ChangeButtonColor(difficultButtons, difficult - 1);
    }

    private void ChangeButtonColor(Button[] buttons, int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == index) buttons[i].image.color = pressButton;
            else buttons[i].image.color = unpressButton;
        }
    }

    public void SetPlayers(int value)
    {
        playersQuantity = value;
        ChangeButtonColor(playersButtons, playersQuantity - 1);
    }

    public void SetDifficult(int value)
    {
        difficult = value;
        ChangeButtonColor(difficultButtons, difficult - 1);
    }

    public int Difficult() => difficult;
    public int GetPlayers() => playersQuantity;
}