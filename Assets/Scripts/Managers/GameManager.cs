using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game camera")]
        public Camera gameCamera = null;

        public void StartGame()
        {
            gameCamera.gameObject.SetActive(true);
        }
    }
}