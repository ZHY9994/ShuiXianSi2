using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
	[SerializeField]
	private int m_FlagIndex;

	[SerializeField]
	private Image m_FlagIcon;

	public Sprite GetIcon => m_FlagIcon.sprite;

	public int SpriteIndex => m_FlagIndex;

	public void SetFlag(Sprite i_Flag, int i_Index)
	{
		m_FlagIcon.sprite = i_Flag;
		m_FlagIndex = i_Index;
	}

	private void SetRefs()
	{
		m_FlagIcon = GetComponent<Image>();
	}
}
