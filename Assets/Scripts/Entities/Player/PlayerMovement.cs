using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public enum MOVE_TYPE
        { 
            WASD,
            ARROWS
        }

        [Header("Movement data")]
        public MOVE_TYPE moveType = MOVE_TYPE.WASD;

        /// Player keys
        [NonSerialized] public KeyCode right = KeyCode.None;
        [NonSerialized] public KeyCode left = KeyCode.None;
        [NonSerialized] public KeyCode up = KeyCode.None;

        /// <summary>
        /// Set player keys
        /// </summary>
        private void Start()
        {
            switch (moveType)
            {
                case MOVE_TYPE.WASD:
                    right = KeyCode.D;
                    left = KeyCode.A;
                    up = KeyCode.W;
                    break;

                case MOVE_TYPE.ARROWS:
                    right = KeyCode.RightArrow;
                    left = KeyCode.LeftArrow;
                    up = KeyCode.UpArrow;
                    break;

                default:
                    break;
            }
        }
    }
}