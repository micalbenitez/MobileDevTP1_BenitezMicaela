using UnityEngine;
using System.Collections;

namespace Entities.Items
{
	public class Pallet : MonoBehaviour
	{
		public enum VALUES
		{
			Value1 = 100000,
			Value2 = 250000,
			Value3 = 500000
		}

		public VALUES value = VALUES.Value1;
		public float time = 0;
		public GameObject CintaReceptora = null;
		public GameObject Portador = null;
		public float TiempEnCinta = 1.5f;
		public float TempoEnCinta = 0;

		public float TiempSmoot = 0.3f;
		public bool EnSmoot = false;

		private float TempoSmoot = 0;

		private void Start()
		{
			Pasaje();
		}

		private void LateUpdate()
		{
			if (Portador != null)
			{
				if (EnSmoot)
				{
					TempoSmoot += T.GetDT();

					if (TempoSmoot >= TiempSmoot)
					{
						EnSmoot = false;
						TempoSmoot = 0;
					}
					else
					{
						if (Portador.GetComponent<ManoRecept>() != null)
							transform.position = Portador.transform.position - Vector3.up * 1.2f;
						else
							transform.position = Vector3.Lerp(transform.position, Portador.transform.position, T.GetDT() * 10);
					}

				}
				else
				{
					if (Portador.GetComponent<ManoRecept>() != null)
						transform.position = Portador.transform.position - Vector3.up * 1.2f;
					else
						transform.position = Portador.transform.position;
				}
			}

		}

		public float GetBonus()
		{
			if (time > 0)
			{
				//calculo del bonus
			}
			return -1;
		}

		public void Pasaje()
		{
			EnSmoot = true;
			TempoSmoot = 0;
		}
	}
}