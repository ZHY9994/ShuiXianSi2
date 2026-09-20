using System;
using System.Collections;
using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using KobGamesSDKSlim.UI;
using UnityEngine;

public class Screen_Settings : MenuScreenBase
{
	[SerializeField]
	private ToggleButton2[] m_Toggles;

	[SerializeField]
	private ExtendedButton m_CloseButton;

	[SerializeField]
	private ExtendedButton m_RestorePurchaseButton;

	[SerializeField]
	private ExtendedButton m_PrivacyButton;

	[SerializeField]
	private ExtendedButton m_ConsentButton;

	[SerializeField]
	private LoadingMessage m_Message;

	private bool check;

	protected override void SetRefs()
	{
		base.SetRefs();
		m_Toggles = GetComponentsInChildren<ToggleButton2>();
		m_CloseButton = transform.FindDeepChild<ExtendedButton>("CloseButton");
		m_PrivacyButton = transform.FindDeepChild<ExtendedButton>("Privacy Button");
		m_ConsentButton = transform.FindDeepChild<ExtendedButton>("Consent Button");
		m_RestorePurchaseButton = transform.FindDeepChild<ExtendedButton>("Restore Purchase Button");
	}

	protected override void OnEnable()
	{
		m_PrivacyButton.gameObject.SetActive(UCM.UserConsentManager.Instance.NeedShowCmpButton());
		base.OnEnable();
		m_CloseButton.Setup(Close);
		m_RestorePurchaseButton.Setup(onRestorePurchaseClick);
		m_PrivacyButton.Setup(onPrivacyClicked);
		m_ConsentButton.Setup(onConsentClicked);
		updateToggles();
	}

	public override void Close()
	{
		base.Close();
		MenuManager.Instance.OpenMenuScreen("Screen_Welcome", true);
		HUDManager.Instance.Open(true);
	}

	private void onRestorePurchaseClick()
	{
		iAPManager.Instance.InititateRestore();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	private void onConsentClicked()
	{
		UCM.UserConsentManager.Instance.ShowGdprUnityUI(true);
	}

	private async void onPrivacyClicked()
	{
		m_Message.Open();
		await UCM.UserConsentManager.Instance.ShowCmpFromSettings();
		StartCoroutine(showMessageCorotuine(true));
	}

	private void OpenMessage()
	{
		m_Message.Open();
	}

	private void showMessage(bool i_Show)
	{
		StartCoroutine(showMessageCorotuine(i_Show));
	}

	private IEnumerator showMessageCorotuine(bool i_Show)
	{
		yield return new WaitForEndOfFrame();
		m_Message.Show(i_Show ? LoadingMessage.eMessageType.Loaded : LoadingMessage.eMessageType.NoInternet);
	}

	private void updateToggles()
	{
		m_Toggles[0].Set(LocalizationUtility.GetWordLocalizedString("music").ToUpper(), !SoundManager.Instance.IsMusicMuted, i_State => SoundManager.Instance.SetMusicMute(!i_State));
		m_Toggles[1].Set(LocalizationUtility.GetWordLocalizedString("sfx").ToUpper(), !SoundManager.Instance.IsSFXMuted, i_State => SoundManager.Instance.SetSFXMute(!i_State));
		//m_Toggles[2].Set(LocalizationUtility.GetWordLocalizedString("vibration").ToUpper(), StorageManager.Instance.IsVibrationOn, i_State => StorageManager.Instance.IsVibrationOn = i_State);
	}
}
