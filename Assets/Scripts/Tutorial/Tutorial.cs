using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public PlayerInput playerInput = null;
        [Header("Bag")]
        public TutorialBag tutorialBag = null;
        [Header("Finish tutorial")]
        public float timeToStartGame = 0;

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
        public void PlayTutorial()
        {
            switch (steps)
            {
                case STEPS.PRETUTORIAL:
                    ActiveTutorial(InputManager.Instance.GetUpButton(playerInput.GetVerticalInput()));
                    break;

                case STEPS.STEP1:
                    NextTutorialStep(InputManager.Instance.GetLeftButton(playerInput.GetHorizontalInput()));
                    break;

                case STEPS.STEP2:
                    NextTutorialStep(InputManager.Instance.GetUpButton(playerInput.GetVerticalInput()));
                    break;

                case STEPS.STEP3:
                    NextTutorialStep(InputManager.Instance.GetRightButton(playerInput.GetHorizontalInput()));
                    break;

                case STEPS.STEP4:
                    if (GameConfiguration.Instance.GetPlayers() == GameConfiguration.GAME_MODE.SINGLEPLAYER)
                    {
                        if (!timer.Active) timer.ActiveTimer();
                    }
                    else
                    {
                        if (!timer.Active && otherTutorial.steps == STEPS.STEP4) timer.ActiveTimer();
                    }
                    break;
                default:
                    break;
            }

            if (timer.Active) timer.UpdateTimer();
        }

        /// <summary>
        /// Active tutorial
        /// </summary>
        private void ActiveTutorial(bool state)
        {
            if (state)
            {
                tutorialScreen.ActiveTutorial();
                steps++;
            }
        }

        /// <summary>
        /// Next tutorial step
        /// </summary>
        private void NextTutorialStep(bool state)
        {
            if (state)
            {
                tutorialScreen.NextTutorialImage();
                tutorialBag.NextPosition();
                steps++;
            }
        }

        /// <summary>
        /// Start the game
        /// </summary>
        public bool FinishTutorial()
        {         
            if (timer.ReachedTimer()) return true;
            else return false;
        }
    }
}