using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        [Header("Car data")]
        public float speed = 20f;

        [Header("Turn data")]
        public float turnSensitive = 1f;
        public float maxAngle = 45f;

        [Header("Wheels data")]
        public List<AxleData> axleData = new List<AxleData>();

        /// Horizontal input
        private float inputX = 0;

        private void FixedUpdate()
        {
            GetInput();
            Move();
        }

        private void GetInput()
        {
            inputX = Input.GetAxis("Horizontal0");
        }

        private void Move()
        {
            foreach (AxleData data in axleData)
            {
                if (data.isBack)
                {
                    data.rightWheel.motorTorque = speed * 500 * Time.deltaTime;
                    data.leftWheel.motorTorque = speed * 500 * Time.deltaTime;
                }
                if (data.isFront)
                {
                    var steerAngle = inputX * turnSensitive * maxAngle;
                    data.rightWheel.steerAngle = Mathf.Lerp(data.rightWheel.steerAngle, steerAngle, 0.5f);
                    data.leftWheel.steerAngle = Mathf.Lerp(data.leftWheel.steerAngle, steerAngle, 0.5f);
                }

                AnimateWheel(data.rightWheel, data.visualRightWheel);
                AnimateWheel(data.leftWheel, data.visualLeftWheel);
            }
        }

        private void AnimateWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Quaternion rotation = Quaternion.identity;
            Vector3 position = Vector3.zero;
            Vector3 wheelRotation = Vector3.zero;

            wheelCollider.GetWorldPose(out position, out rotation);
            wheelTransform.transform.rotation = rotation;
        }
    }
}

[System.Serializable]
public class AxleData
{
    public WheelCollider rightWheel = null;
    public WheelCollider leftWheel = null;

    public Transform visualRightWheel = null;
    public Transform visualLeftWheel = null;

    public bool isBack = false;
    public bool isFront = false;
}