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
        private string horizontalInputName = "Horizontal";
        private string verticalInputName = "Vertical";

        /// <summary>
        /// Set player keys
        /// </summary>
        private void Start()
        {
            playerID = GetComponent<Player>().idPlayer;
            horizontalInputName += playerID;
            verticalInputName += playerID;
        }

        public string GetHorizontalInput()
        {
            return horizontalInputName;
        }

        public string GetVerticalInput()
        {
            return verticalInputName;
        }
    }
}