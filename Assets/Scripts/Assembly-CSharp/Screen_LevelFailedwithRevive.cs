using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Animation;
using KobGamesSDKSlim.MenuManagerV1;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Screen_LevelFailedwithRevive : MenuScreenBase
{
	[SerializeField]
	private TextMeshProUGUI m_LevelNumber;

	[SerializeField]
	private Image m_RVCircle;

	private Tween m_CircleTween;

	[SerializeField]
	private TextMeshProUGUI m_ReviveTime;

	[SerializeField]
	private Scaler m_ReviveTimeScaler;

	[SerializeField]
	private ExtendedButton_RV m_ReviveButton;

	[SerializeField]
	private ExtendedButton_FadeAnim m_NoThanks;

	private float m_CurrReviveDuration;

	private TweenData m_NoThanksTween => new TweenData(m_MenuVars.TimeToShowNoThanks, m_MenuVars.ShowNoThanksDuration, m_MenuVars.ShowNoThanksEase, 0);

	protected override void SetRefs()
	{
		base.SetRefs();
		m_LevelNumber = transform.FindDeepChild<TextMeshProUGUI>("Level ID");
		m_RVCircle = transform.FindDeepChild<Image>("RV Circle");
		m_ReviveTime = transform.FindDeepChild<TextMeshProUGUI>("Revive Time");
		m_ReviveTimeScaler = m_ReviveTime.GetComponent<Scaler>();
		m_ReviveButton = transform.FindDeepChild<ExtendedButton_RV>("Revive Button");
		m_NoThanks = transform.FindDeepChild<ExtendedButton_FadeAnim>("No Thanks Button");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		m_ReviveButton.Setup(eRewardVideoPlacements.Level_Failed_Revive_RV, RewardVideoButtonCallback, ReviveSuccess, ReviveFailed);
		m_NoThanks.Setup(m_NoThanksTween, OnNoThanksButtonClick);
		m_CurrReviveDuration = m_MenuVars.TimeToAllowRevive;
		m_RVCircle.fillAmount = 1f;
		m_ReviveTime.text = m_CurrReviveDuration.ToString();
		m_CircleTween = DOTween.To(() => m_CurrReviveDuration, value => m_CurrReviveDuration = value, 0f, m_CurrReviveDuration)
			.SetEase(Ease.Linear)
			.OnUpdate(() =>
			{
				m_RVCircle.fillAmount = m_CurrReviveDuration / m_MenuVars.TimeToAllowRevive;
				string text = Mathf.Ceil(m_CurrReviveDuration).ToString();
				if (m_ReviveTime.text != text)
					m_ReviveTimeScaler.StartAnimation();
				m_ReviveTime.text = text;
			})
			.OnComplete(GameOver);
		m_LevelNumber.text = "LEVEL " + StorageManager.Instance.CurrentLevel;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	private void RewardVideoButtonCallback(eRewardVideoCallResult i_Result)
	{
		if (i_Result == eRewardVideoCallResult.Success)
		{
			SetButtonsInteractivityOff(true);
			m_CircleTween.Pause();
		}
	}

	private void ReviveSuccess()
	{
		m_CircleTween.Kill();
		GameManager.Instance.LevelContinue();
		Close();
	}

	private void ReviveFailed()
	{
		m_CircleTween.Play();
		SetButtonsInteractivityOn();
	}

	private void OnNoThanksButtonClick()
	{
		GameOver();
	}

	private void GameOver()
	{
		ResetButtonsState();
		GameManager.Instance.GameOver(true);
		m_CircleTween?.Kill();
		Close();
	}
}
