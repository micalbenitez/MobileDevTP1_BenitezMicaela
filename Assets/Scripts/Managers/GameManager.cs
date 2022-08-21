using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game cameras")]
        public Camera cameraPlayer1 = null;
        public Camera cameraPlayer2 = null;

        public void StartGame()
        {
            cameraPlayer1.gameObject.SetActive(true);
            cameraPlayer2.gameObject.SetActive(true);
        }
    }
}