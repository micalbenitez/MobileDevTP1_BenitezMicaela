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

        /// Player keys
        [NonSerialized] public KeyCode right = KeyCode.None;
        [NonSerialized] public KeyCode left = KeyCode.None;
        [NonSerialized] public KeyCode up = KeyCode.None;

        /// <summary>
        /// Set player keys
        /// </summary>
        private void Start()
        {
            playerID = GetComponent<Player>().idPlayer;
            inputName += playerID;

            switch (input)
            {
                case INPUT.WASD:
                    right = KeyCode.D;
                    left = KeyCode.A;
                    up = KeyCode.W;
                    break;

                case INPUT.ARROWS:
                    right = KeyCode.RightArrow;
                    left = KeyCode.LeftArrow;
                    up = KeyCode.UpArrow;
                    break;

                default:
                    break;
            }
        }

        public string GetHorizontalInput()
        {
            return inputName;
        }
    }
}