using UnityEngine;
using System.Collections;

public class PalletManager : MonoBehaviour
{
	public System.Collections.Generic.List<GameObject> moneyBagsInTruck = new System.Collections.Generic.List<GameObject>();

	private int actualQuantity = 0;

	private void Start()
	{
		for (int i = 0; i < moneyBagsInTruck.Count; i++)
		{
			moneyBagsInTruck[i].GetComponent<Renderer>().enabled = false;
		}
	}

	public void TakeOut()
	{
		moneyBagsInTruck[actualQuantity - 1].GetComponent<Renderer>().enabled = false;
		actualQuantity--;
	}

	public void Add()
	{
		actualQuantity++;
		moneyBagsInTruck[actualQuantity - 1].GetComponent<Renderer>().enabled = true;
	}
}