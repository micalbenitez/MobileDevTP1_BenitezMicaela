using UnityEngine;

namespace Entities.camera
{
	public class CarCamera : MonoBehaviour
	{
		[Header("Target to follow")]
		public Transform target = null;

		[Header("Camera data")]
		public float height = 1f;
		public float velocityDamping = 3f;
		public float distance = -4;
		public LayerMask ignoreLayers = -1;

		private RaycastHit hit = new RaycastHit();
		private Vector3 prevVelocity = Vector3.zero;
		private Vector3 currentVelocity = Vector3.zero;
		private LayerMask raycastLayers = -1;

		void Start()
		{
			raycastLayers = ~ignoreLayers;
		}

		void FixedUpdate()
		{
			currentVelocity = Vector3.Lerp(prevVelocity, target.root.GetComponent<Rigidbody>().velocity, velocityDamping * Time.deltaTime);
			currentVelocity.y = 0;
			prevVelocity = currentVelocity;
		}

		void LateUpdate()
		{
			float speedFactor = Mathf.Clamp01(target.root.GetComponent<Rigidbody>().velocity.magnitude / 70.0f);
			GetComponent<Camera>().fieldOfView = Mathf.Lerp(55, 72, speedFactor);
			float currentDistance = Mathf.Lerp(7.5f, 6.5f, speedFactor);

			currentVelocity = currentVelocity.normalized;

			Vector3 newTargetPosition = target.position + Vector3.up * height;
			Vector3 newPosition = newTargetPosition - (currentVelocity * currentDistance);
			newPosition.y = newTargetPosition.y;

			Vector3 targetDirection = newPosition - newTargetPosition;
			if (Physics.Raycast(newTargetPosition, targetDirection, out hit, currentDistance, raycastLayers))
				newPosition = hit.point;

			newPosition += transform.forward * distance; // Difference in z

			transform.position = newPosition;
			transform.LookAt(newTargetPosition);

			// Rotation
			Vector3 vAux = transform.rotation.eulerAngles;
			vAux.x = 20;
			transform.eulerAngles = vAux;
		}
	}
}