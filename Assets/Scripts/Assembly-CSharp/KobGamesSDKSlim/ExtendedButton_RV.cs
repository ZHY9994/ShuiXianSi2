using System;
using KobGamesSDKSlim.Animation;

namespace KobGamesSDKSlim
{
	public class ExtendedButton_RV : ExtendedButton_FadeAnim
	{
		public bool DisableButtonAfterAdShown = true;

		public bool DisableVisualOnClick = true;

		public bool OpenConfirmationScreen;

		private bool m_IsShowingAd;

		private bool m_IsAutoInteractivityEventSetup;

		protected override void OnEnableImpl()
		{
			return;
		}

		protected override void OnDisableImpl()
		{
			return;
		}

		public void Setup(eRewardVideoPlacements i_PlacementID, Action<eRewardVideoCallResult> i_RewardVideoCallResult = null, Action i_CloseWithSuccessCallback = null, Action i_CloseWithFailCallback = null, InteractivityChangedCallback i_OnInteractivityChanged = null)
		{
			if (!m_IsSetup) setup(true, i_PlacementID, i_RewardVideoCallResult, i_CloseWithSuccessCallback, i_CloseWithFailCallback, i_OnInteractivityChanged, OpenConfirmationScreen);
		}

		public void Setup(TweenData i_TweenData, eRewardVideoPlacements i_PlacementID, Action<eRewardVideoCallResult> i_RewardVideoCallResult = null, Action i_CloseWithSuccessCallback = null, Action i_CloseWithFailCallback = null, InteractivityChangedCallback i_OnInteractivityChanged = null)
		{
			Setup(i_TweenData, i_PlacementID, i_RewardVideoCallResult, i_CloseWithSuccessCallback, i_CloseWithFailCallback, i_OnInteractivityChanged, OpenConfirmationScreen);
		}

		public void Setup(TweenData i_TweenData, eRewardVideoPlacements i_PlacementID, Action<eRewardVideoCallResult> i_RewardVideoCallResult = null, Action i_CloseWithSuccessCallback = null, Action i_CloseWithFailCallback = null, InteractivityChangedCallback i_OnInteractivityChanged = null, bool i_OpenRVConfirmationScreen = false)
		{
			if (m_IsSetup) return;
			base.Setup(i_TweenData);
			setup(false, i_PlacementID, i_RewardVideoCallResult, i_CloseWithSuccessCallback, i_CloseWithFailCallback, i_OnInteractivityChanged, i_OpenRVConfirmationScreen);
		}

		private void setup(bool i_SetOnRewardVideoAvailabilityChange, eRewardVideoPlacements i_PlacementID, Action<eRewardVideoCallResult> i_RewardVideoCallResult = null, Action i_CloseWithSuccessCallback = null, Action i_CloseWithFailCallback = null, InteractivityChangedCallback i_OnInteractivityChanged = null, bool i_OpenRVConfirmationScreen = false)
		{
			if (m_IsSetup && !i_SetOnRewardVideoAvailabilityChange) return;
			m_IsSetup = true;
			m_IsShowingAd = false;
			if (i_SetOnRewardVideoAvailabilityChange) setInteractable(true, eButtonInteractivityChangeReason.RV_Availability);
			onClick.AddListener(() =>
			{
				m_IsShowingAd = true;
				ADManager.Instance.ShowRewardAD(() =>
				{
					m_IsShowingAd = false;
					setInteractable(false, eButtonInteractivityChangeReason.Manual);
					i_RewardVideoCallResult?.Invoke(eRewardVideoCallResult.Success);
					i_CloseWithSuccessCallback?.Invoke();
					if (DisableButtonAfterAdShown)
						setAutoInteractivity(false, eButtonInteractivityChangeReason.Manual);
				});
			});
			OnInteractivityChanged += i_OnInteractivityChanged;
		}

		protected override void OnFadeAnimDelayCallback()
		{
			setInteractable(true, eButtonInteractivityChangeReason.RV_Availability);
		}

		public override void ResetState()
		{
			setAutoInteractivityEvent(false);
			base.ResetState();
		}

		protected virtual void onRewardVideoAvailabilityChange(bool i_Value)
		{
			setInteractable(i_Value, eButtonInteractivityChangeReason.RV_Availability);
		}

		private void onOnlineStatusChange(bool i_IsConnected)
		{
			if (!i_IsConnected) setInteractable(false, eButtonInteractivityChangeReason.RV_Availability);
		}

		private void setInteractable(bool i_Value)
		{
			setInteractable(i_Value, eButtonInteractivityChangeReason.Manual);
		}

		private void setInteractable(bool i_Value, eButtonInteractivityChangeReason i_Reason)
		{
			base.SetInteractable(i_Value, i_Reason);
			if ((i_Reason == eButtonInteractivityChangeReason.Manual || m_IsShowingAd) && !DisableVisualOnClick) SetAssetsState(eButtonState.Normal);
		}

		public override void SetInteractable(bool i_Value)
		{
			setInteractable(i_Value);
		}

		public override void SetInteractable(bool i_Value, eButtonInteractivityChangeReason i_Reason)
		{
			setInteractable(i_Value, i_Reason);
		}

		private void setAutoInteractivity(bool i_Value, eButtonInteractivityChangeReason i_Reason)
		{
			setInteractable(i_Value, i_Reason);
		}

		private void setAutoInteractivityEvent(bool i_Value)
		{
			if (m_IsAutoInteractivityEventSetup == i_Value) return;
			m_IsAutoInteractivityEventSetup = i_Value;
			if (AdsManager.OnRewardVideoAvailabilityChange != null)
			{
				if (i_Value) AdsManager.OnRewardVideoAvailabilityChange += onRewardVideoAvailabilityChange;
				else AdsManager.OnRewardVideoAvailabilityChange -= onRewardVideoAvailabilityChange;
			}
		}
	}
}
