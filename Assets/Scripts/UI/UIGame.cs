using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;

namespace UI
{
    public class UIGame : MonoBehaviour
    {
        [Header("Game UI")]
        [SerializeField] private GameObject timer = null;
        [SerializeField] private Text timerText = null;
        [SerializeField] private Text[] scoreTexts = null;

        [Header("Download UI")]
        [SerializeField] private GameObject[] ui = null;
        [SerializeField] private Image[] bonusFill = null;
        [SerializeField] private Text[] bonusText = null;

        [Header("Setting panel")]
        [SerializeField] private GameObject settingPanel = null;

        [Header("Scenes")]
        [SerializeField] private string menuSceneName = "";

        private void Awake()
        {
            timer.SetActive(false);
        }

        public void UpdateTimer(int timer)
        {
            this.timer.SetActive(true);
            timerText.text = timer.ToString();
        }

        public void UpdateScore(int playerID, int score)
        {
            scoreTexts[playerID].text = score.ToString();
        }

        public void SetUIState(int playerID, bool state)
        {
            ui[playerID].SetActive(state);
        }

        public void SetBonusState(int playerID, bool state)
        {
            bonusFill[playerID].gameObject.SetActive(state);
        }

        public void UpdateBonus(int playerID, float amount, string text)
        {
            bonusFill[playerID].fillAmount = amount;
            bonusText[playerID].text = text;
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