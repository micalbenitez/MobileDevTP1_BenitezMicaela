using UnityEngine;
using System.Collections;
using Entities.Items;

namespace Download
{
	public class Band : PalletManagement
	{
		[Header("Band data")]
		public float speed = 0;
		public Animator bandTubeAnimation = null;

		private const float specificHeight = 3.61f;
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
					if (!pallets[i].GetComponent<MoneyBagDownload>().EnSmoot)
					{
						pallets[i].GetComponent<MoneyBagDownload>().enabled = false;
						pallets[i].TempoEnCinta += Time.deltaTime;

						pallets[i].transform.position += transform.right * (speed * Time.deltaTime);
						Vector3 vAux = pallets[i].transform.localPosition;
						vAux.y = specificHeight;
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
			PalletManagement recept = other.GetComponent<PalletManagement>();
			if (recept != null) Give(recept);
		}

		public override bool Receive(MoneyBagDownload pallet)
		{
			download.ArrivePallet();
			pallet.carrier = gameObject;
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