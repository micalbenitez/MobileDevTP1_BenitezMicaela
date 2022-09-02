using UnityEngine;
using System.Collections;
using Entities.Items;

public class EstanteLlegada : ManejoPallets
{

	public GameObject Mano;
	public ContrCalibracion ContrCalib;
	
	//-----------------------------------------------//

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	//--------------------------------------------------//
	
	public override bool Receive(Pallet p)
	{
        p.Portador = this.gameObject;
        base.Receive(p);
        ContrCalib.FinTutorial();

        return true;
    }
}
