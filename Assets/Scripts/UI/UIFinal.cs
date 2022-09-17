using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

namespace UI
{
    public class UIFinal : MonoBehaviour
    {
		[Header("Final UI data")] 
		[SerializeField] private Image winnerImage = null;
		[SerializeField] private Sprite player1Winner = null;
		[SerializeField] private Sprite player2Winner = null;
		[SerializeField] private Text player1ScoreText = null;
		[SerializeField] private Text player2ScoreText = null;

		[Header("Score animations")]
		[SerializeField] private Animator player1Animator = null;
		[SerializeField] private Animator player2Animator = null;

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