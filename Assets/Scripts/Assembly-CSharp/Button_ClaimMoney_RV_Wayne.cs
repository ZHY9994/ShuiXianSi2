using System;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Collectable;
using KobGamesSDKSlim.UI;
using UnityEngine;

public class Button_ClaimMoney_RV_Wayne : MonoBehaviour
{
	[Serializable]
	public class MultiplierAngles
	{
		public float Angle;

		public int Multiplier;
	}

	private int m_MoneyToReceive = 90;

	private int m_MoneyMultiplier = 2;

	[SerializeField]
	private MultiplierAngles[] m_MultiplierAngles;

	private int m_LastMultiplierPick;

	private Quaternion m_LastPickedAngle;

	[SerializeField]
	private ExtendedButton_RV m_ClaimRV;

	[SerializeField]
	private CollectableSender m_MultipliedCoinSender;

	[SerializeField]
	private CoinSender m_MultipliedCoinSenderLegacy;

	[SerializeField]
	private RectTransform m_RewardArrowTransform;

	[SerializeField]
	private Rotator m_RewardArrowRotator;

	[SerializeField]
	private Scaler m_CoinGroupScaler;

	[SerializeField]
	private bool m_ResetEventsOnDisable = true;

	private Action<eRewardVideoCallResult> m_OnRewardVideoCallback;

	private Action m_OnClaimSuccess;

	private Action m_OnClaimFailed;

	private Action m_OnClaimCompleted;

	private int m_MoneyToReceivedRV => m_MoneyMultiplier * m_MoneyToReceive;

	protected MenuVariablesEditor m_MenuVars => GameConfig.Instance.Menus;

	private void OnEnable()
	{
		SetRefs();
		m_LastMultiplierPick = 0;
		m_MoneyMultiplier = 2;
		m_RewardArrowRotator.Duration = m_MenuVars.RV_NeedleRotationDuration;
		m_RewardArrowRotator.Ease = m_MenuVars.RV_NeedleRotationEase;
		m_ClaimRV.Setup(eRewardVideoPlacements.Level_Completed_Claim_RV, RewardVideoButtonCallback, ClaimRewardSuccess, ClaimRewardFailed, OnClaimInteractivityChanged);
		if (m_ClaimRV.interactable)
			m_RewardArrowRotator.ContinueAnimation();
		UpdateMoneyToReceivedRV();
	}

	private void OnDisable()
	{
		if (m_ResetEventsOnDisable)
			ResetEvents();
	}

	public void Set(int i_CoinAmount, Action<eRewardVideoCallResult> i_RewardVideoCallResult, Action i_OnClaimSuccess, Action i_OnClaimFailed, Action i_OnClaimCompleted)
	{
		SetCoins(i_CoinAmount);
		SetEvents(i_RewardVideoCallResult, i_OnClaimSuccess, i_OnClaimFailed, i_OnClaimCompleted);
	}

	private void SetCoins(int i_Value)
	{
		m_MoneyToReceive = i_Value;
		UpdateMoneyToReceivedRV();
	}

	private void SetEvents(Action<eRewardVideoCallResult> i_RewardVideoCallResult, Action i_OnClaimSuccess, Action i_OnClaimFailed, Action i_OnClaimCompleted)
	{
		m_OnRewardVideoCallback = i_RewardVideoCallResult;
		m_OnClaimSuccess = i_OnClaimSuccess;
		m_OnClaimFailed = i_OnClaimFailed;
		m_OnClaimCompleted = i_OnClaimCompleted;
	}

	public void ResetEvents()
	{
		m_OnRewardVideoCallback = null;
		m_OnClaimSuccess = null;
		m_OnClaimFailed = null;
		m_OnClaimCompleted = null;
	}

	private void Update()
	{
		if (m_ClaimRV == null)
		{
			UnityEngine.Debug.LogError("Claim reward video button is not set.");
			return;
		}
		if (m_ClaimRV.interactable || !m_ClaimRV.DisableVisualOnClick)
			CalculateMoneyToReceiveRV(false);
	}

	private void CalculateMoneyToReceiveRV(bool i_Animate)
	{
		if (m_RewardArrowTransform == null || m_MultiplierAngles == null || m_MultiplierAngles.Length == 0)
			return;
		float angle = m_RewardArrowTransform.localEulerAngles.z;
		if (angle > 180f)
			angle -= 360f;
		for (int i = 0; i < m_MultiplierAngles.Length; i++)
		{
			float upper = i + 1 < m_MultiplierAngles.Length ? m_MultiplierAngles[i + 1].Angle : 360f;
			if (!angle.IsBetweenInclusive(m_MultiplierAngles[i].Angle, upper))
				continue;
			if (m_LastMultiplierPick == i)
				return;
			m_LastMultiplierPick = i;
			m_MoneyMultiplier = m_MultiplierAngles[i].Multiplier;
			UpdateMoneyToReceivedRV();
			if (i_Animate && m_CoinGroupScaler != null)
				m_CoinGroupScaler.StartAnimation();
			return;
		}
	}

	private void UpdateMoneyToReceivedRV()
	{
		int amount = m_MoneyToReceivedRV;
		if (m_MultipliedCoinSender != null)
			m_MultipliedCoinSender.Set(amount);
		else if (m_MultipliedCoinSenderLegacy != null)
			m_MultipliedCoinSenderLegacy.SetCoins(amount);
	}

	private void RewardVideoButtonCallback(eRewardVideoCallResult i_Result)
	{
		if (m_RewardArrowRotator != null)
			m_RewardArrowRotator.PauseAnimation();
		if (m_RewardArrowTransform != null)
			m_LastPickedAngle = m_RewardArrowTransform.rotation;
		m_OnRewardVideoCallback?.Invoke(i_Result);
	}

	private void ClaimRewardSuccess()
	{
		if (m_RewardArrowRotator != null)
			m_RewardArrowRotator.StopAnimation();
		if (m_RewardArrowTransform != null)
			m_RewardArrowTransform.rotation = m_LastPickedAngle;
		CalculateMoneyToReceiveRV(false);
		if (m_MultipliedCoinSender != null)
			m_MultipliedCoinSender.Send(m_OnClaimCompleted);
		else if (m_MultipliedCoinSenderLegacy != null)
			m_MultipliedCoinSenderLegacy.SendCoins(m_OnClaimCompleted);
		m_OnClaimSuccess?.Invoke();
	}

	private void ClaimRewardFailed()
	{
		if (m_ClaimRV != null && m_ClaimRV.interactable)
		{
			m_RewardArrowRotator?.ContinueAnimation();
		}
		else
		{
			m_LastMultiplierPick = 0;
			m_MoneyMultiplier = 2;
			UpdateMoneyToReceivedRV();
			m_RewardArrowRotator?.RestartAnimation();
		}
		m_OnClaimFailed?.Invoke();
	}

	private void OnClaimInteractivityChanged(bool i_Value, eButtonInteractivityChangeReason i_Reason)
	{
		if (m_RewardArrowRotator == null)
			return;
		if (i_Value)
			m_RewardArrowRotator.RestartAnimation();
		else
			m_RewardArrowRotator.PauseAnimation();
	}

	private void SetRefs()
	{
		m_ClaimRV = GetComponentInChildren<ExtendedButton_RV>(true);
		if (m_ClaimRV != null)
		{
			m_MultipliedCoinSender = m_ClaimRV.GetComponent<CollectableSender>();
			m_MultipliedCoinSenderLegacy = m_ClaimRV.GetComponent<CoinSender>();
		}
		m_RewardArrowTransform = transform.FindDeepChild<RectTransform>("Reward Arrow");
		if (m_RewardArrowTransform != null)
		{
			m_RewardArrowRotator = m_RewardArrowTransform.GetComponent<Rotator>();
		}
		m_CoinGroupScaler = transform.FindDeepChild<Scaler>("Coin Group Reward");
	}

	private void OnDrawGizmos()
	{
		if (m_MultiplierAngles == null || m_RewardArrowTransform == null)
			return;
		Gizmos.color = Color.green;
		Vector3 position = m_RewardArrowTransform.transform.position;
		foreach (MultiplierAngles multiplierAngle in m_MultiplierAngles)
		{
			if (multiplierAngle == null)
				continue;
			Vector3 direction = Quaternion.Euler(0f, 0f, multiplierAngle.Angle) * Vector3.up * 1000f;
			Gizmos.DrawRay(position, direction);
		}
	}
}
