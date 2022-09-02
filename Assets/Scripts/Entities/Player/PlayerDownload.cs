using UnityEngine;
using System.Collections;
using Download;

namespace Entities.Player
{
	public class PlayerDownload : MonoBehaviour
	{
		public bool isBraking = false;

		private Player player = null;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Deposit"))
			{
				Deposit deposit = other.GetComponent<Deposit>();
				if (deposit)
				{
					if (deposit.Empty && player.WithMoneyBags()) deposit.Enter(player);
				}
			}
		}
	}
}