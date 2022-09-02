using UnityEngine;
using System.Collections;
using Entities.Items;

public class ManejoPallets : MonoBehaviour
{
	public Download.Download download = null;

	protected System.Collections.Generic.List<Pallet> pallets = new System.Collections.Generic.List<Pallet>();
	protected int counter = 0;
	
	public virtual bool Receive(Pallet pallet)
	{
		Debug.Log(gameObject.name + " / Receive()");
		pallets.Add(pallet);
		pallet.Pasaje();
		return true;
	}
	
	public virtual void Give(ManejoPallets receptor)
	{
		/// Here is where is the charge of deciding whether or not to give him the bag
	}

	public bool Possession()
	{
		if (pallets.Count != 0) return true;
		else return false;
	}
}
