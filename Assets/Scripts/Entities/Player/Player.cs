using UnityEngine;
using System;
using Entities.Items;

namespace Entities.Player
{
	public class Player : MonoBehaviour
	{
		public enum STATES
		{
			Tutorial,
			Driving,
			Download
		}

		public STATES state = STATES.Tutorial;

		public int money = 0;
		public int idPlayer = 0;

		public MoneyBag[] moneyBags;

		public bool isDriving = true;
		public bool isDownloading = false;

		private int currentTotalMoneyBags = 0;

		public Action<int, int> OnUpdateScore = null;

		/// Properties
        public int CurrentTotalMoneyBags { get => currentTotalMoneyBags; }

        public bool AddMoneyBag(MoneyBag moneyBag)
		{
			if (currentTotalMoneyBags + 1 <= moneyBags.Length)
			{
				moneyBags[currentTotalMoneyBags] = moneyBag;
				currentTotalMoneyBags++;
				money += (int)moneyBag.value;
				OnUpdateScore?.Invoke(idPlayer, money);
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool WithMoneyBags()
		{
			for (int i = 0; i < moneyBags.Length; i++)
			{
				if (moneyBags[i]) return true;
			}
			return false;
		}

		public void TakeOutOneMoneyBag()
		{
			for (int i = 0; i < moneyBags.Length; i++)
			{
				if (moneyBags[i])
				{
					moneyBags[i] = null;
					return;
				}
			}
		}

		public void EmptyInventory()
		{
			for (int i = 0; i < moneyBags.Length; i++)
				moneyBags[i] = null;

			currentTotalMoneyBags = 0;
		}

		public void ChangePlayerState(STATES state)
        {
			this.state = state;
        }
	}
}