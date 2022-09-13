using UnityEngine;
using System.Collections;
using Entities.Items;

public class EstantePartida : PalletManagement
{
	//public Cinta CintaReceptora;//cinta que debe recibir la bolsa
	public GameObject ManoReceptora;
	//public Pallet.Valores Valor;
	
	void OnTriggerEnter(Collider other)
	{
		PalletManagement recept = other.GetComponent<PalletManagement>();
		if(recept != null)
		{
			Give(recept);
		}
	}
	
	//------------------------------------------------------------//
	
	public override void Give(PalletManagement receptor)
	{
        if (receptor.Receive(pallets[0])) {
            pallets.RemoveAt(0);
        }
    }
	
	public override bool Receive (MoneyBagDownload pallet)
	{
		//pallet.CintaReceptora = CintaReceptora.gameObject;
		pallet.carrier = gameObject;
		return base.Receive (pallet);
	}
}
