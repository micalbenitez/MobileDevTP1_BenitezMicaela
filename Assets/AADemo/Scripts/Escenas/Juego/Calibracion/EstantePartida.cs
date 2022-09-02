using UnityEngine;
using System.Collections;
using Entities.Items;

public class EstantePartida : ManejoPallets
{
	//public Cinta CintaReceptora;//cinta que debe recibir la bolsa
	public GameObject ManoReceptora;
	//public Pallet.Valores Valor;
	
	void OnTriggerEnter(Collider other)
	{
		ManejoPallets recept = other.GetComponent<ManejoPallets>();
		if(recept != null)
		{
			Give(recept);
		}
	}
	
	//------------------------------------------------------------//
	
	public override void Give(ManejoPallets receptor)
	{
        if (receptor.Receive(pallets[0])) {
            pallets.RemoveAt(0);
        }
    }
	
	public override bool Receive (Pallet pallet)
	{
		//pallet.CintaReceptora = CintaReceptora.gameObject;
		pallet.Portador = gameObject;
		return base.Receive (pallet);
	}
}
