using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Entities.Player;
using Toolbox;

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
        [Header("Bag")]
        public TutorialBag tutorialBag = null;
        [Header("Finish tutorial")]
        public float timeToStartGame = 0;
        public UnityEvent finishTutorial = null;

        [Header("Other tutorial")]
        public Tutorial otherTutorial = null;

        /// Actual tutorial step
        private STEPS steps = STEPS.PRETUTORIAL;
        /// Timer to start the game
        public Timer timer = new Timer();

        /// <summary>
        /// Initialize timer
        /// </summary>
        private void Awake()
        {
            timer.SetTimer(timeToStartGame, Timer.TIMER_MODE.DECREASE);
        }

        /// <summary>
        /// Update tutorial
        /// </summary>
        private void Update()
        {
            StartGame();

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
                    if (!timer.Active && otherTutorial.steps == STEPS.STEP4) timer.ActiveTimer();
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
                tutorialBag.NextPosition();
                steps++;
            }
        }

        /// <summary>
        /// Start the game
        /// </summary>
        private void StartGame()
        {
            if (timer.Active) timer.UpdateTimer();
            if (timer.ReachedTimer())
            {
                finishTutorial?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}