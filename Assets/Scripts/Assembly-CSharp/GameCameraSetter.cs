using UnityEngine;
using KobGamesSDKSlim;

public class GameCameraSetter : MonoBehaviour
{
	[SerializeField]
	private Transform m_FollowPoint;

	[SerializeField]
	[Range(10f, 25f)]
		private float m_Distance = 11f;

	private void onValueChagned()
	{
		setGameplayCamera();
	}

	private void OnEnable()
	{
		setGameplayCamera();
		m_FollowPoint.gameObject.SetActive(false);
	}

	private void setGameplayCamera()
	{
		if (Application.isPlaying)
			CameraManager.Instance.SetGameplayCamera(m_FollowPoint, m_Distance);
	}
}
