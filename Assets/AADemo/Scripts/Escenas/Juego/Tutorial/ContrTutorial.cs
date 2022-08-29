using UnityEngine;
using System.Collections;
using Entities.Player;

public class ContrTutorial : MonoBehaviour 
{
	public Player Pj;
	public float TiempTuto = 15;
	public float Tempo = 0;
	
	public bool Finalizado = false;
	
	GameManager GM;
	
	void Start () 
	{
		GM = GameObject.Find("GameMgr").GetComponent<GameManager>();
		
		Pj.ContrTuto = this;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}
	
	public void Iniciar()
	{
		Pj.GetComponent<Frenado>().RestaurarVel();
	}
	
	public void Finalizar()
	{
		Finalizado = true;
		GM.FinTutorial(Pj.idPlayer);
		Pj.GetComponent<Frenado>().Frenar();
		Pj.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Pj.EmptyInventory();
	}
}
