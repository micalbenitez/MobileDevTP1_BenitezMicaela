using UnityEngine;
using System.Collections;
using Entities.Items;

namespace Download
{
	public class Shelve : PalletManagement
	{
		[Header("Shelve animation")]
		public Animator shelveFloorAnimation = null;
		[Header("Band")]
		public Band band = null;
		[Header("Pallets manager")]
		public PalletManager palletManager = null;
		[Header("Pallets value")]
		public MoneyBagDownload.VALUES Valor = MoneyBagDownload.VALUES.Value1;

		private void OnTriggerEnter(Collider other)
		{
			PalletManagement recept = other.GetComponent<PalletManagement>();
			if (recept != null) Give(recept);
		}

		public override void Give(PalletManagement receptor)
		{
			if (Possession())
			{
				if (download.GetPalletEnMov() == null)
				{
					if (receptor.Receive(pallets[0]))
					{
						/// Turn on the tape and indicator
						band.TurnOn();
						download.TakeOutPallet(pallets[0]);
						pallets[0].GetComponent<Renderer>().enabled = true;
						pallets.RemoveAt(0);
						palletManager.TakeOut();
						TurnOffAnimation();
					}
				}
			}
		}

		public override bool Receive(MoneyBagDownload pallet)
		{
			pallet.bandReceiving = band.gameObject;
			pallet.carrier = this.gameObject;
			palletManager.Add();
			pallet.GetComponent<Renderer>().enabled = false;
			return base.Receive(pallet);
		}

		public void TurnOnAnimation()
		{
			shelveFloorAnimation.SetBool("On", true);
		}

		public void TurnOffAnimation()
		{
			shelveFloorAnimation.SetBool("On", false);
		}
	}
}