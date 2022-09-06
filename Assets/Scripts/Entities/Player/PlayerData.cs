using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerData : MonoBehaviour
    {
        public PlayerData(int tipoDeInput, Player player)
        {
            input = tipoDeInput;
            this.player = player;
        }

        public enum PLAYER_SIDE
        {
            RIGHT,
            LEFT
        }
        public PLAYER_SIDE playerSide = PLAYER_SIDE.RIGHT;

        public bool FinCalibrado = false;
        public bool FinTuto1 = false;
        public bool FinTuto2 = false;

        public int input = -1;
        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }
    }
}