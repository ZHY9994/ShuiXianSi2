using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim.UI
{
	public class DynamicCanvasScaler : MonoBehaviour
	{
		private class CanvasScalerResolutionData
		{
			private bool m_WasInit;

			private Vector2 m_DefaultValue;

			public Vector2 DefaultValue => m_DefaultValue;

			public Vector2 DefaultValueInverse => new Vector2(m_DefaultValue.y, m_DefaultValue.x);

			public void Init(Vector2 i_DefaultValue)
			{
				if (!m_WasInit) { m_DefaultValue = i_DefaultValue; m_WasInit = true; }
			}
		}

		[SerializeField]
		private CanvasScaler m_CanvasScaler;

		[SerializeField]
		private float m_MinAspect = 0.4618227f;

		[SerializeField]
		private float m_MaxAspect = 0.5622189f;

		[SerializeField]
		private float m_MaxAspectIpad = 0.75f;

		private CanvasScalerResolutionData m_CanvasScalerResolutionData = new CanvasScalerResolutionData();

		private int m_OldWidth;

		private int m_Oldheight;

		private Camera m_Camera;

		private bool m_IsPortain => true;

		private float m_CameraAspect => m_Camera == null ? 1f : m_Camera.aspect;

		private void Start()
		{
			if (m_CanvasScaler == null) m_CanvasScaler = GetComponent<CanvasScaler>();
			if (m_CanvasScalerResolutionData == null) m_CanvasScalerResolutionData = new CanvasScalerResolutionData();
			m_CanvasScalerResolutionData.Init(m_CanvasScaler.referenceResolution);
			m_Camera = Camera.main;
			Update();
		}

		private void Update()
		{
			if (m_CanvasScaler == null) return;
			if (Screen.width == m_OldWidth && Screen.height == m_Oldheight) return;
			m_OldWidth = Screen.width; m_Oldheight = Screen.height;
			float aspect = m_CameraAspect;
			if (aspect < m_MinAspect) m_CanvasScaler.referenceResolution = m_CanvasScalerResolutionData.DefaultValueInverse;
			else m_CanvasScaler.referenceResolution = m_CanvasScalerResolutionData.DefaultValue;
		}

		private void OnValidate()
		{
			if (m_CanvasScaler == null) m_CanvasScaler = GetComponent<CanvasScaler>();
		}
	}
}
