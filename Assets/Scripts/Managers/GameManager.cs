using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Player;
using Toolbox;
using UI;

namespace Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public enum EstadoJuego { Calibrando, Jugando, Finalizado }
        public EstadoJuego EstAct = EstadoJuego.Calibrando;

        [Header("Game data")]
        public float gameDuration = 0;
        public string finalScene = "";

        [Header("Download data")]
        public Download.Download[] downloads = null;

        [Header("Game UI")]
        public UIGame uiGame = null;

        [Header("Players")]
        public Player[] players = null;
        public CarController[] carControllers = null;
        public PlayerData[] playersData = null;

        [Header("Game cameras")]
        public Camera[] gameCameras = null;

        private Timer gameTimer = new Timer();

        protected override void OnAwaken()
        {
            SetGameObjectsState(false);
        }

        private void Update()
        {
            UpdateGameTimer();
        }

        private void SetGameObjectsState(bool state)
        {
            for (int i = 0; i < gameCameras.Length; i++)
                gameCameras[i].gameObject.SetActive(state);

            for (int i = 0; i < carControllers.Length; i++)
                carControllers[i].enabled = state;

            uiGame.gameObject.SetActive(state);
        }

        private void UpdateGameTimer()
        {
            if (gameTimer.Active) gameTimer.UpdateTimer();
            if (gameTimer.ReachedTimer()) EndGame();
        }

        private void EndGame()
        {
            LoaderManager.Instance.LoadScene(finalScene);
            EstAct = EstadoJuego.Finalizado;

            if (players[0].money > players[1].money)
            {
                //lado que gano
                if (playersData[0].LadoAct == PlayerData.Visualizacion.Der) DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
                else DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

                //puntajes
                DatosPartida.PtsGanador = players[0].money;
                DatosPartida.PtsPerdedor = players[1].money;
            }
            else
            {
                //lado que gano
                if (playersData[1].LadoAct == PlayerData.Visualizacion.Der) DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
                else DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

                //puntajes
                DatosPartida.PtsGanador = players[1].money;
                DatosPartida.PtsPerdedor = players[0].money;
            }

            players[0].GetComponent<CarController>().Stop();
            players[1].GetComponent<CarController>().Stop();

            for (int i = 0; i < downloads.Length; i++)
                downloads[i].EndGame();
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

        public void StartGame()
        {
            SetGameObjectsState(true);
            gameTimer.SetTimer(gameDuration, Timer.TIMER_MODE.DECREASE, true);
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public PlayerData(int tipoDeInput, Player pj)
        {
            TipoDeInput = tipoDeInput;
            PJ = pj;
        }

        public bool FinCalibrado = false;
        public bool FinTuto1 = false;
        public bool FinTuto2 = false;

        public enum Visualizacion { Der, Izq }
        public Visualizacion LadoAct = Visualizacion.Der;

        public int TipoDeInput = -1;

        public Player PJ;
    }
}