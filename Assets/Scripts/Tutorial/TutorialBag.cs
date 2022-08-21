using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox.Lerpers;

namespace Tutorial
{
    public class TutorialBag : MonoBehaviour
    {
        [Header("Bag positions")]
        public Vector3[] positions = null;
        public float positionLerperSpeed = 0;

        /// Actual bag position
        private int bagPosition = 0;
        /// Position lerper
        private Vector3Lerper positionLerper = new Vector3Lerper();

        private void Awake()
        {
            transform.localPosition = positions[0];
        }

        private void Update()
        {
            UpdatePositionLerper();
        }

        private void UpdatePositionLerper()
        {
            if (positionLerper.Active)
            {
                positionLerper.UpdateLerper();
                transform.localPosition = positionLerper.GetValue();
            }
        }

        /// <summary>
        /// Call this to move the bag to the next position
        /// </summary>
        public void NextPosition()
        {
            positionLerper.SetLerperValues(transform.localPosition, positions[bagPosition], positionLerperSpeed, Lerper<Vector3>.LERPER_TYPE.STEP_SMOOTH, true);
            bagPosition++;
            if (bagPosition >= positions.Length) bagPosition = 0;
        }
    }
}