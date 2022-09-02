using UnityEngine;
using System.Collections;
using Toolbox;

namespace Download
{
	public class BrinksSucursal : MonoBehaviour
	{
		[Header("Download")]
		public Download download = null;
		[Header("Truck door")]
		public GameObject door = null;
		[Header("Animations data")]
		public float enterAnimationDuration = 0;
		public float exitAnimationDuration = 0;

		private Animator animator = null;
		private Timer enterTimer = new Timer();
		private Timer exitTimer = new Timer();

		private void Awake()
		{
			animator = GetComponent<Animator>();
			enterTimer.SetTimer(enterAnimationDuration, Timer.TIMER_MODE.DECREASE);
			exitTimer.SetTimer(exitAnimationDuration, Timer.TIMER_MODE.DECREASE);
		}

		private void Update()
		{
			UpdateEnterTimer();
			UpdateExitTimer();

			if (Input.GetKeyDown(KeyCode.Z)) Enter();
			if (Input.GetKeyDown(KeyCode.X)) Exit();
		}

		public void Enter()
		{
			animator.Play("Enter");
			if (door) door.GetComponent<Animator>().Play("Open");
			enterTimer.ActiveTimer();
		}

		public void Exit()
		{
			animator.Play("Exit");
			if (door) door.GetComponent<Animator>().Play("Close");
			exitTimer.ActiveTimer();
		}

		/// <summary>
		/// Call "EndEnterAnimation" when the enter animation end
		/// </summary>
		private void UpdateEnterTimer()
		{
			if (enterTimer.Active) enterTimer.UpdateTimer();
			if (enterTimer.ReachedTimer()) download.EndEnterAnimation();
		}

		/// <summary>
		/// Call "EndExitAnimation" when the exit animation end
		/// </summary>
		private void UpdateExitTimer()
		{
			if (exitTimer.Active) exitTimer.UpdateTimer();
			if (exitTimer.ReachedTimer()) download.EndExitAnimation();
		}
	}
}