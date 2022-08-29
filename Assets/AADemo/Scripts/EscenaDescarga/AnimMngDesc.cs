using UnityEngine;
using System.Collections;

public class AnimMngDesc : MonoBehaviour 
{
	public Download ContrDesc;
	
	enum AnimEnCurso{Salida,Entrada,Nada}
	AnimEnCurso AnimAct = AnimEnCurso.Nada;
	
	public GameObject PuertaAnimada;
	
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Z))
			Enter();
		if(Input.GetKeyDown(KeyCode.X))
			Exit();
		
		switch(AnimAct)
		{
		case AnimEnCurso.Entrada:
			if(!GetComponent<Animator>().IsPlaying("Enter")
			{
				AnimAct = AnimEnCurso.Nada;
				ContrDesc.FinAnimEntrada();
			}			
			break;
			
		case AnimEnCurso.Salida:			
			if(!GetComponent<Animator>().IsPlaying("Exit")
			{
				AnimAct = AnimEnCurso.Nada;
				ContrDesc.FinAnimSalida();
			}			
			break;

		default:
			break;
		}
	}
	
	public void Enter()
	{
		AnimAct = AnimEnCurso.Entrada;
		GetComponent<Animator>().Play("Enter");
		if (PuertaAnimada) PuertaAnimada.GetComponent<Animator>().Play("Open");
	}
	
	public void Exit()
	{
		AnimAct = AnimEnCurso.Salida;	
		GetComponent<Animator>().Play("Exit");
		if (PuertaAnimada) PuertaAnimada.GetComponent<Animator>().Play("Close");
	}
}
