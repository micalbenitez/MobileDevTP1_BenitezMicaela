using System;
using System.Collections.Generic;
using UnityEngine;
using Entities.Player;
using Toolbox;
using UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Serializable]
        public class PlayerComponent
        {
            public string tag;
            public Player player;
            public CarController carController;
            public PlayerData playerData;
            public Camera gameCamera;
            public GameObject playerUI;
            public GameObject virtualJoystick;
            public Tutorial.Tutorial tutorial;
            public Camera tutorialCamera;
            public Download.Download download;
            public Camera downloadCamera;
        }

        public enum GAME_STATE 
        { 
            TUTORIAL, 
            GAME, 
            ENDGAME
        }
        public GAME_STATE gameState = GAME_STATE.TUTORIAL;

        [Header("Game data")]
        public float gameDuration = 0;
        public string finalScene = "";

        [Header("Player data")]
        public PlayerComponent[] players = null;

        [Header("Game UI")]
        public UIGame uiGame = null;

        private Timer gameTimer = new Timer();

        private void Awake()
        {
            SetGameObjectsState(false);
        }

        private void Update()
        {
            UpdateGameTimer();
            ConfiguratedPlayersQuantity();

            /// Quit game
            if (Input.GetKeyDown(KeyCode.Escape)) QuitGame();
        }

        public void StartGame()
        {
            SetGameObjectsState(true);
            gameTimer.SetTimer(gameDuration, Timer.TIMER_MODE.DECREASE, true);
        }

        private void SetGameObjectsState(bool state)
        {
            if (GameConfiguration.Instance.GetPlayers() == 1)
            {
                players[0].gameCamera.gameObject.SetActive(state);
                players[0].carController.enabled = state;
                players[0].playerUI.SetActive(state);
            }
            else
            {
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].gameCamera.gameObject.SetActive(state);
                    players[i].carController.enabled = state;
                    players[i].playerUI.SetActive(state);
                }
            }
        }

        private void ConfiguratedPlayersQuantity()
        {
            if (GameConfiguration.Instance.GetPlayers() == 1)
            {
                players[1].player.gameObject.SetActive(false);
                players[1].gameCamera.gameObject.SetActive(false);
                players[1].playerUI.SetActive(false);
                players[1].virtualJoystick.SetActive(false);
                players[1].tutorial.gameObject.SetActive(false);
                players[1].download.gameObject.SetActive(false);

                players[0].gameCamera.rect = new Rect(0, 0, 1, 1);
                players[0].tutorialCamera.rect = new Rect(0, 0, 1, 1);
                players[0].downloadCamera.rect = new Rect(0, 0, 1, 1);
            }
        }

        private void UpdateGameTimer()
        {
            if (gameTimer.Active) gameTimer.UpdateTimer();
            if (gameTimer.ReachedTimer()) EndGame();
        }

        private void EndGame()
        {
            LoaderManager.Instance.LoadScene(finalScene);
            gameState = GAME_STATE.ENDGAME;

            if (players[0].player.money > players[1].player.money)
            {
                /// Winner
                if (players[0].playerData.playerSide == PlayerData.PLAYER_SIDE.RIGHT) 
                    Stats.playerWinner = Stats.side.RIGHT;
                else 
                    Stats.playerWinner = Stats.side.LEFT;

                /// Score
                Stats.winnerScore = players[0].player.money;
                Stats.loserScore = players[1].player.money;
            }
            else
            {
                /// Winner
                if (players[1].playerData.playerSide == PlayerData.PLAYER_SIDE.RIGHT) 
                    Stats.playerWinner = Stats.side.RIGHT;
                else 
                    Stats.playerWinner = Stats.side.LEFT;

                /// Score
                Stats.winnerScore = players[1].player.money;
                Stats.loserScore = players[0].player.money;
            }

            players[0].player.GetComponent<CarController>().Stop();
            players[1].player.GetComponent<CarController>().Stop();

            for (int i = 0; i < players.Length; i++)
                players[i].download.EndGame();
        }

        private void QuitGame()
        {
            Application.Quit();
        }

        private void OnEnable()
        {
            for (int i = 0; i < players.Length; i++)
                players[i].player.OnUpdateScore += uiGame.UpdateScore;
        }

        private void OnDisable()
        {
            for (int i = 0; i < players.Length; i++)
                players[i].player.OnUpdateScore -= uiGame.UpdateScore;
        }
    }
}