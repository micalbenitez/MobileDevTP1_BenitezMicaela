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
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}
	
	public void Iniciar()
	{
		//Pj.GetComponent<PlayerDownload>().RestaurarVel();
	}
	
	public void Finalizar()
	{
		Finalizado = true;
		GM.FinTutorial(Pj.idPlayer);
		//Pj.GetComponent<PlayerDownload>().Frenar();
		Pj.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Pj.EmptyInventory();
	}
}
