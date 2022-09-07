using UnityEngine;
using System.Collections;

public class PlayerObstacles : MonoBehaviour
{
	public float timeWait = 0.5f;
	public float timeNoCollision = 1;

	private float timer1 = 0;
	private float timer2 = 0;

	private enum COLLIDE 
	{ 
		ConTodo, 
		EspDesact, 
		SinObst 
	}
	private COLLIDE collide = COLLIDE.ConTodo;

	private void Start()
	{
		Physics.IgnoreLayerCollision(8, 10, false);
	}

	private void Update()
	{
		switch (collide)
		{
			case COLLIDE.ConTodo:
				break;

			case COLLIDE.EspDesact:
				timer1 += Time.deltaTime;
				if (timer1 >= timeWait)
				{
					timer1 = 0;
					IgnorarColls(true);
				}
				break;

			case COLLIDE.SinObst:
				timer2 += Time.deltaTime;
				if (timer2 >= timeNoCollision)
				{
					timer2 = 0;
					IgnorarColls(false);
				}
				break;
		}
	}

	private void ColisionConObst()
	{
		switch (collide)
		{
			case COLLIDE.ConTodo:
				collide = PlayerObstacles.COLLIDE.EspDesact;
				break;

			case COLLIDE.EspDesact:
				break;

			case COLLIDE.SinObst:
				break;
		}
	}

	private void IgnorarColls(bool state)
	{
		if (name == "Camion1") Physics.IgnoreLayerCollision(8, 10, state);
		else Physics.IgnoreLayerCollision(9, 10, state);

		if (state) collide = COLLIDE.SinObst;
		else collide = COLLIDE.ConTodo;
	}

	private void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Obstaculo") ColisionConObst();
	}
}
