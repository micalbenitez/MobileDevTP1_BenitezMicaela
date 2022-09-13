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
			switch (Stats.playerWinner)
			{
				case Stats.side.RIGHT:
					winnerImage.sprite = player1Winner;
					player2ScoreText.text = Stats.winnerScore.ToString();
					player1ScoreText.text = Stats.loserScore.ToString();
					player2Animator.Play("Winner");
					break;

				case Stats.side.LEFT:
					winnerImage.sprite = player2Winner;
					player1ScoreText.text = Stats.winnerScore.ToString();
					player2ScoreText.text = Stats.loserScore.ToString();
					player1Animator.Play("Winner");
					break;
			}
		}

		private void ResetGame()
        {
			LoaderManager.Instance.LoadScene(gameSceneName);
        }
	}
}