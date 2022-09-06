using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerData : MonoBehaviour
    {
        public PlayerData(int tipoDeInput, Player pj)
        {
            TipoDeInput = tipoDeInput;
            PJ = pj;
        }

        public bool FinCalibrado = false;
        public bool FinTuto1 = false;
        public bool FinTuto2 = false;

        public enum Visualizacion { Der, Izq }
        public Visualizacion LadoAct = Visualizacion.Der;

        public int TipoDeInput = -1;

        private Player PJ;

        private void Awake()
        {
            PJ = GetComponent<Player>();
        }
    }
}