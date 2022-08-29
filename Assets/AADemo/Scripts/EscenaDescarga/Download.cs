using UnityEngine;
using System.Collections;
using Entities.Player;
using Entities.Items;

public class Download : MonoBehaviour
{
	public Player player = null;
	public GameObject scene = null;	
	public GameObject downloadCamera = null;
	
	//los prefab de los pallets
	public Pallet pallet = null;
	
	public Estanteria estanteria;

	public Band Cin2;	
	public float Bonus = 0;		
	public AnimMngDesc ObjAnimado;

	private int Contador = 0;
	private Deposit deposit;

	private void Start () 
	{
		scene.SetActive(false);
		downloadCamera.SetActive(false);
		if (ObjAnimado) ObjAnimado.ContrDesc = this;
	}
			
	public void Active(Deposit d)
	{
		deposit = d;//recibe el deposito para que sepa cuando dejarlo ir al camion
		scene.SetActive(false);
		downloadCamera.SetActive(false);
		player.CambiarADescarga();		
		
		//asigna los pallets a las estanterias
		for(int i = 0; i < player.moneyBags.Length; i++)
		{
			if(player.moneyBags[i] != null)
			{
				Pallet palletGO = Instantiate(pallet.gameObject).GetComponent<Pallet>();
				if (palletGO)
				{
					if (player.moneyBags[i].value == Pallet.VALUES.Value1) palletGO.value = Pallet.VALUES.Value1;
					else if (player.moneyBags[i].value == Pallet.VALUES.Value2) palletGO.value = Pallet.VALUES.Value2;
					else if (player.moneyBags[i].value == Pallet.VALUES.Value3) palletGO.value = Pallet.VALUES.Value3;

					estanteria.Recibir(palletGO);
				}

				Contador++;
			}
		}

		//animacion
		ObjAnimado.Enter();		
	}
	
	//cuando sale de un estante
	public void SalidaPallet(Pallet p)
	{
		player.TakeOutOneMoneyBag();
		//inicia el contador de tiempo para el bonus
	}
	
	//cuando llega a la cinta
	public void LlegadaPallet(Pallet p)
	{
		//termina el contador y suma los pts
		
		//termina la descarga
		Contador--;
		
		player.money += (int)Bonus;
		
		if(Contador <= 0)
		{
			Finalizacion();
		}
		else
		{
			estanteria.EncenderAnim();
		}
	}
	
	public void FinDelJuego()
	{
		//metodo llamado por el GameManager para avisar que se termino el juego
		
		//desactiva lo que da y recibe las bolsas para que no halla mas flujo de estas
		estanteria.enabled = false;
		Cin2.enabled = false;
	}

	private void Finalizacion()
	{
		ObjAnimado.Exit();
	}
	
	public void FinAnimEntrada()
	{
		//avisa cuando termino la animacion para que prosiga el juego
		estanteria.EncenderAnim();
	}
	
	public void FinAnimSalida()
	{
		//avisa cuando termino la animacion para que prosiga el juego
		scene.SetActive(false);
		downloadCamera.SetActive(false);
		player.CambiarAConduccion();		
		deposit.Exit();		
	}
	
}
