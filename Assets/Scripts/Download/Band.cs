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
			for (int i = 0; i < pallets.Count; i++)
			{
				if (pallets[i].GetComponent<Renderer>().enabled)
				{
					if (!pallets[i].GetComponent<Pallet>().EnSmoot)
					{
						pallets[i].GetComponent<Pallet>().enabled = false;
						pallets[i].TempoEnCinta += T.GetDT();

						pallets[i].transform.position += transform.right * speed * T.GetDT();
						Vector3 vAux = pallets[i].transform.localPosition;
						vAux.y = 3.61f; /// specific height
						pallets[i].transform.localPosition = vAux;

						if (pallets[i].TempoEnCinta >= pallets[i].TiempEnCinta)
						{
							pallets[i].TempoEnCinta = 0;
							pallet.gameObject.SetActive(false);
						}
					}
				}
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			ManejoPallets recept = other.GetComponent<ManejoPallets>();
			if (recept != null) Give(recept);
		}

		public override bool Receive(Pallet pallet)
		{
			download.ArrivePallet();
			pallet.Portador = gameObject;
			this.pallet = pallet.transform;
			base.Receive(pallet);
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