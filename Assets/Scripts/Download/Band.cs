using UnityEngine;
using System.Collections;
using Entities.Items;

namespace Download
{
	public class Band : ManejoPallets
	{
		[Header("Band data")]
		public float speed = 0;
		public Animator bandTubeAnimation = null;

		/// Pallet that is received
		private Transform pallet = null;

		private void Update()
		{
			PalletMovement();
		}

		private void PalletMovement()
        {
			for (int i = 0; i < Pallets.Count; i++)
			{
				if (Pallets[i].GetComponent<Renderer>().enabled)
				{
					if (!Pallets[i].GetComponent<Pallet>().EnSmoot)
					{
						Pallets[i].GetComponent<Pallet>().enabled = false;
						Pallets[i].TempoEnCinta += T.GetDT();

						Pallets[i].transform.position += transform.right * speed * T.GetDT();
						Vector3 vAux = Pallets[i].transform.localPosition;
						vAux.y = 3.61f; /// specific height
						Pallets[i].transform.localPosition = vAux;

						if (Pallets[i].TempoEnCinta >= Pallets[i].TiempEnCinta)
						{
							Pallets[i].TempoEnCinta = 0;
							pallet.gameObject.SetActive(false);
						}
					}
				}
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			ManejoPallets recept = other.GetComponent<ManejoPallets>();
			if (recept != null) Dar(recept);
		}

		public override bool Recibir(Pallet pallet)
		{
			Controlador.ArrivePallet();
			pallet.Portador = gameObject;
			this.pallet = pallet.transform;
			base.Recibir(pallet);
			TurnOff();

			return true;
		}

		public void TurnOn()
		{
			bandTubeAnimation.SetBool("On", true);
		}
		public void TurnOff()
		{
			bandTubeAnimation.SetBool("On", false);
		}
	}
}