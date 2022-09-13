using UnityEngine;
using System.Collections;
using Entities.Items;

public class EstanteLlegada : PalletManagement
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
	
	public override bool Receive(MoneyBagDownload moneyBagDownload)
	{
        moneyBagDownload.carrier = gameObject;
        base.Receive(moneyBagDownload);
        ContrCalib.FinTutorial();

        return true;
    }
}
