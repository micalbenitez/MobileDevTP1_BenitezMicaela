using UnityEngine;
using System.Collections;

namespace Entities.Obstacle
{
	public class Obstacle : MonoBehaviour
	{
		public float speedReduction = 0;
		public float activeTime = 3;
		public float timeDisappearing = 2;
		public string PlayerTag = "Player";

		private float timer1 = 0;
		private float timer2 = 0;
		private bool crashed = false;
		private bool disappear = false;

		void Update()
		{
			if (crashed)
			{
				timer1 += Time.deltaTime;
				if (timer1 > activeTime)
				{
					crashed = false;
					disappear = true;
					GetComponent<Rigidbody>().useGravity = false;
					GetComponent<Collider>().enabled = false;
				}
			}

			if (disappear) /// Disappear animation
			{
				timer2 += Time.deltaTime;
				if (timer2 > timeDisappearing) gameObject.SetActive(false);
			}
		}

		void OnCollisionEnter(Collision coll)
		{
			if (coll.transform.tag == PlayerTag) crashed = true;
		}

		protected virtual void Desaparecer() { }

		protected virtual void Colision() { }
	}
}