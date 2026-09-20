using UnityEngine;

public class Background : MonoBehaviour
{
	public enum eBackgroundPart
	{
		Front = 0,
		Mid = 1,
		Back = 2
	}

	[SerializeField]
	private Color m_SkyColor;

	[SerializeField]
	private bool m_UseFog;

	[SerializeField]
	private BackgroundPart[] m_BGParts;

	public Color SkyColor => m_SkyColor;

	public bool UseFog => m_UseFog;

	public void ResetPositions()
	{
		foreach (BackgroundPart bgPart in m_BGParts)
		{
			bgPart.transform.position = Vector3.zero;
		}
	}

	public void MoveParts()
	{
		foreach (BackgroundPart bgPart in m_BGParts)
		{
			bgPart.MovePart();
		}
	}

	public void BGVisibility(bool i_Value)
	{
		gameObject.SetActive(i_Value);
	}

	private void SetRefs()
	{
		m_BGParts = GetComponentsInChildren<BackgroundPart>(true);
	}
}
