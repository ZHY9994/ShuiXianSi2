using UnityEngine;
using UnityEngine.UI;

namespace KG.Game.UI
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
				if (m_WasInit) return;
				m_DefaultValue = i_DefaultValue;
				m_WasInit = true;
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

		private float m_CameraAspect
		{
			get
			{
				if (m_Camera == null) m_Camera = Camera.main;
				return m_Camera == null ? 1f : m_Camera.aspect;
			}
		}

		private void Start()
		{
			m_Camera = Camera.main;
		}

		private void Update()
		{
			if (m_Camera == null) return;
			if (m_OldWidth == m_Camera.pixelWidth && m_Oldheight == m_Camera.pixelHeight) return;
			m_OldWidth = m_Camera.pixelWidth;
			m_Oldheight = m_Camera.pixelHeight;
			if (m_CanvasScaler == null || m_CanvasScalerResolutionData == null) return;
			m_CanvasScalerResolutionData.Init(m_CanvasScaler.referenceResolution);
			m_CanvasScaler.referenceResolution = m_CanvasScalerResolutionData.DefaultValue;
			float aspect = m_CameraAspect;
			float value;
			float offset;
			if (aspect <= m_MaxAspect)
			{
				value = m_MinAspect == m_MaxAspect ? 0f : Mathf.Clamp01((aspect - m_MinAspect) / (m_MaxAspect - m_MinAspect));
				offset = 0f;
			}
			else
			{
				value = m_MaxAspect == m_MaxAspectIpad ? 0f : Mathf.Clamp01((aspect - m_MaxAspect) / (m_MaxAspectIpad - m_MaxAspect));
				offset = 0.5f;
			}
			m_CanvasScaler.matchWidthOrHeight = Mathf.Max(0f, value) * 0.5f + offset;
		}

		private void OnValidate()
		{
			if (m_CanvasScaler == null) m_CanvasScaler = GetComponent<CanvasScaler>();
		}
	}
}
