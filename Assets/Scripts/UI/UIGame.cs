using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Entities.Player;

namespace UI
{
    public class UIGame : MonoBehaviour
    {
        [Header("Game UI")]
        public GameObject timer = null;
        public Text timerText = null;
        public Text[] scoreTexts = null;

        [Header("Download UI")]
        public GameObject[] ui = null;
        public Image[] bonusFill = null;
        public Text[] bonusText = null;

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
    }
}