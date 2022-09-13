using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

namespace Entities.Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        [Header("Respawn data")]
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
            if (timer.Active) timer.UpdateTimer();
            if (timer.ReachedTimer())
            {
                Physics.IgnoreLayerCollision(8, 9, false);
                GetComponent<CarController>().enabled = true;
            }

            /// Correct rotation
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
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

            if (GetComponent<PlayerData>().playerSide == PlayerData.PLAYER_SIDE.RIGHT)
                transform.position = checkpoint.position + checkpoint.right * Random.Range(rangeMinRight, rangeMaxRight);
            else
                transform.position = checkpoint.position + checkpoint.right * Random.Range(rangeMinRight * (-1), rangeMaxRight * (-1));

            transform.forward = checkpoint.forward;
            transform.rotation = Quaternion.identity;

            timer.ActiveTimer();
        }
    }
}