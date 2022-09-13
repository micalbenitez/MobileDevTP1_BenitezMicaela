using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Entities.Player;

namespace UI
{
    public class UIPlayer : MonoBehaviour
    {
        [Header("Player")]
        public Player player = null;

        [Header("Images")]
        public Sprite[] scoreImages = null;

        private Image image = null;
        private Animator animator = null;

        private void Awake()
        {
            image = GetComponent<Image>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (player.CurrentTotalMoneyBags == 0) image.sprite = scoreImages[0];
            else if (player.CurrentTotalMoneyBags == 1) image.sprite = scoreImages[1];
            else if (player.CurrentTotalMoneyBags == 2) image.sprite = scoreImages[2];
            else if (player.CurrentTotalMoneyBags == 3) image.sprite = scoreImages[3];

            if (player.CurrentTotalMoneyBags == 3) animator.enabled = true;
            else animator.enabled = false;
        }
    }
}