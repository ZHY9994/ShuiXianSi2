using MrWayne;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KobGamesSDKSlim;

public class MatchMakingItem : MonoBehaviour
{
	[SerializeField]
	private Image m_CharacterImage;

	[SerializeField]
	private TextMeshProUGUI m_NameText;

	[SerializeField]
	private TextMeshProUGUI m_RankText;

	[SerializeField]
	private Image m_FlagIcon;

	private AIVisualData.AISkinData m_AISkinData;

	public Sprite CharacterSprite
	{
		get
		{
			return m_CharacterImage.sprite;
		}
		set
		{
			m_CharacterImage.sprite = value;
		}
	}

	public string CharacterName
	{
		get
		{
			return m_NameText.text;
		}
		set
		{
			m_NameText.text = value;
		}
	}

	public Sprite FlagIcon => m_FlagIcon.sprite;

	public string Rank
	{
		get
		{
			return m_RankText.text;
		}
		set
		{
			m_RankText.text = value;
		}
	}

	public AIVisualData.AISkinData AISkinData => m_AISkinData;

	public void RandomizeCharacter()
	{
		m_AISkinData = GameConfig.Instance.GamePlay.UnitData.AI.Visuals.GetRandomSkinData();
		m_CharacterImage.sprite = m_AISkinData.HealthBarIcon;
		m_NameText.text = GameConfig.Instance.GamePlay.MatchMakingParameters.m_Names[Random.Range(0, GameConfig.Instance.GamePlay.MatchMakingParameters.m_Names.Length)];
		m_FlagIcon.sprite = GameConfig.Instance.GamePlay.MatchMakingParameters.Flags[Random.Range(0, GameConfig.Instance.GamePlay.MatchMakingParameters.Flags.Length)];
		m_RankText.text = Random.Range(2, 100).ToString();
	}

	private void SetRefs()
	{
		m_CharacterImage = transform.FindDeepChild<Image>("Character Image");
		m_NameText = transform.FindDeepChild<TextMeshProUGUI>("Name Text");
		m_RankText = transform.FindDeepChild<TextMeshProUGUI>("Rank Text");
		m_FlagIcon = transform.FindDeepChild<Image>("Flag Icon");
	}
}
