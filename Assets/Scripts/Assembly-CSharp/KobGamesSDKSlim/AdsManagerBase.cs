using System;
using KobGamesSDKSlim.Ads;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[RequireComponent(typeof(UnityMainThreadDispatcher))]
	[ExecutionOrder(-80)]
	public class AdsManagerBase : Singleton<AdsManager>
	{
		public bool GDPRConsent = true;

		public bool AdsAlwaysAvailableInEditor;

		public bool AutoShowBanner = true;

		public bool ShowDummyBgPanelEditor = true;

		public bool ShowDummyBgPanel;

		public float BannerDelayStart = 1f;

		public int RemoteConfigTimeoutSeconds;

		public bool ShowInterstitialPreview = true;

		public AdsInterstitialPreview AdsInterstitialPreview;

		public IMediationNetwork IMediationNetwork;

		public bool IsInit;

		public InterstitialHandler InterstitialHandler = new InterstitialHandler();

		private BannerEvents m_BannerEventsDummy = new BannerEvents();

		private InterstitialEvents m_InterstitialEventsDummy = new InterstitialEvents();

		private RewardVideoEvents m_RewardVideoCallbacksDummy = new RewardVideoEvents();

		public static BannerEvents BannerEventsGlobal = new BannerEvents();

		public static InterstitialEvents InterstitialEventsGlobal = new InterstitialEvents();

		public static RewardVideoEvents RewardVideoEventsGlobal = new RewardVideoEvents();

		public static Action OnAdsMediationInitialized = delegate { };

		public static Action OnBannerShown = delegate { };

		public static Action OnBannerHidden = delegate { };

		public static Action<bool> OnRewardVideoAvailabilityChange = delegate { };

		private const float k_RVAvailbilityCounterCheckerTimer = 2f;

		private float m_RVAvailbilityCounterChecker = 2f;

		private bool m_RVAvailabilityState;

		private static bool m_ForceExitPlayMode = false;

		private bool m_WasInitMediationCalled;

		private float m_InitTime = 99999f;

		private bool m_BannerSetup;

		private bool m_FoundBanner;

		protected bool m_IsLevelCompleted;

		protected bool m_IsRewardVideoShownOnThisLevel;

		private bool m_WasDebugKeyPressed;

		public bool IsBannerVisible { get; private set; }

		public bool IsAdsMediationRemotelyEnabled => false;

		public bool IsRemovedAds => true;

		public bool IsBannerRemotelyEnabled => false;

		public bool IsInterstitialLoaded => false;

		public bool IsInterstitialRemotelyEnabled => false;

		public bool IsRewardVideoLoaded => true;

		public bool IsRewardVideoRemotelyEnabled => true;

		public void ShowInterstitialTimedBased_Debug()
		{
			return;
		}

		public void ShowInterstitialNormal_Debug()
		{
			return;
		}

		public void ShowRewardVideo_Debug()
		{
			return;
		}

		public void InvokeInterstitialShown()
		{
			return;
		}

		public void InvokeBannerShown()
		{
			return;
		}

		public void InvokeBannerHidden()
		{
			return;
		}

		public void InvokeDestroyBanner()
		{
			return;
		}

		public void SetRefs()
		{
			if (IMediationNetwork == null) SetIMediationRefs();
			if (GetComponent<UnityMainThreadDispatcher>() == null) gameObject.AddComponent<UnityMainThreadDispatcher>();
		}

		private void OnValidate()
		{
			if (RemoteConfigTimeoutSeconds < 0) RemoteConfigTimeoutSeconds = 0;
		}

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			AdsAlwaysAvailableInEditor = AdsAlwaysAvailableInEditor && Application.isEditor;
			SetRefs();
			if (m_BannerEventsDummy == null) m_BannerEventsDummy = new BannerEvents();
			if (m_InterstitialEventsDummy == null) m_InterstitialEventsDummy = new InterstitialEvents();
			if (m_RewardVideoCallbacksDummy == null) m_RewardVideoCallbacksDummy = new RewardVideoEvents();
			BannerEventsGlobal = m_BannerEventsDummy;
			InterstitialEventsGlobal = m_InterstitialEventsDummy;
			RewardVideoEventsGlobal = new RewardVideoEvents();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		public override void OnDisable()
		{
			base.OnDisable();
		}

		public void SetIMediationRefs()
		{
			IMediationNetwork = GetComponent<IMediationNetwork>();
			IMediationNetwork?.SetRefs();
		}

		public void Update()
		{
			m_RVAvailbilityCounterChecker -= Time.deltaTime;
			if (m_RVAvailbilityCounterChecker <= 0f) { m_RVAvailbilityCounterChecker = k_RVAvailbilityCounterCheckerTimer; onRewardVideoAvailabilityChange(true); }
		}

		private void onRewardVideoAvailabilityChange(bool i_State)
		{
			m_RVAvailabilityState = i_State;
			OnRewardVideoAvailabilityChange?.Invoke(i_State);
		}

		public override void OnDestroy()
		{
			if (IMediationNetwork != null) IMediationNetwork.OnInitializedCallback -= onAdsMediationInitialized;
			base.OnDestroy();
		}

		private void RemoteConfigTimeout()
		{
			InitMediation();
		}

		public void OnFirebaseRemotConfigUpdated(bool i_UpdateResult)
		{
			if (i_UpdateResult) InitMediation();
		}

		public void InitMediation()
		{
			if (m_WasInitMediationCalled) return;
			m_WasInitMediationCalled = true;
			m_InitTime = Time.realtimeSinceStartup;
			onAdsMediationInitialized();
		}

		private void onAdsMediationInitialized()
		{
			IsInit = true;
			OnAdsMediationInitialized?.Invoke();
		}

		private void DelayedStart()
		{
			return;
		}

		public void SetShortcutKeyPress()
		{
			m_WasDebugKeyPressed = true;
		}

		protected virtual void LevelLoaded()
		{
			m_IsRewardVideoShownOnThisLevel = false;
		}

		protected virtual void LevelCompleted()
		{
			m_IsLevelCompleted = true;
		}

		protected virtual void LevelFailed()
		{
			m_IsLevelCompleted = false;
		}

		public virtual void TryShowInterstitial(bool i_IsLevelCompleted)
		{
			return;
		}

		protected virtual void ShowInterstitialOnLaunch()
		{
			return;
		}

		[Obsolete("Use the other method. This one has a default Placement which is not advisable")]
		public void ShowBanner(BannerEvents.BannerShown i_ShownCallback = null, string i_PlacementId = "main", BannerEvents.BannerHidden i_HiddenCallback = null, BannerEvents.BannerFailed i_FailedCallback = null, BannerEvents.BannerDestroyed i_DestroyedCallback = null)
		{
			ShowBanner(i_PlacementId, i_ShownCallback, i_HiddenCallback, i_FailedCallback, i_DestroyedCallback);
		}

		public void ShowBanner(string i_PlacementId, BannerEvents.BannerShown i_ShownCallback = null, BannerEvents.BannerHidden i_HiddenCallback = null, BannerEvents.BannerFailed i_FailedCallback = null, BannerEvents.BannerDestroyed i_DestroyedCallback = null)
		{
			IsBannerVisible = false;
			i_HiddenCallback?.Invoke(i_PlacementId);
			OnBannerHidden?.Invoke();
		}

		public void HideBanner()
		{
			IsBannerVisible = false;
			OnBannerHidden?.Invoke();
		}

		public void DestroyBanner()
		{
			IsBannerVisible = false;
		}

		public void LoadInterstitial()
		{
		}

		public void ShowInterstitial(string i_PlacementId, InterstitialEvents.InterstitialShown i_InterstitialShown = null, InterstitialEvents.InterstitialOpen i_InterstitialOpen = null, InterstitialEvents.InterstitialClose i_InterstitialClose = null, InterstitialEvents.InterstitialShownFail i_InterstitialShownFail = null)
		{
			return;
		}

		public eRewardVideoCallResult ShowRewardVideo(eRewardVideoPlacements i_RVPlacement, Action i_CloseWithSuccessCallback = null, Action i_CloseWithFailCallback = null)
		{
			return ShowRewardVideo(i_RVPlacement, (RewardVideoEvents.RewardVideoSuccess)null, (RewardVideoEvents.RewardVideoOpen)null, (p, s) => { if (s) i_CloseWithSuccessCallback?.Invoke(); else i_CloseWithFailCallback?.Invoke(); }, (p, e) => i_CloseWithFailCallback?.Invoke());
		}

		public eRewardVideoCallResult ShowRewardVideo(eRewardVideoPlacements i_RVPlacement, RewardVideoEvents.RewardVideoSuccess i_SuccessCallback = null, RewardVideoEvents.RewardVideoOpen i_OpenCallback = null, RewardVideoEvents.RewardVideoClose i_CloseCallback = null, RewardVideoEvents.RewardVideoFail i_FailCallback = null)
		{
			m_IsRewardVideoShownOnThisLevel = true;
			return eRewardVideoCallResult.AdsMediationNotInitialized;
		}

		private void onRewardVideoClosed(string i_PlacementName, bool i_IsRVSuccess)
		{
			m_IsRewardVideoShownOnThisLevel = true;
		}

		private void onInterstitialShown(eVideoAdsSuccessResult i_Result, string i_PlacementName)
		{
			IsInit = true;
		}

		private void onBannerShown(string i_PlacementName)
		{
			IsBannerVisible = true;
			OnBannerShown?.Invoke();
		}

		private void onBannerHidden(string i_PlacementName)
		{
			IsBannerVisible = false;
			OnBannerHidden?.Invoke();
		}

		private new void OnApplicationPause(bool i_Pause)
		{
			return;
		}

		public void RemoveAds()
		{
			HideBanner();
		}

		public void ShowMediationDebugPanel()
		{
			return;
		}
	}
}
