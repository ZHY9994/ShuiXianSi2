using KobGamesSDKSlim;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthItem : MonoBehaviour
{
	[SerializeField]
	private Image m_CharacterIcon;

	[SerializeField]
	private Image m_FlagIcon;

	[SerializeField]
	private ProgressBar m_ProgressBar;

	[SerializeField]
	private TextMeshProUGUI m_CharacterName;

	public ProgressBar ProgressBar => m_ProgressBar;

	public void SetHealthProgress(float i_Value)
	{
		m_ProgressBar.SetProgress(i_Value);
	}

	public void ChangeItemProperties(Sprite i_Sprite, string i_Name, Sprite i_Flag)
	{
		m_CharacterIcon.sprite = i_Sprite;
		m_CharacterName.text = i_Name;
		m_FlagIcon.sprite = i_Flag;
	}

	private void SetRefs()
	{
		m_CharacterIcon = transform.FindDeepChild<Image>("Character Icon");
		m_ProgressBar = transform.FindDeepChild<ProgressBar>("Progress Bar");
		m_CharacterName = transform.FindDeepChild<TextMeshProUGUI>("Character Name");
		m_FlagIcon = transform.FindDeepChild<Image>("Flag Icon");
	}
}
