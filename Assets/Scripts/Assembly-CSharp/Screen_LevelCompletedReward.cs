using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Animation;
using KobGamesSDKSlim.Collectable;
using KobGamesSDKSlim.MenuManagerV1;
using KobGamesSDKSlim.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wayne.Game.Tutorials;
using Wayne.Game.UIs;

public class Screen_LevelCompletedReward : MenuScreenBase
{
	private int m_MoneyToReceive = 180;

	private bool m_OpenSecondScreen;

	[SerializeField]
	private ParticleSystem[] m_Confetti;

	[Header("Reward")]
	[SerializeField]
	private Image m_RewardSkinSilhouette;

	[SerializeField]
	private Image m_RewardSkinFill;

	[SerializeField]
	private Image m_RewardSkinSilhouetteComplete;

	[SerializeField]
	private Image m_RewardSkinFillComplete;

	[SerializeField]
	private CollectableSender m_RewardCoinSender;

	[SerializeField]
	private GameObject m_TitleComplete;

	[SerializeField]
	private GameObject m_TitleNewSkin;

	[SerializeField]
	private GameObject m_CashRoot;

	[SerializeField]
	private GameObject m_SkinFillProgressRoot;

	[SerializeField]
	private GameObject m_SkinFillCompleteRoot;

	[SerializeField]
	private ExtendedButton_RV m_GetSkinButton;

	[SerializeField]
	private ExtendedButton m_FreeSkinButton;

	[SerializeField]
	private ExtendedButton_FadeAnim m_LoseSkinButton;

	[SerializeField]
	private ExtendedButton_FadeAnim m_ContinueButton;

	[SerializeField]
	private TextMeshProUGUI m_FillProgressionText;

	[SerializeField]
	private Scaler m_FillSkinScaler;

	[SerializeField]
	private ParticleSystem m_UnlockSkinParticles;

	[SerializeField]
	private ParticleSystem m_UnlockSkinParticlesIdle;

	[SerializeField]
	private ProgressBar m_ProgressBar;

	[SerializeField]
	private GameObject m_Plus;

	[Header("Reward anim parameters")]
	[SerializeField]
	private float m_FillDuration;

	[SerializeField]
	private Ease m_FillEase;

	private int m_LastMultiplierPick;

	private float m_FillProgression;

	private TItemData m_CurrentItemToFill;

	private TItemData m_PreviousItemToFill;

	private TweenData m_NoThanksTween => new TweenData(m_MenuVars.TimeToShowNoThanks, m_MenuVars.ShowNoThanksDuration, m_MenuVars.ShowNoThanksEase, 0);

	private bool m_IsSkinBarFilled => m_FillProgression >= 1f;

	protected override void SetRefs()
	{
		base.SetRefs();
		m_RewardSkinSilhouette = transform.FindDeepChild<Image>("Reward Skin Silhouette");
		m_RewardSkinFill = transform.FindDeepChild<Image>("Reward Skin Fill");
		m_RewardSkinSilhouetteComplete = transform.FindDeepChild<Image>("Reward Skin Silhouette Complete");
		m_RewardSkinFillComplete = transform.FindDeepChild<Image>("Reward Skin Fill Complete");
		m_RewardCoinSender = transform.FindDeepChild<CollectableSender>("Reward Coins");
		m_TitleComplete = transform.FindDeepChild<Transform>("Title Complete")?.gameObject;
		m_TitleNewSkin = transform.FindDeepChild<Transform>("Title New Skin")?.gameObject;
		m_CashRoot = transform.FindDeepChild<Transform>("Cash Root")?.gameObject;
		m_SkinFillProgressRoot = transform.FindDeepChild<Transform>("Skin Fill Progress Root")?.gameObject;
		m_SkinFillCompleteRoot = transform.FindDeepChild<Transform>("Skin Fill Complete Root")?.gameObject;
		m_GetSkinButton = transform.FindDeepChild<ExtendedButton_RV>("Get RV Prise Button");
		m_FreeSkinButton = transform.FindDeepChild<ExtendedButton>("Get Free Prise Button");
		m_LoseSkinButton = transform.FindDeepChild<ExtendedButton_FadeAnim>("Lose Skin Button");
		m_ContinueButton = transform.FindDeepChild<ExtendedButton_FadeAnim>("Next Button");
		m_FillProgressionText = transform.FindDeepChild<TextMeshProUGUI>("Fill Progression Text");
		m_UnlockSkinParticles = transform.FindDeepChild<ParticleSystem>("Unlock Skin Particles");
		m_UnlockSkinParticlesIdle = transform.FindDeepChild<ParticleSystem>("Unlock Skin Particles Idle");
		m_FillSkinScaler = m_RewardSkinSilhouetteComplete.GetComponent<Scaler>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		m_GetSkinButton?.Setup(eRewardVideoPlacements.LevelComplete_Get_Weapon, onRVButtonCallback, getSkinSuccessCallback, onRVClaimFailed);
		m_FreeSkinButton?.Setup(getSkinSuccessCallback);
		m_LoseSkinButton?.Setup(m_NoThanksTween, Continue);
		m_ContinueButton?.Setup(m_NoThanksTween, Continue);
		m_MoneyToReceive = 150;
		m_RewardCoinSender?.Set(300);
		m_Plus?.SetActive(true);
		PlayConfetti();
		showFillProgress();
	}

	protected override void OnScreenOpenEnd()
	{
		base.OnScreenOpenEnd();
		m_FillProgression = 0f;
		m_CurrentItemToFill = Wayne.Game.Utils.ShopUtils.GetCurrentItemToFill();
		m_PreviousItemToFill = Wayne.Game.Utils.ShopUtils.GetPreviousItemToFill();
		if (m_CurrentItemToFill == null)
		{
			showCash();
			return;
		}
		m_RewardSkinSilhouette.sprite = m_CurrentItemToFill.RewardSprite;
		m_RewardSkinFill.sprite = m_CurrentItemToFill.RewardSprite;
		m_RewardSkinSilhouetteComplete.sprite = m_CurrentItemToFill.RewardSprite;
		m_RewardSkinFillComplete.sprite = m_CurrentItemToFill.RewardSprite;
		int currentLevel = StorageManager.Instance.CurrentLevel;
		m_FillProgression = 1f - (m_CurrentItemToFill.RequiredLevel - currentLevel + 1f) /
			(m_CurrentItemToFill.RequiredLevel - m_PreviousItemToFill.RequiredLevel);
		Tween tween = DOVirtual.Float(0f, m_FillProgression, m_FillDuration, x =>
		{
			m_RewardSkinFill.fillAmount = x;
			m_FillProgressionText.text = ((int)(x * 100f)).ToString() + "%";
			m_ProgressBar.SetProgress(x);
		});
		tween.OnComplete(onFillComplete);
	}

	private void getSkinSuccessCallback()
	{
		m_Plus?.SetActive(false);
		ResetButtonsState();
		PlayConfetti();
		Wayne.Game.Utils.ShopUtils.UnlockItem(m_CurrentItemToFill);
		Close();
	}

	private void onFillComplete()
	{
		if (m_IsSkinBarFilled)
			showFillComplete();
	}

	private void showFillProgress()
	{
		m_TitleComplete?.SetActive(true);
		m_TitleNewSkin?.SetActive(false);
		m_SkinFillProgressRoot?.SetActive(true);
		m_SkinFillCompleteRoot?.SetActive(false);
		m_CashRoot?.SetActive(false);
		m_GetSkinButton?.gameObject.SetActive(false);
		m_FreeSkinButton?.gameObject.SetActive(false);
		m_LoseSkinButton?.gameObject.SetActive(false);
		m_ContinueButton?.gameObject.SetActive(true);
		m_UnlockSkinParticles?.Stop();
		m_UnlockSkinParticlesIdle?.Stop();
		m_RewardSkinFill.fillAmount = 0f;
	}

	private void showFillComplete()
	{
		m_TitleComplete?.SetActive(true);
		m_TitleNewSkin?.SetActive(true);
		m_SkinFillProgressRoot?.SetActive(false);
		m_SkinFillCompleteRoot?.SetActive(true);
		m_CashRoot?.SetActive(false);
		m_GetSkinButton?.gameObject.SetActive(true);
		m_LoseSkinButton?.gameObject.SetActive(true);
		m_FreeSkinButton?.gameObject.SetActive(false);
		m_ContinueButton?.gameObject.SetActive(false);
		m_UnlockSkinParticles?.Play();
		m_UnlockSkinParticlesIdle?.Play();
		m_FillSkinScaler?.StartAnimation();
		Tutorials();
	}

	private void Tutorials()
	{
		if (StorageManager.Instance.DefaultStartlevel == 3)
		{
			m_FreeSkinButton?.gameObject.SetActive(true);
			m_GetSkinButton?.gameObject.SetActive(false);
			m_LoseSkinButton?.gameObject.SetActive(false);
			m_ContinueButton?.gameObject.SetActive(false);
		}
	}

	private void showCash()
	{
		m_TitleComplete?.SetActive(true);
		m_TitleNewSkin?.SetActive(false);
		m_SkinFillProgressRoot?.SetActive(false);
		m_SkinFillCompleteRoot?.SetActive(false);
		m_CashRoot?.SetActive(true);
		m_GetSkinButton?.gameObject.SetActive(false);
		m_LoseSkinButton?.gameObject.SetActive(false);
		m_UnlockSkinParticles?.Stop();
		m_UnlockSkinParticlesIdle?.Stop();
	}

	protected override void OpenAnim()
	{
		base.OpenAnim();
	}

	private void PlayConfetti()
	{
		foreach (ParticleSystem particle in m_Confetti)
			particle?.Play();
	}

	private void onRVButtonCallback(eRewardVideoCallResult i_Result)
	{
		if (i_Result == eRewardVideoCallResult.Success)
			SetButtonsInteractivityOff(true);
	}

	private void onRVClaimFailed()
	{
		SetButtonsInteractivityOn();
	}

	private void Continue()
	{
		Close();
	}

	private void onClaimFinished()
	{
		DOVirtual.DelayedCall(0.5f, Close, true);
	}

	protected override void OnScreenCloseEnd()
	{
		base.OnScreenCloseEnd();
		if (m_FillProgression >= 1f)
		{
			MenuManager.Instance.OpenMenuScreen("Screen_LevelCompletedReward_V2");
			return;
		}
		GameManager.Instance.ResetGame();
		if (StorageManager.Instance.CurrentLevel == 3 && TutorialManager.Instance.IsShopTutorialActive)
		{
			MenuManager.Instance.OpenWelcomeScreen();
			TMenuButtons.OpenShop();
		}
	}

}
