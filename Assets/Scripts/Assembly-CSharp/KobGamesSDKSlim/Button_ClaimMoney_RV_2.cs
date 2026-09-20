using System;
using KobGamesSDKSlim.Collectable;
using KobGamesSDKSlim.UI;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class Button_ClaimMoney_RV_2 : MonoBehaviour
	{
		[Serializable]
		private struct MoneyToReceiveData
		{
			[SerializeField]
			private float m_Angle;

			[SerializeField]
			private int m_Multiplier;

			public float Angle => m_Angle;

			public int Multiplier => m_Multiplier;
		}

		private int m_MoneyToReceive = 90;

		private int m_MoneyMultiplier = 2;

		[SerializeField]
		private MoneyToReceiveData[] m_MultiplierData;

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

		private void SetRefs()
		{
			m_ClaimRV = GetComponentInChildren<ExtendedButton_RV>();
			if (m_ClaimRV == null) throw new NullReferenceException(nameof(m_ClaimRV));
			m_MultipliedCoinSender = m_ClaimRV.GetComponent<CollectableSender>();
			m_MultipliedCoinSenderLegacy = m_ClaimRV.GetComponent<CoinSender>();
			m_RewardArrowTransform = transform.FindDeepChild<RectTransform>("Reward Arrow");
			if (m_RewardArrowTransform == null) throw new NullReferenceException(nameof(m_RewardArrowTransform));
			m_RewardArrowRotator = m_RewardArrowTransform.GetComponent<Rotator>();
			m_CoinGroupScaler = transform.FindDeepChild<Scaler>("Coin Group Reward");
		}

		private void OnEnable()
		{
			SetRefs();
			m_LastMultiplierPick = 0;
			m_MoneyMultiplier = 2;
			if (m_RewardArrowRotator == null || m_ClaimRV == null) throw new NullReferenceException();
			m_RewardArrowRotator.Duration = m_MenuVars.RV_NeedleRotationDuration;
			m_RewardArrowRotator.Ease = m_MenuVars.RV_NeedleRotationEase;
			m_ClaimRV.Setup(eRewardVideoPlacements.Level_Completed_Claim_RV, RewardVideoButtonCallback, ClaimRewardSuccess, ClaimRewardFailed, OnClaimInteractivityChanged);
			if (m_ClaimRV.interactable) m_RewardArrowRotator.ContinueAnimation();
			UpdateMoneyToReceivedRV();
		}

		private void OnDisable()
		{
			if (m_ResetEventsOnDisable) ResetEvents();
		}

		public void Set(int i_CoinAmount, Action<eRewardVideoCallResult> i_RewardVideoCallResult, Action i_OnClaimSuccess, Action i_OnClaimFailed, Action i_OnClaimCompleted)
		{
			m_MoneyToReceive = i_CoinAmount;
			UpdateMoneyToReceivedRV();
			SetEvents(i_RewardVideoCallResult, i_OnClaimSuccess, i_OnClaimFailed, i_OnClaimCompleted);
		}

		private void SetCoins(int i_Value)
		{
			m_MoneyToReceive = i_Value;
			UpdateMoneyToReceivedRV();
		}

		private void SetEvents(Action<eRewardVideoCallResult> i_RewardVideoCallResult, Action i_OnClaimSuccess, Action i_OnClaimFailed, Action i_OnClaimCompleted)
		{
			m_OnRewardVideoCallback = (Action<eRewardVideoCallResult>)Delegate.Combine(m_OnRewardVideoCallback, i_RewardVideoCallResult);
			m_OnClaimSuccess = (Action)Delegate.Combine(m_OnClaimSuccess, i_OnClaimSuccess);
			m_OnClaimFailed = (Action)Delegate.Combine(m_OnClaimFailed, i_OnClaimFailed);
			m_OnClaimCompleted = (Action)Delegate.Combine(m_OnClaimCompleted, i_OnClaimCompleted);
		}

		public void ResetEvents()
		{
			m_OnRewardVideoCallback = null;
			m_OnClaimCompleted = null;
			m_OnClaimFailed = null;
			m_OnClaimSuccess = null;
		}

		private void Update()
		{
			if (m_ClaimRV == null) throw new NullReferenceException(nameof(m_ClaimRV));
			if (m_ClaimRV.interactable || !m_ClaimRV.DisableVisualOnClick)
				CalculateMoneyToReceiveRV(false);
		}

		private void CalculateMoneyToReceiveRV(bool i_Animate)
		{
			if (m_RewardArrowTransform == null || m_MultiplierData == null) throw new NullReferenceException();
			float angle = m_RewardArrowTransform.localEulerAngles.z;
			if (angle > 180f) angle = -(360f - angle);
			for (int i = 0; i < m_MultiplierData.Length - 1; i++)
			{
				if (!angle.IsBetweenInclusive(m_MultiplierData[i + 1].Angle, m_MultiplierData[i].Angle)) continue;
				if (m_LastMultiplierPick + 1 == i + 1) return;
				m_LastMultiplierPick = i;
				m_MoneyMultiplier = m_MultiplierData[i].Multiplier;
				UpdateMoneyToReceivedRV();
				if (i_Animate && m_CoinGroupScaler != null) m_CoinGroupScaler.StartAnimation();
				return;
			}
		}

		private void UpdateMoneyToReceivedRV()
		{
			int amount = m_MoneyToReceive * m_MoneyMultiplier;
			if (m_MultipliedCoinSender != null)
				m_MultipliedCoinSender.Set(amount);
			else if (m_MultipliedCoinSenderLegacy != null)
				m_MultipliedCoinSenderLegacy.SetCoins(amount);
		}

		private void RewardVideoButtonCallback(eRewardVideoCallResult i_Result)
		{
			if (i_Result == eRewardVideoCallResult.Success)
			{
				if (m_RewardArrowRotator == null || m_RewardArrowTransform == null) throw new NullReferenceException();
				m_RewardArrowRotator.PauseAnimation();
				m_LastPickedAngle = m_RewardArrowTransform.rotation;
			}
			m_OnRewardVideoCallback?.Invoke(i_Result);
		}

		private void ClaimRewardSuccess()
		{
			if (m_RewardArrowRotator == null || m_RewardArrowTransform == null) throw new NullReferenceException();
			m_RewardArrowRotator.StopAnimation();
			m_RewardArrowTransform.rotation = m_LastPickedAngle;
			CalculateMoneyToReceiveRV(false);
			if (m_MultipliedCoinSender != null) m_MultipliedCoinSender.Send(m_OnClaimCompleted);
			else if (m_MultipliedCoinSenderLegacy != null) m_MultipliedCoinSenderLegacy.SendCoins(m_OnClaimCompleted);
			m_OnClaimSuccess?.Invoke();
		}

		private void ClaimRewardFailed()
		{
			if (m_ClaimRV == null || m_RewardArrowRotator == null) throw new NullReferenceException();
			if (m_ClaimRV.interactable) m_RewardArrowRotator.ContinueAnimation();
			else
			{
				m_LastMultiplierPick = 0;
				m_MoneyMultiplier = 2;
				UpdateMoneyToReceivedRV();
				m_RewardArrowRotator.RestartAnimation();
			}
			m_OnClaimFailed?.Invoke();
		}

		private void OnClaimInteractivityChanged(bool i_Value, eButtonInteractivityChangeReason i_Reason)
		{
			if (m_RewardArrowRotator == null) throw new NullReferenceException(nameof(m_RewardArrowRotator));
			if (i_Value) m_RewardArrowRotator.RestartAnimation();
			else m_RewardArrowRotator.PauseAnimation();
		}

		private void OnDrawGizmos()
		{
			if (m_MultiplierData == null || m_RewardArrowTransform == null) return;
			Gizmos.color = Color.green;
			Vector3 position = m_RewardArrowTransform.transform.position;
			foreach (var data in m_MultiplierData)
			{
				Vector3 direction = Quaternion.Euler(0f, 0f, data.Angle) * Vector3.up;
				Gizmos.DrawRay(position, direction * 1000f);
			}
		}
	}
}
