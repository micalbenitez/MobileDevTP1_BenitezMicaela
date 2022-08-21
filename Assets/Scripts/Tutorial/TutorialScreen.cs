using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

namespace Tutorial
{
    public class TutorialScreen : MonoBehaviour
    {
        [Header("Pre tutorial data")]
        public Texture2D[] preTutorialImages = null;
        public float timePerImage = 0;

        [Header("Tutorial data")]
        public Texture2D[] tutorialImages = null;

        /// If the tutorial is active or not
        private bool tutorialActive = false;
        /// Actual pre tutorial image
        private int preTutorialImage = 0;
        /// Actual tutorial image
        private int tutorialImage = 0;
        /// Timer
        private Timer timerPerImage = new Timer();

        /// Renderer parameter
        private MeshRenderer meshRenderer = null;

        /// <summary>
        /// Initialize private parameters
        /// </summary>
        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            timerPerImage.SetTimer(timePerImage, Timer.TIMER_MODE.DECREASE, true);
            meshRenderer.material.mainTexture = preTutorialImages[preTutorialImage];
        }

        /// <summary>
        /// Check timer per image
        /// </summary>
        private void Update()
        {
            if (!tutorialActive)
            {
                if (timerPerImage.Active) timerPerImage.UpdateTimer();
                if (timerPerImage.ReachedTimer()) NextPreTutorialImage();
            }
        }

        /// <summary>
        /// Call this to go to the next pre tutorial image
        /// </summary>
        private void NextPreTutorialImage()
        {
            preTutorialImage++;
            if (preTutorialImage >= preTutorialImages.Length) preTutorialImage = 0;
            meshRenderer.material.mainTexture = preTutorialImages[preTutorialImage];
            timerPerImage.ActiveTimer();
        }

        /// <summary>
        /// Call this to active the tutorial
        /// </summary>
        public void ActiveTutorial()
        {
            tutorialActive = true;
            meshRenderer.material.mainTexture = tutorialImages[tutorialImage];
        }

        /// <summary>
        /// Call this to go to the next tutorial image
        /// </summary>
        public void NextTutorialImage()
        {
            tutorialImage++;

            if (tutorialImage < tutorialImages.Length)  
                meshRenderer.material.mainTexture = tutorialImages[tutorialImage];
        }
    }
}