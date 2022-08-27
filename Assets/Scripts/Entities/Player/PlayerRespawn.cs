using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        private Vector3 respawnPosition = Vector3.zero;

        private void Awake()
        {
            respawnPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Checkpoint")) Checkpoint(other.transform.position.z);

            if (other.CompareTag("Limit")) Respawn();
        }

        private void Checkpoint(float posZ)
        {
            respawnPosition = new Vector3(respawnPosition.x, respawnPosition.y, posZ);
        }

        private void Respawn()
        {
            transform.position = respawnPosition;
            transform.rotation = Quaternion.identity;
        }
    }
}