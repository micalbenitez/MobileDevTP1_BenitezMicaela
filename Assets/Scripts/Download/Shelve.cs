using UnityEngine;
using System.Collections;
using Entities.Items;

namespace Download
{
	public class Shelve : ManejoPallets
	{
		[Header("Shelve animation")]
		public Animator shelveFloorAnimation = null;
		[Header("Band")]
		public Band band = null;
		[Header("Pallets manager")]
		public PalletManager palletManager = null;
		[Header("Pallets value")]
		public Pallet.VALUES Valor = Pallet.VALUES.Value1;

		private void OnTriggerEnter(Collider other)
		{
			ManejoPallets recept = other.GetComponent<ManejoPallets>();
			if (recept != null) Give(recept);
		}

		public override void Give(ManejoPallets receptor)
		{
			if (Possession())
			{
				if (receptor.Receive(pallets[0]))
				{
					/// Turn on the tape and indicator
					band.TurnOn();
					download.TakeOutPallet();
					pallets[0].GetComponent<Renderer>().enabled = true;
					pallets.RemoveAt(0);
					palletManager.TakeOut();
					TurnOffAnimation();
				}
			}
		}

		public override bool Receive(Pallet pallet)
		{
			pallet.CintaReceptora = band.gameObject;
			pallet.Portador = this.gameObject;
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