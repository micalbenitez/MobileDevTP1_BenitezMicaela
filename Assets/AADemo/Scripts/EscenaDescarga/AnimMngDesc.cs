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
			Entrar();
		if(Input.GetKeyDown(KeyCode.X))
			Salir();
		
		switch(AnimAct)
		{
		case AnimEnCurso.Entrada:			
			if(!GetComponent<Animation>().IsPlaying("Entrada"))
			{
				AnimAct = AnimEnCurso.Nada;
				ContrDesc.FinAnimEntrada();
			}			
			break;
			
		case AnimEnCurso.Salida:			
			if(!GetComponent<Animation>().IsPlaying("Salida"))
			{
				AnimAct = AnimEnCurso.Nada;
				ContrDesc.FinAnimSalida();
			}			
			break;

		default:
			break;
		}
	}
	
	public void Entrar()
	{
		AnimAct = AnimEnCurso.Entrada;
		GetComponent<Animation>().Play("Entrada");
		
		if(PuertaAnimada != null)
		{
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = 0;
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = 1;
			PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
		}
	}
	
	public void Salir()
	{
		AnimAct = AnimEnCurso.Salida;	
		GetComponent<Animation>().Play("Salida");
		
		if(PuertaAnimada != null)
		{
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].time = PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].length;
			PuertaAnimada.GetComponent<Animation>()["AnimPuerta"].speed = -1;
			PuertaAnimada.GetComponent<Animation>().Play("AnimPuerta");
		}
	}
}
