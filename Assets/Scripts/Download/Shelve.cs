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
		public PalletManager pallets = null;
		[Header("Pallets value")]
		public Pallet.VALUES Valor = Pallet.VALUES.Value1;

		private void OnTriggerEnter(Collider other)
		{
			ManejoPallets recept = other.GetComponent<ManejoPallets>();
			if (recept != null) Dar(recept);
		}

		public override void Dar(ManejoPallets receptor)
		{
			if (Tenencia())
			{
				if (receptor.Recibir(Pallets[0]))
				{
					//enciende la cinta y el indicador
					//cambia la textura de cuantos pallet le queda
					band.TurnOn();
					Controlador.TakeOutPallet();
					Pallets[0].GetComponent<Renderer>().enabled = true;
					Pallets.RemoveAt(0);
					pallets.TakeOut();
					TurnOffAnimation();
				}
			}
		}

		public override bool Recibir(Pallet pallet)
		{
			pallet.CintaReceptora = band.gameObject;
			pallet.Portador = this.gameObject;
			pallets.Add();
			pallet.GetComponent<Renderer>().enabled = false;
			return base.Recibir(pallet);
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