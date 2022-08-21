using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

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

        [Header("Player data")]
        public PlayerMovement playerMovement = null;
        [Header("Tutorial screen")]
        public TutorialScreen tutorialScreen = null;

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
                    NextTutorialStep(playerMovement.left);
                    break;

                case STEPS.STEP2:
                    NextTutorialStep(playerMovement.up);
                    break;

                case STEPS.STEP3:
                    NextTutorialStep(playerMovement.right);
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
        private void NextTutorialStep(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                tutorialScreen.NextTutorialImage();
                steps++;
            }
        }
    }
}