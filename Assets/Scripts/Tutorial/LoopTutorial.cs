using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Tutorial
{
	public class LoopTutorial : MonoBehaviour
	{
		[Header("Tutorial images")]
		public float durationPerImage = 0;
		public Sprite[] images = null;

		private Image image = null;
		private float timer = 0;
		private int index = 0;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Start()
		{
			if (images.Length > 0) image.sprite = images[0];
		}

		private void Update()
		{
			timer += Time.deltaTime;

			if (timer >= durationPerImage)
			{
				timer = 0;
				index++;
				if (index >= images.Length) index = 0;
				image.sprite = images[index];
			}
		}
	}
}