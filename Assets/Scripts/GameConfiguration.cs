using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

public class GameConfiguration : MonoBehaviourSingleton<GameConfiguration>
{
    [Header("Configuration")]
    [SerializeField] private int playersQuantity;
    [SerializeField] private int difficult;

    public void SetPlayers(int value) => playersQuantity = value;
    public int GetPlayers() => playersQuantity;

    public void SetDifficult(int value) => difficult = value;
    public int Difficult() => difficult;
}