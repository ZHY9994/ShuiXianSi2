using System;
using UnityEngine;

[Serializable]
public class ScreenData
{
	private Vector2 m_OriginalRes;

	private float m_OriginalDPI;

	public float FinalDPI;

	private Vector2 m_ScreenSizeInch;

	private Vector2 m_ScreenSizeCm;

	private Vector2 m_ScalingVector;

	private float m_Scaling;

	public float ScreenDiagonalSizeInches
	{
		get
		{
			CalculateData();
			return Mathf.Sqrt(m_ScreenSizeInch.x * m_ScreenSizeInch.x + m_ScreenSizeInch.y * m_ScreenSizeInch.y);
		}
	}

	public Vector2 ScreenSizeInch => m_ScreenSizeInch;

	public void CalculateData(bool i_LogData = false)
	{
		m_OriginalRes = new Vector2(Screen.width, Screen.height);
		m_OriginalDPI = Screen.dpi;
		m_ScalingVector = Vector3.one;
		m_Scaling = (m_ScalingVector.x + m_ScalingVector.y) * 0.5f;
		FinalDPI = Screen.dpi == 0f ? 320f : Screen.dpi / m_Scaling;
		m_ScreenSizeInch = m_OriginalRes / FinalDPI;
		m_ScreenSizeCm = m_ScreenSizeInch * 2.54f;
	}
}
