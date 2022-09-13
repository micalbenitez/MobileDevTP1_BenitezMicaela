using UnityEngine;
using System.Collections;
using Entities.Items;

public class PalletManagement : MonoBehaviour
{
	public Download.Download download = null;

	protected System.Collections.Generic.List<MoneyBagDownload> pallets = new System.Collections.Generic.List<MoneyBagDownload>();
	protected int counter = 0;
	
	public virtual bool Receive(MoneyBagDownload pallet)
	{
		Debug.Log(gameObject.name + " / Receive()");
		pallets.Add(pallet);
		pallet.Passage();
		return true;
	}
	
	public virtual void Give(PalletManagement receptor)
	{
		/// Here is where is the charge of deciding whether or not to give him the bag
	}

	public bool Possession()
	{
		if (pallets.Count != 0) return true;
		else return false;
	}
}
