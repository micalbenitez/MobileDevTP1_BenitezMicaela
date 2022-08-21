using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolbox.Lerpers
{
    public class Vector3Lerper : Lerper<Vector3>
    {
        /// <summary>
        /// Call this to set vector3 lerper values, auto start if you want the timer to start or not yet
        /// </summary>
        public override void SetLerperValues(Vector3 start, Vector3 end, float time, LERPER_TYPE lerperType, bool autoStart = false)
        {
            SetValues(start, end, time, lerperType, autoStart);
        }

        /// <summary>
        /// Call this to get the vector3 lerper actual value
        /// </summary>
        public override Vector3 GetValue()
        {
            return currentValue;
        }

        /// <summary>
        /// Update vector3 lerper position
        /// </summary>
        protected override void UpdateCurrentPosition(float percentage)
        {
            currentValue = Vector3.Lerp(start, end, percentage);
        }

        /// <summary>
        /// Check if the vector3 lerper has reached or not
        /// </summary>
        protected override bool CheckReached()
        {
            if (currentValue == end) return true;
            else return false;
        }
    }
}

/* Test vector3 lerper:
public class test : MonoBehaviour
{
    public float time = 0;
    private Vector3Lerper vector3Lerper = new Vector3Lerper();

    private void Awake()
    {
        vector3Lerper.SetLerperValues(Vector3.zero, new Vector3(8, 15.4f, 40.1f), time, Lerper<Vector3>.LERPER_TYPE.STEP_SMOOTH, true);
    }

    private void Update()
    {
        if (vector3Lerper.Active) vector3Lerper.UpdateLerper();

        if (Input.GetKeyDown(KeyCode.M)) vector3Lerper.ActiveLerper();
        if (Input.GetKeyDown(KeyCode.N)) vector3Lerper.DesactiveLerper();

        Debug.Log(vector3Lerper.GetValue());
    }
}
 */