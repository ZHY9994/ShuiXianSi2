using Cinemachine;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class CameraFOVSize : MonoBehaviour
	{
		[SerializeField]
		private CinemachineVirtualCamera m_CinemachineVirtualCamera;

		public float MinWH = 0.56f;

		public float FOV = 60f;

		private int m_WidthOld;

		private int m_HeightOld;

		private float m_FOVOriginal = -1f;

		private void Start()
		{
			if (m_FOVOriginal < 0f) m_FOVOriginal = FOV;
			Update();
		}

		public float GetFov()
		{
			if (m_FOVOriginal < 0f) m_FOVOriginal = FOV;
			float aspect = (float)m_WidthOld / m_HeightOld;
			return aspect < MinWH ? m_FOVOriginal * MinWH / aspect : m_FOVOriginal;
		}

		private void Update()
		{
			if (Screen.width == m_WidthOld && Screen.height == m_HeightOld) return;
			m_WidthOld = Screen.width;
			m_HeightOld = Screen.height;
			initIfNeed();
			float aspect = (float)m_WidthOld / m_HeightOld;
			float fov = aspect < MinWH ? m_FOVOriginal * MinWH / aspect : m_FOVOriginal;
			m_CinemachineVirtualCamera.m_Lens.FieldOfView = fov;
		}

		private void initIfNeed()
		{
			if (m_FOVOriginal < 0f) m_FOVOriginal = FOV;
		}

		private void OnValidate()
		{
			if (m_CinemachineVirtualCamera == null)
				m_CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
		}
	}
}
