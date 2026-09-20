using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Screen_ExperienceLevelUp : MenuScreenBase
{
	[SerializeField]
	private ExtendedButton m_ClaimButton;

	[SerializeField]
	private TextMeshProUGUI m_ReachedLevelText;

	[SerializeField]
	private TextMeshProUGUI m_CashText;

	[SerializeField]
	private TextMeshProUGUI m_HPText;

	[SerializeField]
	private GameObject m_HpRoot;

	[SerializeField]
	private GameObject m_CashRoot;

	private string m_ReachedLevelTextFormat;

	protected override void Awake()
	{
		base.Awake();
		m_ReachedLevelTextFormat = m_ReachedLevelText.text;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		m_ClaimButton.Setup(new UnityAction(onButtonClicked));
		SoundManager.Instance.PlayGeneralSFXByType(eSFXType.LevelUp);
		updateVisuals();
	}

	private void onButtonClicked()
	{
		ExperienceManager.Instance.LevelUp();
		Close();
	}

	private void updateVisuals()
	{
		string level = LocalizationUtility.GetWordLocalizedString("lvl");
		m_ReachedLevelText.SetText(string.Format("{0} {1}", level, ExperienceManager.Instance.ExperienceLevel + 2));
		int healthIncrease = ExperienceManager.Instance.CurrentExperienceData.HealthIncreaseValue;
		int cashIncrease = ExperienceManager.Instance.CurrentExperienceData.CashIncreaseValue;
		m_HpRoot.SetActive(healthIncrease != 0);
		m_CashRoot.SetActive(cashIncrease != 0);
		m_HPText.SetText(string.Format("+{0} {1}", healthIncrease, LocalizationUtility.GetWordLocalizedString("hp")));
		m_CashText.SetText(string.Format("+{0}", cashIncrease));
	}
}
