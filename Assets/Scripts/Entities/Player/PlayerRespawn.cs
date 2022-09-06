using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

namespace Entities.Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        [Header("Respawn data")]
        public int frameCheck = 0;
        public float rangeMinRight = 0;
        public float rangeMaxRight = 0;

        private CarController carController = null;
        private Rigidbody rigidBody = null;
        private Transform checkpoint = null;
        private Timer timer = new Timer();

        private void Awake()
        {
            carController = GetComponent<CarController>();  
            rigidBody = GetComponent<Rigidbody>();
            timer.SetTimer(1, Timer.TIMER_MODE.DECREASE);
        }

        private void Update()
        {
            if (Time.deltaTime % frameCheck == 0)
                transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);

            if (timer.Active) timer.UpdateTimer();
            if (timer.ReachedTimer())
            {
                Physics.IgnoreLayerCollision(8, 9, false);
                GetComponent<CarController>().enabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Checkpoint")) checkpoint = other.transform;

            if (other.CompareTag("Limit")) Respawn();
        }

        private void Respawn()
        {
            carController.enabled = false;
            rigidBody.Sleep();
            Physics.IgnoreLayerCollision(8, 9, true);

            if (GetComponent<PlayerData>().LadoAct == PlayerData.Visualizacion.Der)
                transform.position = checkpoint.position + checkpoint.right * Random.Range(rangeMinRight, rangeMaxRight);
            else
                transform.position = checkpoint.position + checkpoint.right * Random.Range(rangeMinRight * (-1), rangeMaxRight * (-1));

            transform.forward = checkpoint.forward;
            transform.rotation = Quaternion.identity;

            timer.ActiveTimer();
        }
    }
}