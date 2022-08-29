using UnityEngine;
using System;
using Entities.Items;

namespace Entities.Player
{
	public class Player : MonoBehaviour
	{
		public enum STATES 
		{ 
			Download, 
			Driving, 
			Calibration, 
			Tutorial
		}

		public STATES state = STATES.Driving;

		public int money = 0;
		public int idPlayer = 0;

		public MoneyBag[] moneyBags;

		public bool isDriving = true;
		public bool isDownloading = false;

		public ControladorDeDescarga ContrDesc;
		public ContrCalibracion ContrCalib;
		public ContrTutorial ContrTuto;

		private int currentTotalMoneyBags = 0;

		public Action<int, int> OnUpdateScore = null;

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

		public void SetContrDesc(ControladorDeDescarga contr)
		{
			ContrDesc = contr;
		}

		public ControladorDeDescarga GetContr()
		{
			return ContrDesc;
		}

		public void CambiarACalibracion()
		{
			state = Player.STATES.Calibration;
		}

		public void CambiarATutorial()
		{
			state = Player.STATES.Tutorial;
			ContrTuto.Iniciar();
		}

		public void CambiarAConduccion()
		{
			state = Player.STATES.Driving;
		}

		public void CambiarADescarga()
		{
			state = Player.STATES.Download;
		}
	}
}