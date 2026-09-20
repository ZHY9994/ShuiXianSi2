using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Animation;
using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;

public class Screen_BoosterReward : MenuScreenBase
{
	public enum eGetPackageType
	{
		LevelCompleteReward = 0,
		Shop = 1
	}

	public static eGetPackageType GetPackageType;

	[SerializeField]
	private GameObject m_LevelRoot;

	[SerializeField]
	private GameObject m_RewardRoot;

	[SerializeField]
	private ExtendedButton m_LoseBoosterButton;

	[SerializeField]
	private ExtendedButton_RV m_GetBoosterButton;

	[SerializeField]
	private ExtendedButton m_TapToOpenButton;

	private BoosterManager.eBoosterOpenType m_OpenType;

	private bool m_ShouldOpenNextPanel;

	private TweenData m_NoThanksTween => new TweenData(m_MenuVars.TimeToShowNoThanks, m_MenuVars.ShowNoThanksDuration, m_MenuVars.ShowNoThanksEase, 0);

	private BoosterManager.eBoosterOpenType OpenType
	{
		get
		{
			return m_OpenType;
		}
		set
		{
			m_OpenType = value;
			m_LevelRoot?.SetActive(value == BoosterManager.eBoosterOpenType.Level);
			m_RewardRoot?.SetActive(value == BoosterManager.eBoosterOpenType.Shop);
		}
	}

	public static event Action OnBoosterOpen = delegate { };

	protected override void OnEnable()
	{
		base.OnEnable();
		buttonsVisibility(true);
		m_ShouldOpenNextPanel = false;
		m_LoseBoosterButton?.Setup(onLoseSkinClick);
		eRewardVideoPlacements placement = GetPackageType == eGetPackageType.Shop ? eRewardVideoPlacements.Shop_Get_Package : eRewardVideoPlacements.LevelComplete_Get_Package;
		m_GetBoosterButton?.Setup(placement, onRVButtonCallback, onGetBoosterSuccess, onRVClaimFailed);
		m_TapToOpenButton?.Setup(onTapToOpenClick);
		m_TapToOpenButton?.SetInteractable(true);
		updateType();
		StorageManager.Instance.RewardWaiting = true;
	}

	private void onLoseSkinClick()
	{
		m_ShouldOpenNextPanel = true;
		Close();
	}

	private void onTapToOpenClick()
	{
		OnBoosterOpen?.Invoke();
		buttonsVisibility(false);
	}

	protected override void OnScreenCloseEnd()
	{
		base.OnScreenCloseEnd();
		if (OpenType == BoosterManager.eBoosterOpenType.Level && m_ShouldOpenNextPanel)
			MenuManager.Instance.OpenMenuScreen("Screen_LevelCompletedReward_V2");
	}

	private void updateType()
	{
		OpenType = BoosterManager.Instance.OpenType;
	}

	public override void Close()
	{
		m_CanvasGroup?.DOFade(0f, m_MenuVars.ScreenFadeOutDuration)
			.SetEase(Ease.Linear)
			.OnComplete(base.Close);
	}

	private void onRVButtonCallback(eRewardVideoCallResult i_Result)
	{
		if (i_Result == eRewardVideoCallResult.Success)
			SetButtonsInteractivityOff(true);
	}

	private void onGetBoosterSuccess()
	{
		OnBoosterOpen?.Invoke();
		buttonsVisibility(false);
	}

	private void buttonsVisibility(bool i_Value)
	{
		m_LoseBoosterButton?.gameObject.SetActive(i_Value);
		m_GetBoosterButton?.gameObject.SetActive(i_Value);
		m_TapToOpenButton?.gameObject.SetActive(i_Value);
	}

	private void onRVClaimFailed()
	{
		SetButtonsInteractivityOn();
	}
}
