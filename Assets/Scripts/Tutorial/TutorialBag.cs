using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialBag : MonoBehaviour
    {
        [Header("Bag positions")]
        public Vector3[] positions = null;

        /// Actual bag position
        private int bagPosition = 0;

        /// <summary>
        /// Call this to move the bag to the next position
        /// </summary>
        public void NextPosition()
        {
            transform.localPosition = positions[bagPosition];
            bagPosition++;
            if (bagPosition >= positions.Length) bagPosition = 0;
        }
    }
}