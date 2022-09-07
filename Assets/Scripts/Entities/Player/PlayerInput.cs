using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public enum INPUT
        { 
            WASD,
            ARROWS,
            MOUSE
        }

        [Header("Movement data")]
        public INPUT input = INPUT.WASD;

        private int playerID = -1;
        private string inputName = "Horizontal";

        /// <summary>
        /// Set player keys
        /// </summary>
        private void Start()
        {
            playerID = GetComponent<Player>().idPlayer;
            inputName += playerID;
        }

        public string GetHorizontalInput()
        {
            return inputName;
        }

        public bool Up()
        {
            return InputManager.Instance.GetAxis("Vertical" + playerID) > 0.5f;
        }

        public bool Down()
        {
            return InputManager.Instance.GetAxis("Vertical" + playerID) < -0.5f;
        }

        public bool Left()
        {
            return InputManager.Instance.GetAxis("Horizontal" + playerID) < -0.5f;
        }

        public bool Right()
        {
            return InputManager.Instance.GetAxis("Horizontal" + playerID) > 0.5f;
        }
    }
}