using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;

public class Screen_Welcome : MenuScreenBase
{
	[SerializeField]
	private GameObject m_SettingsButton;

	[SerializeField]
	private GameObject m_VibrationButton;

	[SerializeField]
	private ExtendedButton m_TapToStart;

	[SerializeField]
	private PlayerInfo m_PlayerInfo;

	protected override void OnEnable()
	{
		base.OnEnable();
		if (m_TapToStart == null || m_SettingsButton == null || m_VibrationButton == null)
			SetRefs();
		m_TapToStart.Setup(OnTapToStart);
		SetSettingsButton();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += OnLevelStarted;
		Screen_Transition.OnHideComplete += invokeLevelStart;
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_MenuButtons");
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= OnLevelStarted;
		Screen_Transition.OnHideComplete -= invokeLevelStart;
	}

	private void showTransitionScreen()
	{
		Screen_Transition.TransitionMode = Screen_Transition.eTransitionMode.HideAndShow;
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_Transition");
	}

	private void OnLevelStarted()
	{
		Close();
	}

	private void OnTapToStart()
	{
		if (ExperienceManager.Instance.IsBlock)
		{
			return;
		}
		if (Wayne.Game.Shops.SkinShopManager.Instance.IsShopOpened)
		{
			return;
		}
		if (!Wayne.Game.Shops.WeaponShopManager.Instance.IsShopOpened)
		{
			ADManager.Instance.ShowBlackAd();
			ADManager.Instance.ShowBox();
			showTransitionScreen();
			KobGamesSDKSlim.SoundManager.Instance.PlayGeneralSFXByType(eSFXType.PlayButtonClick);
		}
	}

	private void invokeLevelStart()
	{
		KobGamesSDKSlim.GameManager.Instance.LevelStarted();
	}

	public override void Reset()
	{
		SetSettingsButton();
	}

	private void SetSettingsButton()
	{
		if (m_SettingsButton == null || m_VibrationButton == null || GameConfig.Instance == null || GameConfig.Instance.Menus == null)
			return;
		m_SettingsButton.SetActive(GameConfig.Instance.Menus.UseScreenSettings);
		m_VibrationButton.SetActive(!GameConfig.Instance.Menus.UseScreenSettings);
	}

	public override void Close()
	{
		base.Close();
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.CloseMenuScreen("Screen_MenuButtons");
	}

	protected override void SetRefs()
	{
		base.SetRefs();
		ExtendedButton tapToStart = transform.FindDeepChild<ExtendedButton>("Tap To Start Button");
		if (tapToStart != null) m_TapToStart = tapToStart;
		GameObject settingsButton = transform.FindDeepChild<GameObject>("SettingsButton");
		if (settingsButton == null) settingsButton = GameObject.Find("SettingsButton");
		if (settingsButton != null) m_SettingsButton = settingsButton;
		GameObject vibrationButton = transform.FindDeepChild<GameObject>("VibrationButton");
		if (vibrationButton == null) vibrationButton = GameObject.Find("VibrationButton");
		if (vibrationButton != null) m_VibrationButton = vibrationButton;
		PlayerInfo playerInfo = GetComponentInChildren<PlayerInfo>();
		if (playerInfo != null) m_PlayerInfo = playerInfo;
	}
}
