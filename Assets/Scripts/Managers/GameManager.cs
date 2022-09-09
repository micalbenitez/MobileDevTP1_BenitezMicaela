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



        public abstract class GMState
        {
            public abstract void Enter(GameManager gameManager);
            public abstract void Update(GameManager gameManager);
            public abstract GMState NextState(GameManager gameManager);
            public abstract void Exit(GameManager gameManager);
        }
        public class GMStateTutorial : GMState
        {
            public override void Enter(GameManager gameManager)
            {
                gameManager.SetGameObjectsState(false);
            }
            public override void Update(GameManager gameManager)
            {
                gameManager.players[0].tutorial.PlayTutorial();
                gameManager.players[1].tutorial.PlayTutorial();
            }
            public override GMState NextState(GameManager gameManager)
            {
                if (GameConfiguration.Instance.GetPlayers() == GameConfiguration.GAME_MODE.SINGLEPLAYER)
                {
                    if (gameManager.players[0].tutorial.FinishTutorial())
                        return gameManager.gMGame;
                }
                else
                {
                    if (gameManager.players[0].tutorial.FinishTutorial() && gameManager.players[1].tutorial.FinishTutorial())
                        return gameManager.gMGame;
                }

                return null;
            }
            public override void Exit(GameManager gameManager)
            {
                gameManager.StartGame();
                gameManager.players[0].tutorial.gameObject.SetActive(false);
                gameManager.players[1].tutorial.gameObject.SetActive(false);
            }
        }
        public class GMStateGame : GMState
        {
            public override void Enter(GameManager gameManager)
            {
                
            }
            public override void Update(GameManager gameManager)
            {
                // Codigo que se ejecuta en el game
            }
            public override GMState NextState(GameManager gameManager)
            {
                return null;
            }
            public override void Exit(GameManager gameManager)
            {
                
            }
        }
        public class GMStateEndGame : GMState
        {
            public override void Enter(GameManager gameManager)
            {
                
            }
            public override void Update(GameManager gameManager)
            {
                // Codigo que se ejecuta en el end game
            }
            public override GMState NextState(GameManager gameManager)
            {
                return null;
            }
            public override void Exit(GameManager gameManager)
            {
                
            }
        }

        [SerializeField] private GMState currentState = null;
        private GMStateTutorial gMStateTutorial = new GMStateTutorial();
        private GMStateGame gMGame = new GMStateGame();
        private GMStateEndGame gMEndGame = new GMStateEndGame();






        [Header("Game data")]
        public float gameDuration = 0;
        public string finalScene = "";

        [Header("Player data")]
        public PlayerComponent[] players = null;

        [Header("Game UI")]
        public UIGame uiGame = null;

        [Header("Obstacles")]
        public GameObject boxes = null;
        public GameObject cones = null;
        public GameObject taxis = null;

        private Timer gameTimer = new Timer();

        private void Awake()
        {
            currentState = gMStateTutorial;
            if (currentState != null) currentState.Enter(this);
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Update(this);
                GMState nextState = currentState.NextState(this);
                if (nextState != null) ChangeState(nextState);
            }


            var touches = Input.touches;
            string log = "";
            foreach (var t in touches)
            {
                log += $"{t.fingerId} - {t.position} \n";
            }
            Debug.LogWarning(log);

            UpdateGameTimer();
            ConfiguratePlayersQuantity();
            ConfigurateDifficult();

            /// Quit game
            if (Input.GetKeyDown(KeyCode.Escape)) QuitGame();
        }

        private void ChangeState(GMState nextState)
        {
            if (currentState != null) currentState.Exit(this );
            nextState.Enter(this);
            currentState = nextState;
        }

        public void StartGame()
        {
            SetGameObjectsState(true);
            gameTimer.SetTimer(gameDuration, Timer.TIMER_MODE.DECREASE, true);
        }

        private void SetGameObjectsState(bool state)
        {
            if (GameConfiguration.Instance.GetPlayers() == GameConfiguration.GAME_MODE.SINGLEPLAYER)
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

        private void ConfiguratePlayersQuantity()
        {
            if (GameConfiguration.Instance.GetPlayers() == GameConfiguration.GAME_MODE.SINGLEPLAYER)
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

        private void ConfigurateDifficult()
        {
            if (GameConfiguration.Instance.GetDifficult() == GameConfiguration.GAME_DIFFICULT.EASY)
            {
                boxes.SetActive(false);
                cones.SetActive(false);
                taxis.SetActive(false);
            }
            else if (GameConfiguration.Instance.GetDifficult() == GameConfiguration.GAME_DIFFICULT.MEDIUM)
            {
                boxes.SetActive(true);
                cones.SetActive(true);
                taxis.SetActive(false);
            }
            else
            {
                boxes.SetActive(true);
                cones.SetActive(true);
                taxis.SetActive(true);
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
            currentState = gMEndGame;

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