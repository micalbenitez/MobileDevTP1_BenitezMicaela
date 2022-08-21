using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Player;
using Entities.Items;

namespace Tutorial
{
    public class Tutorial : MonoBehaviour
    {
        public enum STEPS
        {
            PRETUTORIAL,
            STEP1,
            STEP2,  
            STEP3,
            STEP4
        }

        [Header("Tutorial screen")]
        public TutorialScreen tutorialScreen = null;
        [Header("Player data")]
        public PlayerMovement playerMovement = null;
        [Header("Bag data")]
        public Bag bag = null;  
        public Vector3 positionStep1 = Vector3.zero;
        public Vector3 positionStep2 = Vector3.zero;
        public Vector3 positionStep3 = Vector3.zero;

        /// Actual tutorial step
        private STEPS steps = STEPS.PRETUTORIAL;

        /// <summary>
        /// Update tutorial
        /// </summary>
        private void Update()
        {
            switch (steps)
            {
                case STEPS.PRETUTORIAL:
                    ActiveTutorial(playerMovement.up);
                    break;

                case STEPS.STEP1:
                    NextTutorialStep(playerMovement.left, positionStep1);
                    break;

                case STEPS.STEP2:
                    NextTutorialStep(playerMovement.up, positionStep2);
                    break;

                case STEPS.STEP3:
                    NextTutorialStep(playerMovement.right, positionStep3);
                    break;

                case STEPS.STEP4:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Active tutorial
        /// </summary>
        private void ActiveTutorial(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                tutorialScreen.ActiveTutorial();
                steps++;
            }
        }

        /// <summary>
        /// Next tutorial step
        /// </summary>
        private void NextTutorialStep(KeyCode key, Vector3 bagPosition)
        {
            if (Input.GetKeyDown(key))
            {
                tutorialScreen.NextTutorialImage();
                bag.transform.localPosition = bagPosition;
                steps++;
            }
        }
    }
}