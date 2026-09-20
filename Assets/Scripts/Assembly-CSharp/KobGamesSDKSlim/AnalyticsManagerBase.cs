using System;
using System.Collections.Generic;
using KobGamesSDKSlim.Ads;
using KobGamesSDKSlim.Analytics;
using KobGamesSDKSlim.Analytics.EventsData;
using KobGamesSDKSlim.Analytics.Modules;
using KobGamesSDKSlim.iAP;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-200)]
	public abstract class AnalyticsManagerBase : Singleton<AnalyticsManager>
	{
		[Space(10f)]
		[SerializeField]
		protected bool m_AnalyticsEnabled = true;

		[SerializeField]
		private bool m_IsCurrentLevelZeroBased;

		[SerializeField]
		private DisabledEvents m_GlobalDisabledEvents;

		[SerializeField]
		protected EventsSO m_EventConstants;

		[SerializeReference]
		private ModuleManager m_ModuleManager;

		private InitializationDataBase _initializationData;

		private DynamicDataBase _dynamicData;

		public int CurrentLevel => m_IsCurrentLevelZeroBased ? Math.Max(0, (m_StorageData?.CurrentLevel ?? 0) + 1) : (m_StorageData?.CurrentLevel ?? 0);

		public int HighScoreLevel => m_IsCurrentLevelZeroBased ? Math.Max(0, (m_StorageData?.HighScoreLevel ?? 0) + 1) : (m_StorageData?.HighScoreLevel ?? 0);

		private List<Module> m_Modules => m_ModuleManager == null ? null : m_ModuleManager.Modules;

		private StorageManager m_StorageData => StorageManager.Instance;

		protected string m_GameLaunchDetails => string.Format("GameLaunch: {0} HighScoreLevel: {1} TotalAttempts: {2} TotalVideosWatched: {3} Name: {4} Config: {5}", m_StorageData?.GameLaunchCount ?? 0, HighScoreLevel, m_StorageData?.TotalAttempts ?? 0, m_StorageData?.TotalVideosWatched ?? 0, Application.productName, Application.version);

		protected InitializationDataBase m_InitializationData => _initializationData ?? (_initializationData = getNewInitializationData());

		protected DynamicDataBase m_DynamicData => _dynamicData ?? (_dynamicData = getNewDynamicData());

		private void onEnableChanged()
		{
			if (!m_AnalyticsEnabled) return;
			onServicesEnable();
			onEnableImpl();
		}

		protected virtual InitializationDataBase getNewInitializationData()
		{
			return new InitializationDataBase
			{
				i_Events = m_EventConstants
			};
		}

		protected virtual DynamicDataBase getNewDynamicData()
		{
			return new DynamicDataBase();
		}

		private void updateDynamicData()
		{
			DynamicDataBase dynamicData = m_DynamicData;
			if (dynamicData == null)
			{
				throw new NullReferenceException();
			}

			dynamicData.CurrentLevel = CurrentLevel;
			dynamicData.FPSAveragePreviousLevel = FPSManager.FPSAveragePreviousLevel;
			dynamicData.FPSTickPreviousLevel = FPSManager.FPSTickPreviousLevel;
			updateDynamicDataImpl();
		}

		protected virtual void updateDynamicDataImpl()
		{
			return;
		}

		public sealed override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			if (!m_AnalyticsEnabled) return;
			onServicesAwake();
			onAwakeImpl();
		}

		protected sealed override void OnEnable()
		{
			base.OnEnable();
			if (m_AnalyticsEnabled) onEnableChanged();
		}

		public sealed override void OnDisable()
		{
			if (m_AnalyticsEnabled)
			{
				onServicesDisable();
				onDisableImpl();
			}
			base.OnDisable();
		}

		public sealed override void Start()
		{
			base.Start();
			if (m_AnalyticsEnabled) Initialize();
		}

		protected void Update()
		{
			if (m_AnalyticsEnabled) updateDynamicData();
		}

		protected virtual void onAwakeImpl()
		{
			return;
		}

		protected virtual void startImpl()
		{
			return;
		}

		protected virtual void onEnableImpl()
		{
			return;
		}

		protected virtual void onDisableImpl()
		{
			return;
		}

		public void Initialize()
		{
			onServicesStart();
			startImpl();
		}

		private void onServicesEnable()
		{
			if (m_Modules == null) return;
			foreach (Module module in m_Modules) module.OnServiceEnable();
		}

		private void onServicesDisable()
		{
			if (m_Modules == null) return;
			foreach (Module module in m_Modules) module.OnServiceDisable();
		}

		private void onServiceApplicationPause(bool i_PauseStatus)
		{
			if (m_Modules == null) return;
			foreach (Module module in m_Modules) module.OnServiceApplicationPause(i_PauseStatus);
		}

		private void onServicesAwake()
		{
			if (m_Modules == null) return;
			foreach (Module module in m_Modules) module.OnServiceAwake(m_InitializationData);
		}

		private void onServicesStart()
		{
			if (m_Modules == null) return;
			foreach (Module module in m_Modules) module.OnServiceStart();
			Invoke(nameof(onLogServiceStartEvent), 2f);
		}

		private void onLogServiceStartEvent()
		{
			onLogServiceStartEvent(false);
		}

		private void onLogServiceStartEvent(bool i_DummyParam)
		{
			string eventName = EventsNamesDefault.k_Name_Game_Launch.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			if (m_StorageData == null) throw new NullReferenceException(nameof(m_StorageData));
			GADataContainer data = GADataContainer.CreatePooled(eventName, m_StorageData.GameLaunchCount.ToString());
			sendEvent(eventName, data, getDetailsOnLogServiceStartEvent());
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogServiceStartEvent()
		{
			List<EventNameObjectPair> details = EventNameObjectPairListPooled.CreatePooled();
			if (m_StorageData == null) return details;
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Game_LaunchCount.Name, m_StorageData.GameLaunchCount));
			details.Add(EventNameObjectPair.CreatePooled("HighScoreLevel", HighScoreLevel));
			details.Add(EventNameObjectPair.CreatePooled("TotalAttempts", m_StorageData.TotalAttempts));
			details.Add(EventNameObjectPair.CreatePooled("TotalVideosWatched", m_StorageData.TotalVideosWatched));
			return details;
		}

		protected sealed override void OnApplicationPause(bool i_PauseStatus)
		{
			base.OnApplicationPause(i_PauseStatus);
			if (m_AnalyticsEnabled) onServiceApplicationPause(i_PauseStatus);
		}

		protected virtual List<EventNameObjectPair> getDetailsOnApplicationPause(bool i_PauseStatus)
		{
			List<EventNameObjectPair> details = EventNameObjectPairListPooled.CreatePooled();
			details.Add(EventNameObjectPair.CreatePooled(
				EventsNamesDefault.k_Type_App_PauseStatus.Name,
				i_PauseStatus ? EventsNamesDefault.k_Value_General_True.Name : EventsNamesDefault.k_Value_General_False.Name));
			return details;
		}

		private void onLevelLoaded(LevelData i_LevelData)
		{
			string eventName = EventsNamesDefault.k_Name_Level_Load.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			if (i_LevelData == null) throw new ArgumentNullException(nameof(i_LevelData));
			GADataContainer data = GADataContainer.CreatePooled(eventName, i_LevelData.LevelNumber.ToString());
			sendEvent(eventName, data, getDetailsOnLevelLoaded(i_LevelData));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLevelLoaded(LevelData i_LevelData)
		{
			if (i_LevelData == null) return EventNameObjectPairListPooled.CreatePooled();
			List<EventNameObjectPair> details = EventNameObjectPairListPooled.CreatePooled();
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Number.Name, i_LevelData.LevelNumber));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Name.Name, i_LevelData.LevelName));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Count.Name, i_LevelData.LevelCount));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Difficulty.Name, i_LevelData.LevelDifficult));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Loop.Name, i_LevelData.LevelLoop <= 1 ? 1 : i_LevelData.LevelLoop));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Random.Name, i_LevelData.LevelRandom));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Type.Name, i_LevelData.LevelType));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Game_Mode.Name, i_LevelData.GameMode));
			return details;
		}

		private void onLevelStarted(LevelData i_LevelData)
		{
			string eventName = EventsNamesDefault.k_Name_Level_Start.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			if (i_LevelData == null) throw new ArgumentNullException(nameof(i_LevelData));
			GADataContainer data = GADataContainer.CreatePooled(eventName, i_LevelData.LevelNumber.ToString());
			sendEvent(eventName, data, getDetailsOnLevelStarted(i_LevelData));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLevelStarted(LevelData i_LevelData)
		{
			return getDetailsOnLevelLoaded(i_LevelData);
		}

		private void onLevelCompleted(LevelData i_LevelData)
		{
			string eventName = EventsNamesDefault.k_Name_Level_Completed.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			if (i_LevelData == null) throw new ArgumentNullException(nameof(i_LevelData));
			GADataContainer data = GADataContainer.CreatePooled(eventName, i_LevelData.LevelNumber.ToString());
			sendEvent(eventName, data, getDetailsOnLevelCompleted(i_LevelData));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLevelCompleted(LevelData i_LevelData)
		{
			List<EventNameObjectPair> details = getDetailsOnLevelLoaded(i_LevelData);
			if (i_LevelData == null) return details;
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Time.Name, i_LevelData.LevelDuration));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Progress.Name, i_LevelData.LevelProgress));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Continue.Name, i_LevelData.ContinueAmount));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Result.Name, EventsNamesDefault.k_Value_Level_Win.Name));
			return details;
		}

		private void onLevelFailedNoContinue(LevelData i_LevelData)
		{
			string eventName = EventsNamesDefault.k_Name_Level_Failed.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			if (i_LevelData == null) throw new ArgumentNullException(nameof(i_LevelData));
			GADataContainer data = GADataContainer.CreatePooled(eventName, i_LevelData.LevelNumber.ToString());
			sendEvent(eventName, data, getDetailsOnLevelFailedNoContinue(i_LevelData));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLevelFailedNoContinue(LevelData i_LevelData)
		{
			List<EventNameObjectPair> details = getDetailsOnLevelLoaded(i_LevelData);
			if (i_LevelData == null) return details;
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Time.Name, i_LevelData.LevelDuration));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Progress.Name, i_LevelData.LevelProgress));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Continue.Name, i_LevelData.ContinueAmount));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Result.Name, EventsNamesDefault.k_Value_Level_Lose.Name));
			return details;
		}

		private void onLevelContinue(LevelData i_LevelData)
		{
			string eventName = EventsNamesDefault.k_Name_Level_Continue.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			if (i_LevelData == null) throw new ArgumentNullException(nameof(i_LevelData));
			GADataContainer data = GADataContainer.CreatePooled(eventName, i_LevelData.LevelNumber.ToString());
			sendEvent(eventName, data, getDetailsOnLevelContinue(i_LevelData));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLevelContinue(LevelData i_LevelData)
		{
			List<EventNameObjectPair> details = getDetailsOnLevelLoaded(i_LevelData);
			if (i_LevelData != null)
				details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Continue.Name, i_LevelData.ContinueAmount));
			return details;
		}

		private GameLoopEventsData convertLevelDataToGameLoopEventsData(LevelData i_LevelData)
		{
			if (i_LevelData == null) throw new ArgumentNullException(nameof(i_LevelData));
			return new GameLoopEventsData
			{
				LevelNumber = i_LevelData.LevelNumber,
				LevelName = i_LevelData.LevelName,
				LevelCount = i_LevelData.LevelCount,
				LevelDifficulty = i_LevelData.LevelDifficult,
				LevelLoop = i_LevelData.LevelLoop <= 1 ? 1 : i_LevelData.LevelLoop,
				LevelRandom = i_LevelData.LevelRandom,
				LevelType = i_LevelData.LevelType,
				GameMode = i_LevelData.GameMode
			};
		}

		private void logBannerShownSuccess(string i_PlacementName)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_Banner.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			GADataContainer data = GADataContainer.CreatePooled(eventName, EventsNamesDefault.k_Value_Ads_Watched.Name, i_PlacementName, EventsNamesDefault.k_Value_General_True.Name);
			sendEvent(eventName, data, getDetailsOnLogBannerShownSuccess(i_PlacementName));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogBannerShownSuccess(string i_PlacementName)
		{
			return buildAdDetails(i_PlacementName, EventsNamesDefault.k_Value_Ads_Watched.Name, null);
		}

		private void logBannerShownFailed(string i_PlacementName, IAdNetworkError i_AdNetworkError)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_Banner.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			sendEvent(eventName, GADataContainer.CreatePooled(eventName, EventsNamesDefault.k_Value_Ads_Failed.Name, i_PlacementName), getDetailsOnLogBannerShownFailed(i_PlacementName, i_AdNetworkError));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogBannerShownFailed(string i_PlacementName, IAdNetworkError i_AdNetworkError)
		{
			return buildAdDetails(i_PlacementName, EventsNamesDefault.k_Value_Ads_Failed.Name, i_AdNetworkError);
		}

		private void logInterstitialAvailability(string i_PlacementName, eAdsAvailabilityResult i_AvailabilityResult)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_Interstitial.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			sendEvent(eventName, GADataContainer.CreatePooled(eventName, convertAdsAvailabilityResult(i_AvailabilityResult), i_PlacementName), getDetailsOnLogInterstitialAvailability(i_PlacementName, i_AvailabilityResult));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogInterstitialAvailability(string i_PlacementName, eAdsAvailabilityResult i_AvailabilityResult)
		{
			return buildAdAvailabilityDetails(i_PlacementName, i_AvailabilityResult);
		}

		private string convertAdsAvailabilityResult(eAdsAvailabilityResult i_Result)
		{
			switch (i_Result)
			{
				case eAdsAvailabilityResult.Not_Available:
					return EventsNamesDefault.k_Value_Ads_Availability_NotAvailable.Name;
				case eAdsAvailabilityResult.Waited:
					return EventsNamesDefault.k_Value_Ads_Availability_Waited.Name;
				case eAdsAvailabilityResult.Success:
				default:
					return EventsNamesDefault.k_Value_Ads_Availability_Success.Name;
			}
		}

		private void logInterstitialOpen(string i_PlacementName)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_Interstitial.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			GADataContainer data = GADataContainer.CreatePooled(eventName, EventsNamesDefault.k_Value_Ads_Start.Name, i_PlacementName);
			sendEvent(eventName, data, getDetailsOnLogInterstitialOpen(i_PlacementName));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogInterstitialOpen(string i_PlacementName)
		{
			return buildAdDetails(i_PlacementName, EventsNamesDefault.k_Value_Ads_Start.Name, null);
		}

		private void logInterstitialShownSuccess(eVideoAdsSuccessResult i_Result, string i_PlacementName)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_Interstitial.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			string result = i_Result == eVideoAdsSuccessResult.Clicked ? EventsNamesDefault.k_Value_Ads_Clicked.Name : EventsNamesDefault.k_Value_Ads_Watched.Name;
			sendEvent(eventName, GADataContainer.CreatePooled(eventName, result, i_PlacementName), getDetailsOnLogInterstitialShownSuccess(i_Result, i_PlacementName));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogInterstitialShownSuccess(eVideoAdsSuccessResult i_Result, string i_PlacementName)
		{
			return buildAdDetails(i_PlacementName, i_Result == eVideoAdsSuccessResult.Clicked ? EventsNamesDefault.k_Value_Ads_Clicked.Name : EventsNamesDefault.k_Value_Ads_Watched.Name, null);
		}

		private void logInterstitialShownFailed(string i_PlacementName, IAdNetworkError i_AdNetworkError)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_Interstitial.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			sendEvent(eventName, GADataContainer.CreatePooled(eventName, EventsNamesDefault.k_Value_Ads_Failed.Name, i_PlacementName), getDetailsOnLogInterstitialShownFailed(i_PlacementName, i_AdNetworkError));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogInterstitialShownFailed(string i_PlacementName, IAdNetworkError i_AdNetworkError)
		{
			return buildAdDetails(i_PlacementName, EventsNamesDefault.k_Value_Ads_Failed.Name, i_AdNetworkError);
		}

		private void logRewardVideoAvailability(string i_PlacementName, eAdsAvailabilityResult i_AvailabilityResult)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_RewardVideo.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			sendEvent(eventName, GADataContainer.CreatePooled(eventName, convertAdsAvailabilityResult(i_AvailabilityResult), i_PlacementName), getDetailsOnLogRewardVideoAvailability(i_PlacementName, i_AvailabilityResult));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogRewardVideoAvailability(string i_PlacementName, eAdsAvailabilityResult i_AvailabilityResult)
		{
			return buildAdAvailabilityDetails(i_PlacementName, i_AvailabilityResult);
		}

		private void logRewardVideoOpen(string i_PlacementName)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_RewardVideo.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			GADataContainer data = GADataContainer.CreatePooled(eventName, EventsNamesDefault.k_Value_Ads_Start.Name, i_PlacementName);
			sendEvent(eventName, data, getDetailsOnLogRewardVideoOpen(i_PlacementName));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogRewardVideoOpen(string i_PlacementName)
		{
			return buildAdDetails(i_PlacementName, EventsNamesDefault.k_Value_Ads_Start.Name, null);
		}

		private void logRewardVideoShownSuccess(eVideoAdsSuccessResult i_Result, string i_PlacementName)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_RewardVideo.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			string result = i_Result == eVideoAdsSuccessResult.Clicked ? EventsNamesDefault.k_Value_Ads_Clicked.Name : EventsNamesDefault.k_Value_Ads_Watched.Name;
			sendEvent(eventName, GADataContainer.CreatePooled(eventName, result, i_PlacementName), getDetailsOnLogRewardVideoShownSuccess(i_Result, i_PlacementName));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogRewardVideoShownSuccess(eVideoAdsSuccessResult i_Result, string i_PlacementName)
		{
			return buildAdDetails(i_PlacementName, i_Result == eVideoAdsSuccessResult.Clicked ? EventsNamesDefault.k_Value_Ads_Clicked.Name : EventsNamesDefault.k_Value_Ads_Watched.Name, null);
		}

		private void logRewardVideoClosed(string i_Placement, bool i_IsRVSuccess)
		{
			if (i_IsRVSuccess) return;
			string eventName = EventsNamesDefault.k_Name_Ads_RewardVideo.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			sendEvent(eventName, GADataContainer.CreatePooled(eventName, EventsNamesDefault.k_Value_Ads_Failed.Name, i_Placement), getDetailsOnLogRewardVideoClosed(i_Placement));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogRewardVideoClosed(string i_PlacementName)
		{
			return buildAdDetails(i_PlacementName, EventsNamesDefault.k_Value_Ads_Failed.Name, null);
		}

		private void logRewardVideoShownFailed(string i_PlacementName, IAdNetworkError i_AdNetworkError)
		{
			string eventName = EventsNamesDefault.k_Name_Ads_RewardVideo.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			sendEvent(eventName, GADataContainer.CreatePooled(eventName, EventsNamesDefault.k_Value_Ads_Failed.Name, i_PlacementName), getDetailsOnLogRewardVideoShownFailed(i_PlacementName, i_AdNetworkError));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnLogRewardVideoShownFailed(string i_PlacementName, IAdNetworkError i_AdNetworkError)
		{
			return buildAdDetails(i_PlacementName, EventsNamesDefault.k_Value_Ads_Failed.Name, i_AdNetworkError);
		}

		private List<EventNameObjectPair> buildAdDetails(string i_PlacementName, string i_Result, IAdNetworkError i_Error)
		{
			List<EventNameObjectPair> details = EventNameObjectPairListPooled.CreatePooled();
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Ads_Placement.Name, i_PlacementName));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Ads_Result.Name, i_Result));
			if (i_Error != null)
			{
				details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Ads_ErrorCode.Name, i_Error.getErrorCode()));
				details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Ads_ErrorDesc.Name, i_Error.getDescription()));
			}
			return details;
		}

		private List<EventNameObjectPair> buildAdAvailabilityDetails(string i_PlacementName, eAdsAvailabilityResult i_Result)
		{
			return buildAdDetails(i_PlacementName, convertAdsAvailabilityResult(i_Result), null);
		}

		private void onIAP_PurchaseComplete(iAPProduct i_Product)
		{
			if (i_Product == null || i_Product.definition == null) return;
			string eventName = EventsNamesDefault.k_Name_iAP_PaymentSucceed.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			GADataContainer data = GADataContainer.CreatePooled(eventName, EventsNamesDefault.k_Type_iAP_StoreSpecificID.Name, i_Product.definition.storeSpecificId);
			sendEvent(eventName, data, getDetailsOnIAP_PurchaseComplete(i_Product));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnIAP_PurchaseComplete(iAPProduct i_Product)
		{
			return buildIAPDetails(i_Product);
		}

		private void onIAP_PurchaseFailed(iAPProduct i_Product, ePurchaseFailureReason i_Reason)
		{
			return;
		}

		protected virtual List<EventNameObjectPair> getDetailsOnIAP_PurchaseFailed(iAPProduct i_Product)
		{
			return buildIAPDetails(i_Product);
		}

		private void onIAP_PurchaseRestoreComplete(iAPProduct i_Product)
		{
			return;
		}

		protected virtual List<EventNameObjectPair> getDetailsOnIAP_PurchaseRestoreComplete(iAPProduct i_Product)
		{
			return buildIAPDetails(i_Product);
		}

		private void onIAP_PurchaseRestoreFailed(iAPProduct i_Product, ePurchaseFailureReason i_Reason)
		{
			return;
		}

		protected virtual List<EventNameObjectPair> getDetailsOnIAP_PurchaseRestoreFailed(iAPProduct i_Product)
		{
			return buildIAPDetails(i_Product);
		}

		private void onGameAwakeAfterGameClosed(LevelData i_LevelData)
		{
			string eventName = EventsNamesDefault.k_Name_Game_AwakeAfterGameClosed.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			if (i_LevelData == null) throw new ArgumentNullException(nameof(i_LevelData));
			GADataContainer data = GADataContainer.CreatePooled(
				eventName,
				i_LevelData.LevelNumber.ToString(),
				EventsNamesDefault.k_Value_Game_Closed.Name);
			sendEvent(eventName, data, getDetailsOnGameAwakeAfterLostFocus(i_LevelData));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnGameAwakeAfterLostFocus(LevelData i_LevelData)
		{
			List<EventNameObjectPair> details = getDetailsOnLevelCompleted(i_LevelData);
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_Level_Result.Name, EventsNamesDefault.k_Value_Game_Closed.Name));
			return details;
		}

		private List<EventNameObjectPair> buildIAPDetails(iAPProduct i_Product)
		{
			List<EventNameObjectPair> details = EventNameObjectPairListPooled.CreatePooled();
			if (i_Product == null) return details;
			if (i_Product.metadata != null)
			{
				details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_iAP_LocalizedPrice.Name, i_Product.metadata.localizedPrice));
				details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_iAP_LocalizedTitle.Name, i_Product.metadata.localizedTitle));
				details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_iAP_IsoCurrencyCode.Name, i_Product.metadata.isoCurrencyCode));
			}
			if (i_Product.definition != null)
				details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_iAP_StoreSpecificID.Name, i_Product.definition.storeSpecificId));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_iAP_HasReceipt.Name, i_Product.hasReceipt));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_iAP_ReceiptID.Name, i_Product.receipt));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_iAP_TransactionID.Name, i_Product.transactionID));
			return details;
		}

		private void onRateUs(string i_ShowReason, string i_RateUsResult, int i_NumberOfStars = 0)
		{
			string eventName = EventsNamesDefault.k_Name_RateUs.Name;
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(eventName)) return;
			string resultName = EventsNamesDefault.k_Type_RateUs_Result.Name;
			GADataContainer data = GADataContainer.CreatePooled(eventName, resultName, i_NumberOfStars.ToString());
			sendEvent(eventName, data, getDetailsOnRateUs(i_ShowReason, i_RateUsResult, i_NumberOfStars));
		}

		protected virtual List<EventNameObjectPair> getDetailsOnRateUs(string i_ShowReason, string i_RateUsResult, int i_NumberOfStars = 0)
		{
			List<EventNameObjectPair> details = EventNameObjectPairListPooled.CreatePooled();
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_RateUs_ShowReason.Name, i_ShowReason));
			details.Add(EventNameObjectPair.CreatePooled(EventsNamesDefault.k_Type_RateUs_Result.Name, i_RateUsResult == "Yes" ? i_NumberOfStars.ToString() : "0"));
			return details;
		}

		protected void sendEvent(string i_EventName, GADataContainer i_GAData = null, List<EventNameObjectPair> i_Details = null)
		{
			if (m_GlobalDisabledEvents != null && m_GlobalDisabledEvents.Contains(i_EventName)) return;
			if (m_Modules == null) return;
			foreach (Module module in m_Modules) module.OnSendEvent(i_EventName, m_DynamicData, i_GAData, i_Details);
		}

		[Obsolete("Don't use. It will be removed in future iterations")]
		public void LogEvent(string i_EventCategory, string i_EventAction, string i_EventLabel = "None", long i_Value = 0L, params kv[] i_FirebaseEventTypes)
		{
			List<EventNameObjectPair> details = EventNameObjectPairListPooled.CreatePooled();
			if (i_FirebaseEventTypes != null)
			{
				for (int i = 0; i < i_FirebaseEventTypes.Length; i++)
				{
					details.Add(i_FirebaseEventTypes[i].ToKeyValuePair());
				}
			}
			if (i_Value != 0L)
			{
				details.Add(EventNameObjectPair.CreatePooled("Value", i_Value));
			}
			sendEvent(i_EventCategory, GADataContainer.CreatePooled(i_EventCategory, i_EventAction, i_EventLabel), details);
		}

		[Obsolete("Don't use. It will be removed in future iterations")]
		public virtual void LogAppMetricaEvent(string i_EventName, params Tuple<string, object>[] i_KeyPairs)
		{
			sendEvent(i_EventName, null, ModuleUtils.TupleToStringObjectPair(i_KeyPairs));
		}

		[Obsolete]
		public void LogOpenUrlEvent()
		{
			return;
		}

		[Obsolete]
		public void LogLevelProgress(params kv[] i_Params)
		{
			LogEvent(Utils.GetFuncName(nameof(LogLevelProgress)), "LevelId", CurrentLevel.ToString("00"), 0L, i_Params);
		}

		[Obsolete]
		public void LogOpenFromNotification(string i_Name)
		{
			return;
		}

		[Obsolete]
		public void LogEventCritical(string i_EventAction, string i_EventLabel, long i_Value)
		{
			return;
		}
	}
}
