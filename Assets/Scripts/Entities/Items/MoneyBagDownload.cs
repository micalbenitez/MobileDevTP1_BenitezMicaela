using UnityEngine;
using System.Collections;

namespace Entities.Items
{
	public class MoneyBagDownload : MonoBehaviour
	{
		public enum VALUES
		{
			Value1 = 100000,
			Value2 = 250000,
			Value3 = 500000
		}

		public VALUES value = VALUES.Value1;
		public float time = 0;
		public GameObject bandReceiving = null;
		public GameObject carrier = null;
		public float TiempEnCinta = 1.5f;
		public float TempoEnCinta = 0;

		public float TiempSmoot = 0.3f;
		public bool EnSmoot = false;

		private float TempoSmoot = 0;

		private void Start()
		{
			Passage();
		}

		private void LateUpdate()
		{
			if (carrier != null)
			{
				if (EnSmoot)
				{
					TempoSmoot += Time.deltaTime;

					if (TempoSmoot >= TiempSmoot)
					{
						EnSmoot = false;
						TempoSmoot = 0;
					}
					else
					{
						transform.position = Vector3.Lerp(transform.position, carrier.transform.position, Time.deltaTime * 10);
					}

				}
				else
				{
					transform.position = carrier.transform.position;
				}
			}

		}

		public float GetBonus()
		{
			if (time > 0)
			{
				/// Calculation of the bonus
			}
			return -1;
		}

		public void Passage()
		{
			EnSmoot = true;
			TempoSmoot = 0;
		}
	}
}