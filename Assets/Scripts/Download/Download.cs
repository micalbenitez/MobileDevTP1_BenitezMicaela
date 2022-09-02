using UnityEngine;
using System.Collections;
using Entities.Player;
using Entities.Items;

namespace Download
{
	public class Download : MonoBehaviour
	{
		[Header("Player")]
		public Player player = null;

		[Header("Download scene and camera")]
		public GameObject scene = null;
		public GameObject downloadCamera = null;

		[Header("Download objects")]
		public Pallet pallet = null;
		public Shelve shelve = null;
		public Band band = null;
		public BrinksSucursal brinksSucursal = null;

		[Header("Paller bonus")]
		public float bonus = 0;

		private int counter = 0;
		private Deposit deposit = null;

		private void Start()
		{
			scene.SetActive(false);
			downloadCamera.SetActive(false);
			if (brinksSucursal) brinksSucursal.download = this;
		}

		public void Active(Deposit deposit)
		{
			this.deposit = deposit; /// Receive the deposit so you know when to let it go to the truck
			scene.SetActive(true);
			downloadCamera.SetActive(true);
			player.ChangePlayerState(Player.STATES.Download);

			/// Assign the pallets to the racks
			for (int i = 0; i < player.moneyBags.Length; i++)
			{
				if (player.moneyBags[i] != null)
				{
					Pallet palletGO = Instantiate(pallet.gameObject).GetComponent<Pallet>();
					if (palletGO)
					{
						if (player.moneyBags[i].value == Pallet.VALUES.Value1) palletGO.value = Pallet.VALUES.Value1;
						else if (player.moneyBags[i].value == Pallet.VALUES.Value2) palletGO.value = Pallet.VALUES.Value2;
						else if (player.moneyBags[i].value == Pallet.VALUES.Value3) palletGO.value = Pallet.VALUES.Value3;

						shelve.Recibir(palletGO);
					}

					counter++;
				}
			}

			brinksSucursal.Enter();
		}

		/// <summary>
		/// When the pallet leaves the truck
		/// </summary>
		public void TakeOutPallet()
		{
			player.TakeOutOneMoneyBag();
			//inicia el contador de tiempo para el bonus
		}

		/// <summary>
		/// When the pallet arrives at the band
		/// </summary>
		public void ArrivePallet()
		{
			//termina el contador y suma los pts

			counter--;

			player.money += (int)bonus;

			if (counter <= 0) EndDownload();
			else shelve.TurnOnAnimation();
		}

		private void EndDownload()
		{
			brinksSucursal.Exit();
		}

		public void EndEnterAnimation()
		{
			shelve.TurnOnAnimation();
		}

		public void EndExitAnimation()
		{
			scene.SetActive(false);
			downloadCamera.SetActive(false);
			player.ChangePlayerState(Player.STATES.Driving);
			deposit.Exit();
		}

		/// <summary>
		/// Deactivate the shelve and the band so that there is no more flow of pallets
		/// </summary>
		public void EndGame()
		{
			/// Method called by the GameManager to notify that the game is over
			shelve.enabled = false;
			band.enabled = false;
		}
	}
}