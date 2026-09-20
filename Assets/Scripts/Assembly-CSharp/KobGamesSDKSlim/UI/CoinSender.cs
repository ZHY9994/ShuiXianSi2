using System;
using System.Collections;
using KobGamesSDKSlim.Animation;
using MoreMountains.FeedbacksForThirdParty;
using TMPro;
using UnityEngine;

namespace KobGamesSDKSlim.UI
{
	public class CoinSender : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI m_CoinText;

		[SerializeField]
		private RectTransform m_CoinIcon;

		[SerializeField]
		private bool m_IsUseTextFormat;

		[SerializeField]
		private bool m_IsOverrideTarget;

		[SerializeField]
		private CoinUpdater m_CoinsTarget;

		[SerializeField]
		private eCoinSenderType m_CoinSenderType;

		[SerializeField]
		private EarnObjectUIAnimData m_EarnObjectUIAnimData;

		[SerializeField]
		private bool m_IsDecreaseSendAmount;

		private int m_CoinsValue;

		private int m_ReceivedCoinsValue;

		private int m_SentCoinsValue;

		private EarnCoinUI m_EarnCoinUIDummy;

		private bool m_IsHapticsCooldown;

		private Action m_SendCompleteCallback;

		private string m_TextFormat;

		private HUDVariablesEditorBase.EarnObjectData m_EarnParameters => GameConfig.Instance.HUD.EarnObject;

		private bool m_IsOverrideAnimData => m_CoinSenderType == (eCoinSenderType)3;

		public void SetCoins(int i_CoinsAmount)
		{
			m_CoinsValue = i_CoinsAmount;
			updateCoinValueText(i_CoinsAmount);
		}

		private void updateCoinValueText(int i_CoinValue)
		{
			if (m_CoinText == null) throw new NullReferenceException(nameof(m_CoinText));
			string text;
			if (!m_IsUseTextFormat)
				text = i_CoinValue.ToString();
			else
			{
				if (m_TextFormat == null) m_TextFormat = m_CoinText.text;
				text = string.Format(m_TextFormat, i_CoinValue);
			}
			m_CoinText.SetText(text);
		}

		public void SendCoins(Action i_CompleteCallback = null)
		{
			EarnObjectUIAnimData animData = m_IsOverrideAnimData ? m_EarnObjectUIAnimData : null;
			if (animData == null)
			{
				var data = m_EarnParameters;
				if (data == null || data.EarnObjectUIAnimData == null) throw new NullReferenceException(nameof(m_EarnParameters));
				if (!data.EarnObjectUIAnimData.ContainsKey(m_CoinSenderType))
					animData = data.DefaultEarnObjectUIAnimData;
				else
					animData = data.EarnObjectUIAnimData[m_CoinSenderType];
			}
			RectTransform target = null;
			if (m_IsOverrideTarget && m_CoinsTarget != null) target = m_CoinsTarget.CoinMoveTarget;
			if (target == null && HUDManager.Instance != null) target = HUDManager.Instance.DefaultCoinsTarget;
			if (animData == null || target == null) throw new NullReferenceException();
			m_SendCompleteCallback = i_CompleteCallback;
			StartCoroutine(sendCoinsCoroutine(m_CoinIcon, target, animData));
		}

		private IEnumerator sendCoinsCoroutine(RectTransform i_SpawnRectTransform, RectTransform i_TargetRectTransform, EarnObjectUIAnimData i_EarnAnimData)
		{
			EarnObjectUIAnimData.AnimData animData = null;
			switch (i_EarnAnimData.SpawnMode)
			{
				case eEarnObjectSpawnMode.Linear:
					animData = i_EarnAnimData.LinearMode;
					break;
				case eEarnObjectSpawnMode.Burst:
					animData = i_EarnAnimData.BurstMode;
					break;
			}
			int coinCost = m_CoinsValue / animData.CoinsNumber;
			int coinCostRest = m_CoinsValue % animData.CoinsNumber;
			m_ReceivedCoinsValue = 0;
			for (int i = 0; i < animData.CoinsNumber; i++)
			{
				GameObject coinGo = PoolManager.Instance.Dequeue(ePoolType.CoinUI);
				m_EarnCoinUIDummy = coinGo.GetComponent<EarnCoinUI>();
				m_EarnCoinUIDummy.transform.SetParent(i_TargetRectTransform);
				m_EarnCoinUIDummy.SetCoinCost(i == animData.CoinsNumber - 1 ? coinCostRest + coinCost : coinCost);
				m_EarnCoinUIDummy.Animate(i_SpawnRectTransform, i_TargetRectTransform, i_EarnAnimData.SpawnMode, animData, coinMoveComplete);
				m_SentCoinsValue += m_EarnCoinUIDummy.CoinCost;
				if (m_IsDecreaseSendAmount)
					updateCoinValueText(m_CoinsValue - m_SentCoinsValue);
				float delay = animData.DelayBetweenCoins * animData.DelayBetweenCoinsCurve.Evaluate((float)i / (float)animData.CoinsNumber);
				yield return new WaitForSeconds(delay);
			}
		}

		private void coinMoveComplete(EarnObjectUI i_SentCoin)
		{
			if (i_SentCoin == null) throw new NullReferenceException(nameof(i_SentCoin));
			EarnCoinUI sentCoin = i_SentCoin as EarnCoinUI;
			if (sentCoin == null) throw new InvalidCastException(nameof(i_SentCoin));
			m_ReceivedCoinsValue += sentCoin.CoinCost;
			if (m_ReceivedCoinsValue == m_CoinsValue)
			{
				m_SendCompleteCallback?.Invoke();
				if (m_EarnParameters != null && m_EarnParameters.IsPlayHapticsOnReceive)
					playHaptics(m_EarnParameters.EarningCompleteHaptics);
			}
			else if (m_EarnParameters != null && m_EarnParameters.IsPlayHapticsOnReceive)
				playHaptics(m_EarnParameters.ReceivedHaptics);
		}

		private void playHaptics(MMFeedbackHaptics.HapticTypes i_HapticsType)
		{
			if (m_IsHapticsCooldown) return;
			m_IsHapticsCooldown = true;
			Managers.Instance.HapticManager.Haptic(i_HapticsType);
			if (m_EarnParameters == null) throw new NullReferenceException(nameof(m_EarnParameters));
			Invoke(nameof(resetHapticsCooldown), m_EarnParameters.HapticsCooldown);
		}

		private void resetHapticsCooldown()
		{
			m_IsHapticsCooldown = false;
		}

		private void SendCoinsTest()
		{
			SendCoins();
		}

		private void SetRefs()
		{
			m_CoinText = transform.FindDeepChild<TextMeshProUGUI>("Coin Text");
			m_CoinIcon = transform.FindDeepChild<RectTransform>("Coin Icon");
		}
	}
}
