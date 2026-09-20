using System;
using DG.Tweening;
using Dreamteck.Splines;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Collectable;
using KobGamesSDKSlim.MenuManagerV1;
using TMPro;
using UnityEngine;
using Wayne.Game.Tutorials;
using Wayne.Game.UIs;

public class Screen_LevelCompletedReward_V2 : MenuScreenBase
{
	[Serializable]
	private class SplinePointData
	{
		public Vector3 Position;

		public Vector3 Normal;

		public SplinePointData()
		{
		}
	}

	private int m_BaseMiniGameReward = 50;

	[SerializeField]
	private SplineComputer m_Spline;

	[SerializeField]
	private SplineFollower m_Follower;

	[SerializeField]
	private SplinePointData[] m_Points;

	[SerializeField]
	private AudioClip m_HitSound;

	[SerializeField]
	private UIDummy m_Dummy;

	[SerializeField]
	private ParticleSystem m_HitParticles;

	[SerializeField]
	private ParticleSystem m_MoneyExplosion_1;

	[SerializeField]
	private ParticleSystem m_MoneyExplosion_2;

	[SerializeField]
	private ExtendedButton m_RewardButton;

	[SerializeField]
	private ExtendedButton m_NoThanksButton;

	[SerializeField]
	private Scaler m_ClaimRewardScaler;

	[SerializeField]
	private Scaler m_TextBumpScaler;

	[SerializeField]
	private TextMeshProUGUI m_RewardButtonText;

	[SerializeField]
	private TextMeshProUGUI m_NoThanksButtonText;

	[SerializeField]
	private CollectableSender m_CollectableSender;

	[SerializeField]
	private CollectableSender m_NoThanksCollectableSender;

	[SerializeField]
	private RectTransform m_Target;

	[SerializeField]
	private RectTransform m_CrossHair;

	private int m_PendingRewardAmount;

	private SoundManager m_SoundManager => SoundManager.Instance;

	protected override void OnEnable()
	{
		base.OnEnable();
		m_NoThanksButtonText.text = m_BaseMiniGameReward.ToString();
		enableInteraction();
	}

	private void UpdateUI()
	{
		int oldAmount;
		int.TryParse(m_RewardButtonText.text, out oldAmount);
		int amount = GetRewardAmount();
		if (oldAmount != amount)
			m_TextBumpScaler?.StartAnimation();
		m_RewardButtonText.text = amount.ToString();
	}

	private void RewardButton()
	{
		m_PendingRewardAmount = GetRewardAmount();
		disableInteraction();
		stopFollowing();
		ADManager.Instance.ShowRewardAD(onRewardAdSuccess, onRewardAdFailed);
	}

	private void onRewardAdSuccess()
	{
		m_CrossHair.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f, 10, 1f);
		m_ClaimRewardScaler?.StartAnimation();
		m_Dummy.ThrowToTarget();
		Invoke(nameof(OnRewardSuccesfull), 2.25f);
	}

	private void onRewardAdFailed()
	{
		m_PendingRewardAmount = 0;
		m_RewardButton?.SetInteractable(true);
		m_NoThanksButton?.SetInteractable(true);
		startFollowing();
	}

	private void OnRewardSuccesfull()
	{
		m_Dummy?.ResetDummy();
		m_ClaimRewardScaler?.StartAnimation();
		disableInteraction();
		stopFollowing();
		m_CollectableSender?.Set(m_PendingRewardAmount > 0 ? m_PendingRewardAmount : GetRewardAmount());
		m_PendingRewardAmount = 0;
		m_CollectableSender?.Send(Close);
	}

	private void NoThanksButton()
	{
		disableInteraction();
		m_NoThanksCollectableSender?.Set(m_BaseMiniGameReward);
		m_NoThanksCollectableSender?.Send(Close);
	}

	private void doClaimReward(int Amount)
	{
		disableInteraction();
		stopFollowing();
		m_CollectableSender?.Set(Amount);
		m_CollectableSender?.Send(Close);
	}

	private void enableInteraction()
	{
		m_Dummy?.ResetDummy();
		m_RewardButton?.SetInteractable(true);
		m_NoThanksButton?.SetInteractable(true);
		m_RewardButton?.Setup(RewardButton);
		m_NoThanksButton?.Setup(NoThanksButton);
		initSpline();
	}

	private void disableInteraction()
	{
		m_RewardButton?.SetInteractable(false);
		m_NoThanksButton?.SetInteractable(false);
	}

	private void stopFollowing()
	{
		m_Follower.follow = false;
	}

	private void startFollowing()
	{
		if (m_Follower != null)
			m_Follower.follow = true;
	}

	protected override void OnScreenCloseEnd()
	{
		int level = StorageManager.Instance.CurrentLevel;
		bool itemBeingUnlocked = Wayne.Game.Utils.ShopUtils.IsAnyItemBeingUnlocked(level - 1);
		if (itemBeingUnlocked)
		{
			GameManager.Instance.ResetGame();
			if (StorageManager.Instance.CurrentLevel == 3 && TutorialManager.Instance.IsShopTutorialActive)
			{
				MenuManager.Instance.OpenWelcomeScreen();
				TMenuButtons.OpenShop();
			}
		}
		else
		{
			MenuManager.Instance.OpenMenuScreen("Screen_LevelCompletedReward");
		}
		base.OnScreenCloseEnd();
		StopAllCoroutines();
	}

	public int GetRewardAmount()
	{
		Vector3 target = m_Target.position;
		Vector3 crossHair = m_CrossHair.position;
		float distance = Mathf.Sqrt((target.x - crossHair.x) * (target.x - crossHair.x) + (target.y - crossHair.y) * (target.y - crossHair.y));
		if (distance < 1.1f) return 5 * m_BaseMiniGameReward;
		return distance < 1.9f ? 4 * m_BaseMiniGameReward : 3 * m_BaseMiniGameReward;
	}

	public void OnRagdollHit()
	{
		m_HitParticles.transform.position = m_CrossHair.TransformPoint(m_CrossHair.position);
		m_Target.localPosition = Vector3.zero;
		m_Target.DOShakePosition(0.48f, new Vector3(12f, 0f, 0f), 10, 0f, false, true, ShakeRandomnessMode.Full);
		m_HitParticles.Play();
		m_SoundManager?.PlaySFX(m_HitSound, 0.75f);
		m_MoneyExplosion_1?.Play();
		m_MoneyExplosion_2?.Play();
	}

	private void initSpline()
	{
		for (int i = 0; i < m_Points.Length; i++)
		{
			SplinePoint point = m_Spline.GetPoint(i, SplineComputer.Space.World);
			point.position = m_Points[i].Position;
			point.normal = m_Points[i].Normal;
			m_Spline.SetPoint(i, point, SplineComputer.Space.Local);
		}
		m_Follower.follow = true;
		m_Follower.onMotionApplied += UpdateUI;
	}

}
