using UnityEngine;
using System.Collections;
using Entities.Items;

public class ManoRecept : PalletManagement 
{
	public bool TengoPallet = false;
	
	void FixedUpdate () 
	{
		TengoPallet = Possession();
	}
	
	void OnTriggerEnter(Collider other)
	{
		PalletManagement recept = other.GetComponent<PalletManagement>();
		if(recept != null)
		{
			Give(recept);
		}
		
	}
	
	//---------------------------------------------------------//	
	
	public override bool Receive(Pallet pallet)
	{
		if(!Possession())
		{
			pallet.Portador = this.gameObject;
			base.Receive(pallet);
			return true;
		}
		else
			return false;
	}
	
	public override void Give(PalletManagement receptor)
	{
		//Debug.Log(gameObject.name+ " / Dar()");
		switch (receptor.tag)
		{
		case "Mano":
			if(Possession())
			{
				//Debug.Log(gameObject.name+ " / Dar()"+" / Tenencia=true");
				if(receptor.name == "Right Hand")
				{
					if(receptor.Receive(pallets[0]))
					{
						//Debug.Log(gameObject.name+ " / Dar()"+" / Tenencia=true"+" / receptor.Recibir(Pallets[0])=true");
						pallets.RemoveAt(0);
						//Debug.Log("pallet entregado a Mano de Mano");
					}
				}
				
			}
			break;
			
		case "Cinta":
			if(Possession())
			{
				if(receptor.Receive(pallets[0]))
				{
					pallets.RemoveAt(0);
					//Debug.Log("pallet entregado a Cinta de Mano");
				}
			}
			break;
			
		case "Estante":
			break;
		}
	}
}
