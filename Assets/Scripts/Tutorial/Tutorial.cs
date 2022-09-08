using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Entities.Player;
using Toolbox;
using Managers;

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
                    ActiveTutorial(playerInput.Up());
                    break;

                case STEPS.STEP1:
                    NextTutorialStep(playerInput.Left());
                    break;

                case STEPS.STEP2:
                    NextTutorialStep(playerInput.Up());
                    break;

                case STEPS.STEP3:
                    NextTutorialStep(playerInput.Right());
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