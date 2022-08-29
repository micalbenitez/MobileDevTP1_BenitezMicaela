using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Entities.Player;

namespace UI
{
    public class UIGame : MonoBehaviour
    {
        [Header("Score UI")]
        public Text[] scoreTexts = null;

        public void UpdateScore(int playerID, int score)
        {
            scoreTexts[playerID].text = score.ToString();
        }
    }
}