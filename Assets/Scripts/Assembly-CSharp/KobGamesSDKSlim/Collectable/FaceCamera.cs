using UnityEngine;

namespace KobGamesSDKSlim.Collectable
{
	public class FaceCamera : MonoBehaviour
	{
		private void OnEnable()
		{
			rotateTowardsCamera();
		}

		private void FixedUpdate()
		{
			rotateTowardsCamera();
		}

		private void rotateTowardsCamera()
		{
			Transform transform = this.transform;
			Transform cameraTransform = CameraManager.Instance.MainCamera.transform;
			transform.rotation = cameraTransform.rotation;
		}
	}
}
