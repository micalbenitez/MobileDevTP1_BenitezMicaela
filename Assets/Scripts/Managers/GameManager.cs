using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Player;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game cameras")]
        public Camera cameraPlayer1 = null;
        public Camera cameraPlayer2 = null;

        [Header("Car controller")]
        public CarController carController1 = null;
        public CarController carController2 = null;

        private void Awake()
        {
            cameraPlayer1.gameObject.SetActive(false);
            cameraPlayer2.gameObject.SetActive(false);
            carController1.enabled = false;
            carController2.enabled = false;
        }

        public void StartGame()
        {
            cameraPlayer1.gameObject.SetActive(true);
            cameraPlayer2.gameObject.SetActive(true);
            carController1.enabled = true;
            carController2.enabled = true;
        }
    }
}