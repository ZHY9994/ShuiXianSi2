using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class CameraZoomData
	{
		public float Sensitivity;

		public Vector2 Limits;

		public CameraZoomData(float m_Sensitivity, Vector2 m_Limits)
		{
			Sensitivity = m_Sensitivity;
			Limits = m_Limits;
		}
	}
}
