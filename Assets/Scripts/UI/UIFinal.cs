using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

namespace UI
{
    public class UIFinal : MonoBehaviour
    {
		[Header("Game scene")]
		public string gameSceneName = "";

		[Header("Final UI data")] 
		public Image winnerImage = null;
		public Sprite player1Winner = null;
		public Sprite player2Winner = null;
		public Text player1ScoreText = null;
		public Text player2ScoreText = null;

		[Header("Score animations")]
		public Animator player1Animator = null; 
		public Animator player2Animator = null;

		private void Start()
        {
			SetWinner();
		}

        private void Update()
        {
			/// Replay game
			if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
				ResetGame();
		}

        private void SetWinner()
		{
			switch (DatosPartida.LadoGanadaor)
			{
				case DatosPartida.Lados.Der:
					winnerImage.sprite = player1Winner;
					player1ScoreText.text = DatosPartida.PtsGanador.ToString();
					player2ScoreText.text = DatosPartida.PtsPerdedor.ToString();
					player1Animator.Play("Winner");
					break;

				case DatosPartida.Lados.Izq:
					winnerImage.sprite = player2Winner;
					player2ScoreText.text = DatosPartida.PtsGanador.ToString();
					player1ScoreText.text = DatosPartida.PtsPerdedor.ToString();
					player2Animator.Play("Winner");
					break;
			}
		}

		private void ResetGame()
        {
			LoaderManager.Instance.LoadScene(gameSceneName);
        }
	}
}