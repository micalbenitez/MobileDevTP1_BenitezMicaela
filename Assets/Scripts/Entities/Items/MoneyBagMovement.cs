using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Items;
using Entities.Player;

namespace Entities.Items
{
    public class MoneyBagMovement : PalletManagement
    {
        public PalletManagement shelve = null;
        public PalletManagement band = null;

        [Header("Player data")]
        public PlayerInput playerInput = null;

        private bool secondStepComplete = false;

        private void Update()
        {
            if (!Possession() && shelve.Possession() && InputManager.Instance.GetLeftButton(playerInput.GetHorizontalInput()))
            {
                FirstStep();
            }
            else if (Possession() && InputManager.Instance.GetUpButton(playerInput.GetVerticalInput()))
            {
                SecondStep();
            }
            else if (secondStepComplete && Possession() && InputManager.Instance.GetRightButton(playerInput.GetHorizontalInput()))
            {
                ThirdStep();
            }
        }

        private void FirstStep()
        {
            shelve.Give(this);
            secondStepComplete = false;
        }

        private void SecondStep()
        {
            pallets[0].transform.position = transform.position;
            secondStepComplete = true;
        }

        private void ThirdStep()
        {
            Give(band);
            secondStepComplete = false;
        }

        public override void Give(PalletManagement receptor)
        {
            if (Possession())
            {
                if (receptor.Receive(pallets[0]))
                {
                    pallets.RemoveAt(0);
                }
            }
        }

        public override bool Receive(MoneyBagDownload pallet)
        {
            if (!Possession())
            {
                pallet.carrier = gameObject;
                base.Receive(pallet);
                return true;
            }
            else
                return false;
        }
    }
}