using KobGamesSDKSlim;
using KobGamesSDKSlim.Animation;
using KobGamesSDKSlim.Collectable;
using KobGamesSDKSlim.MenuManagerV1;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Screen_LevelCompleted : MenuScreenBase
{
	private int m_MoneyToReceive = 90;

	[SerializeField]
	private TextMeshProUGUI m_LevelNumber;

	[SerializeField]
	private ParticleSystem[] m_Confetti;

	[SerializeField]
	private Button_ClaimMoney_RV_1 m_ClaimRV;

	[SerializeField]
	private ExtendedButton_FadeAnim m_NoThanks;

	[SerializeField]
	private CollectableSender m_CoinSender;

	private TweenData m_NoThanksTween => new TweenData(m_MenuVars.TimeToShowNoThanks, m_MenuVars.ShowNoThanksDuration, m_MenuVars.ShowNoThanksEase, 0);

	protected override void SetRefs()
	{
		base.SetRefs();
		m_LevelNumber = transform.FindDeepChild<TextMeshProUGUI>("Level ID");
		m_ClaimRV = transform.FindDeepChild<Button_ClaimMoney_RV_1>("Claim RV Button");
		m_NoThanks = transform.FindDeepChild<ExtendedButton_FadeAnim>("No Thanks Button");
		m_CoinSender = m_NoThanks.GetComponent<CollectableSender>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		m_NoThanks.Setup(m_NoThanksTween, new UnityAction(OnNoThanksButtonClick));
		m_MoneyToReceive = 90;
		m_CoinSender.Set(m_MoneyToReceive);
		m_ClaimRV.Set(m_MoneyToReceive, onRVButtonCallback, onRVClaimSuccess, onRVClaimFailed, onClaimFinished);
		m_LevelNumber.text = "LEVEL " + StorageManager.Instance.CurrentLevel;
		PlayConfetti();
	}

	private void PlayConfetti()
	{
		for (int i = 0; i < m_Confetti.Length; i++)
		{
			if (m_Confetti[i] != null)
				m_Confetti[i].Play();
		}
	}

	private void onRVButtonCallback(eRewardVideoCallResult i_Result)
	{
		if (i_Result == eRewardVideoCallResult.Success)
			SetButtonsInteractivityOff(true);
	}

	private void onRVClaimSuccess()
	{
		ResetButtonsState();
		PlayConfetti();
	}

	private void onRVClaimFailed()
	{
		SetButtonsInteractivityOn();
	}

	private void OnNoThanksButtonClick()
	{
		ResetButtonsState();
		m_CoinSender.Send(onClaimFinished);
	}

	private void onClaimFinished()
	{
		DOVirtual.DelayedCall(0.5f, Close);
	}

	protected override void OnScreenCloseEnd()
	{
		base.OnScreenCloseEnd();
		GameManager.Instance.ResetGame();
	}
}
