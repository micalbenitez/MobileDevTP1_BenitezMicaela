using System;
using System.Collections;
using UnityEngine;

namespace Entities.Items
{
	public class MoneyBag : MonoBehaviour
	{
		[Header("Money bag data")]
		public Pallet.VALUES value = Pallet.VALUES.Value2;
		public MeshRenderer meshRenderer = null;
		public GameObject particles = null;

		public Action<Vector3> OnDestroy = null;

		private void Start()
		{
			if (particles) particles.SetActive(false);
		}

        private void OnTriggerEnter(Collider other)
        {
			if (other.CompareTag("Player"))
			{
				Player.Player player = other.GetComponent<Player.Player>();

				if (player.AddMoneyBag(this))
				{
					gameObject.SetActive(false);
					OnDestroy?.Invoke(transform.position);
				}
			}
		}
	}
}