using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

namespace UI
{
    public class UIFinal : MonoBehaviour
    {
		[Header("Multiplayer UI data")] 
		[SerializeField] private Image winnerImage = null;
		[SerializeField] private Text winnertText = null;
		[SerializeField] private Sprite player1Winner = null;
		[SerializeField] private Sprite player2Winner = null;
		[SerializeField] private GameObject player1 = null;
		[SerializeField] private GameObject player2 = null;
		[SerializeField] private Text player1ScoreText = null;
		[SerializeField] private Text player2ScoreText = null;
		[SerializeField] private Animator player1Animator = null;
		[SerializeField] private Animator player2Animator = null;

		[Header("Single UI data")]
		[SerializeField] private GameObject player = null;
		[SerializeField] private Text playerScoreText = null;
		[SerializeField] private Animator playerAnimator = null;

		[Header("Setting panel")]
		[SerializeField] private GameObject settingPanel = null;

		[Header("Scenes")]
		[SerializeField] private string menuSceneName = "";
		[SerializeField] private string gameSceneName = "";

		private void Start()
        {
			SetWinner();
		}

        private void SetWinner()
		{
			if (GameConfiguration.Instance.GetPlayers() == GameConfiguration.GAME_MODE.SINGLEPLAYER) // Single player
			{
				player1.SetActive(false);
				player2.SetActive(false);
				winnerImage.gameObject.SetActive(false);
				playerScoreText.text = Stats.winnerScore.ToString();
				playerAnimator.Play("Winner");
			}
			else // Multiplayer
			{
				player.SetActive(false);

				if (Stats.winnerScore == Stats.loserScore) // Empate
				{
					winnerImage.enabled = false;
					winnertText.enabled = true;
					player1ScoreText.text = Stats.winnerScore.ToString();
					player2ScoreText.text = Stats.loserScore.ToString();
					player1Animator.Play("Winner");
					player2Animator.Play("Winner");
				}
				else // No empate
				{
					winnertText.enabled = false;
					switch (Stats.playerWinner)
					{
						case Stats.side.RIGHT:
							winnerImage.sprite = player2Winner;
							player2ScoreText.text = Stats.winnerScore.ToString();
							player1ScoreText.text = Stats.loserScore.ToString();
							player2Animator.Play("Winner");
							break;

						case Stats.side.LEFT:
							winnerImage.sprite = player1Winner;
							player1ScoreText.text = Stats.winnerScore.ToString();
							player2ScoreText.text = Stats.loserScore.ToString();
							player1Animator.Play("Winner");
							break;
					}
				}
			}
		}

		public void ResetGame()
        {
			LoaderManager.Instance.LoadScene(gameSceneName);
		}

		public void SettingPanel(bool state)
		{
			settingPanel.SetActive(state);
			if (state) Time.timeScale = 0;
			else Time.timeScale = 1;
		}

		public void MainMenu()
		{
			LoaderManager.Instance.LoadScene(menuSceneName);
		}
	}
}