using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Player;
using Toolbox;
using UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
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

        [Header("Download data")]
        public Download.Download[] downloads = null;

        [Header("Game UI")]
        public UIGame uiGame = null;
        public GameObject[] playersUI = null;

        [Header("Players")]
        public Player[] players = null;
        public CarController[] carControllers = null;
        public PlayerData[] playersData = null;

        [Header("Game cameras")]
        public Camera[] gameCameras = null;

        private Timer gameTimer = new Timer();

        private void Awake()
        {
            SetGameObjectsState(false);
        }

        private void Update()
        {
            UpdateGameTimer();

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
            for (int i = 0; i < gameCameras.Length; i++)
                gameCameras[i].gameObject.SetActive(state);

            for (int i = 0; i < carControllers.Length; i++)
                carControllers[i].enabled = state;


            for (int i = 0; i < playersUI.Length; i++)
                playersUI[i].gameObject.SetActive(state);
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

            if (players[0].money > players[1].money)
            {
                //lado que gano
                if (playersData[0].playerSide == PlayerData.PLAYER_SIDE.RIGHT) 
                    Stats.playerWinner = Stats.side.RIGHT;
                else 
                    Stats.playerWinner = Stats.side.LEFT;

                //puntajes
                Stats.winnerScore = players[0].money;
                Stats.loserScore = players[1].money;
            }
            else
            {
                //lado que gano
                if (playersData[1].playerSide == PlayerData.PLAYER_SIDE.RIGHT) 
                    Stats.playerWinner = Stats.side.RIGHT;
                else 
                    Stats.playerWinner = Stats.side.LEFT;

                //puntajes
                Stats.winnerScore = players[1].money;
                Stats.loserScore = players[0].money;
            }

            players[0].GetComponent<CarController>().Stop();
            players[1].GetComponent<CarController>().Stop();

            for (int i = 0; i < downloads.Length; i++)
                downloads[i].EndGame();
        }

        private void QuitGame()
        {
            Application.Quit();
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