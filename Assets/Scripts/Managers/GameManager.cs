using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Player;
using UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Players")]
        public Player[] players = null;

        [Header("Game cameras")]
        public Camera[] gameCameras = null;

        [Header("Car controller")]
        public CarController[] carControllers = null;

        [Header("Game UI")]
        public UIGame uiGame = null;

        private void Awake()
        {
            SetGameObjectsState(false);
        }

        public void StartGame()
        {
            SetGameObjectsState(true);
        }

        private void SetGameObjectsState(bool state)
        {
            for (int i = 0; i < gameCameras.Length; i++)
                gameCameras[i].gameObject.SetActive(state);

            for (int i = 0; i < carControllers.Length; i++)
                carControllers[i].enabled = state;

            uiGame.gameObject.SetActive(state);
        }

        private void OnEnable()
        {
            for (int i = 0; i < players.Length; i++)
                players[i].OnUpdateScore += uiGame.UpdateScore;
        }

        private void OnDisable()
        {
            for (int i = 0; i < players.Length; i++)
                players[i].OnUpdateScore -= uiGame.UpdateScore;
        }
    }
}