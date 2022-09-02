using UnityEngine;
using System.Collections;
using Entities.Player;

namespace Download
{
	public class Deposit : MonoBehaviour
	{
		public Download downloadPlayer1 = null;
		public Download downloadPlayer2 = null;

		private bool empty = true;
		private Player player = null;
		private Collider[] depositCollider = null;

		/// Properties
		public bool Empty { get => empty; }

		void Start()
		{
			Physics.IgnoreLayerCollision(8, 9, false);
		}

		void Update()
		{
			if (!empty)
			{
				player.transform.position = transform.position;
				player.transform.forward = transform.forward;
			}
		}

		public void Enter(Player player)
		{
			if (player.WithMoneyBags())
			{
				this.player = player;

				depositCollider = this.player.GetComponentsInChildren<Collider>();

				for (int i = 0; i < depositCollider.Length; i++)
					depositCollider[i].enabled = false;

				this.player.GetComponent<Rigidbody>().useGravity = false;

				this.player.transform.position = transform.position;
				this.player.transform.forward = transform.forward;

				empty = false;

				Physics.IgnoreLayerCollision(8, 9, true);

				if (this.player.idPlayer == 0) downloadPlayer1.Active(this);
				else downloadPlayer2.Active(this);
			}
		}

		public void Exit()
		{
			player.EmptyInventory();

			player.GetComponent<Rigidbody>().useGravity = true;

			for (int i = 0; i < depositCollider.Length; i++)
				depositCollider[i].enabled = true;

			Physics.IgnoreLayerCollision(8, 9, false);

			player = null;
			empty = true;
		}
	}
}