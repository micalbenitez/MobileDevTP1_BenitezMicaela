using UnityEngine;
using System.Collections;

/// <summary>
/// basicamente lo que hace es que viaja en linea recta y ocacionalmente gira para un cosatado
/// previamente verificado, tambien cuando llega al final del recorrido se reinicia en la pos. orig.
/// </summary>

namespace Entities.Obstacle
{
	public class Taxi : MonoBehaviour
	{
		enum SIDE 
		{ 
			RIGHT, 
			LEFT 
		}

		public string endTaxiTag = "FinTaxi";
		public string limitTag = "Terreno";
		public float speed = 0;
		public Vector2 timeHowOftenTurn_MaxMin = Vector2.zero;
		public float turnDuration = 0;
		public float scopeVerified = 0;
		public string groundTag = "Terreno";
		public bool turning = false;
		public float turningAngle = 30;

		private Vector3 initialRotation = Vector3.zero;
		private Vector3 initialPosition = Vector3.zero;
		private RaycastHit raycast;
		private float turnDurationTimer = 0;
		private float tiempEntreGiro = 0;
		private float tempoEntreGiro = 0;
		private float tiempPGiro = 1; /// 1 es el tiempo que tarda en llegar al otro quaternion
		private bool respawn = false;

		private void Start()
		{
			tiempEntreGiro = Random.Range(timeHowOftenTurn_MaxMin.x, timeHowOftenTurn_MaxMin.y);
			initialRotation = transform.localEulerAngles;
			initialPosition = transform.position;
		}

		private void FixedUpdate()
		{
			transform.position += transform.forward * Time.fixedDeltaTime * speed;
		}

		private void Update()
		{
			if (respawn)
			{
				if (Measurement()) Respawn();
			}
			else
			{
				if (turning)
				{
					turnDurationTimer += Time.deltaTime;
					if (turnDurationTimer > turnDuration)
					{
						turnDurationTimer = 0;
						StopTurning();
					}
				}
				else
				{
					tempoEntreGiro += Time.deltaTime;
					if (tempoEntreGiro > tiempEntreGiro)
					{
						tempoEntreGiro = 0;
						Turn();
					}
				}
			}
		}

		private void OnTriggerEnter(Collider collision)
		{
			if (collision.tag == endTaxiTag)
			{
				transform.position = initialPosition;
				transform.localEulerAngles = initialRotation;
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.transform.tag == limitTag) respawn = true;
		}

		private bool SideVerification(SIDE lado)
		{
			switch (lado)
			{
				case SIDE.RIGHT:
					if (Physics.Raycast(transform.position, transform.right, out raycast, scopeVerified))
					{
						if (raycast.transform.tag == groundTag) return false;
					}
					break;

				case SIDE.LEFT:
					if (Physics.Raycast(transform.position, transform.right * (-1), out raycast, scopeVerified))
					{
						if (raycast.transform.tag == groundTag) return false;
					}
					break;
			}

			return true;
		}

		private void Turn()
		{
			turning = true; /// Choose a side
			SIDE side = SIDE.RIGHT;
			if (Random.Range(0, 2) == 0)
			{
				side = SIDE.LEFT; /// Check, if it does not give it changes to the right
				if (!SideVerification(side)) side = SIDE.RIGHT;
			}
			else
			{
				side = SIDE.RIGHT; /// Check, if it does not give it changes to the left
				if (!SideVerification(side)) side = SIDE.LEFT;
			}

			if (side == SIDE.RIGHT)
			{
				Vector3 vaux = transform.localEulerAngles;
				vaux.y += turningAngle;
				transform.localEulerAngles = vaux;
			}
			else
			{
				Vector3 vaux = transform.localEulerAngles;
				vaux.y -= turningAngle;
				transform.localEulerAngles = vaux;
			}
		}

		private void StopTurning()
		{
			turning = false;
			tiempEntreGiro = Random.Range(timeHowOftenTurn_MaxMin.x, timeHowOftenTurn_MaxMin.y);

			transform.localEulerAngles = initialRotation;
		}

		private bool Measurement()
		{
			float dist1 = (GameManager.Instancia.Player1.transform.position - initialPosition).magnitude;
			float dist2 = (GameManager.Instancia.Player2.transform.position - initialPosition).magnitude;

			if (dist1 > 4 && dist2 > 4) return true;
			else return false;
		}

		private void Respawn()
		{
			respawn = false;

			transform.position = initialPosition;
			transform.localEulerAngles = initialRotation;
		}
	}
}