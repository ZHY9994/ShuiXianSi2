using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unity.Usercentrics
{
	public class Usercentrics : Singleton<Usercentrics>
	{
		[Header("Enter either Settings ID or Ruleset ID")]
		[SerializeField]
		public string SettingsID;

		[SerializeField]
		public string RulesetID;

		[SerializeField]
		public UsercentricsOptions Options;

		private readonly IUsercentricsPlatform UsercentricsPlatform;

		private UnityAction<UsercentricsConsentUserResponse> onDismissCallback;

		private UnityAction<UsercentricsReadyStatus> initializeCallback;

		private UnityAction<string> initializeErrorCallback;

		private UnityAction<TCFData> tcfDataCallback;

		private List<UnityAction<UsercentricsUpdatedConsentEvent>> onConsentUpdatedCallbacks;

		private List<UnityAction<UsercentricsMediationEvent>> onConsentMediationCallbacks;

		private UnityAction<UsercentricsReadyStatus> restoreSessionSuccessCallback;

		private UnityAction<string> restoreSessionErrorCallback;

		private UnityAction<UsercentricsReadyStatus> clearSessionSuccessCallback;

		private UnityAction<string> clearSessionErrorCallback;

		public bool IsInitialized { get; private set; }

		public void Initialize(UnityAction<UsercentricsReadyStatus> initializeCallback, UnityAction<string> initializeErrorCallback)
		{
			this.initializeCallback = initializeCallback;
			this.initializeErrorCallback = initializeErrorCallback;
			ensureSupportedPlatform();
			UsercentricsPlatform.Initialize(JsonUtility.ToJson(UsercentricsOptionsInternal.CreateFrom(Options, SettingsID, RulesetID)));
		}

		private void Awake()
		{
			onConsentUpdatedCallbacks = new List<UnityAction<UsercentricsUpdatedConsentEvent>>();
			onConsentMediationCallbacks = new List<UnityAction<UsercentricsMediationEvent>>();
		}

		public bool IsPlatformSupported()
		{
			return UsercentricsPlatform != null;
		}

		public void ShowFirstLayer(UnityAction<UsercentricsConsentUserResponse> onDismissCallback)
		{
			ShowFirstLayer(null, onDismissCallback);
		}

		public void ShowFirstLayer(BannerSettings bannerSettings, UnityAction<UsercentricsConsentUserResponse> onDismissCallback)
		{
			ensureSupportedPlatform(); ensureInitialized(); this.onDismissCallback = onDismissCallback;
			UsercentricsPlatform.ShowFirstLayer(JsonUtility.ToJson(BuildBannerSettingsWithOptions(bannerSettings)));
		}

		public void ShowSecondLayer(BannerSettings bannerSettings, UnityAction<UsercentricsConsentUserResponse> onDismissCallback)
		{
			ensureSupportedPlatform(); ensureInitialized(); this.onDismissCallback = onDismissCallback;
			UsercentricsPlatform.ShowSecondLayer(JsonUtility.ToJson(BuildBannerSettingsWithOptions(bannerSettings)));
		}

		public string GetControllerId()
		{
			ensureSupportedPlatform(); ensureInitialized(); return UsercentricsPlatform.GetControllerID();
		}

		public void GetTCFData(UnityAction<TCFData> callback)
		{
			ensureSupportedPlatform(); ensureInitialized(); tcfDataCallback = callback; UsercentricsPlatform.GetTCFData();
		}

		public CCPAData GetUSPData()
		{
			ensureSupportedPlatform(); ensureInitialized(); return JsonUtility.FromJson<CCPAData>(UsercentricsPlatform.GetUSPData());
		}

		public FirstLayerSettings GetFirstLayerSettings()
		{
			ensureSupportedPlatform(); ensureInitialized(); return JsonUtility.FromJson<FirstLayerSettings>(UsercentricsPlatform.GetFirstLayerSettings());
		}

		public void AcceptAll()
		{
			ensureSupportedPlatform(); ensureInitialized(); UsercentricsPlatform.AcceptAll();
		}

		public void DenyAll()
		{
			ensureSupportedPlatform(); ensureInitialized(); UsercentricsPlatform.DenyAll();
		}

		public void RestoreUserSession(string controllerId, UnityAction<UsercentricsReadyStatus> successCallback, UnityAction<string> errorCallback)
		{
			ensureSupportedPlatform(); restoreSessionSuccessCallback = successCallback; restoreSessionErrorCallback = errorCallback; UsercentricsPlatform.RestoreUserSession(controllerId);
		}

		public void SubscribeOnConsentUpdated(UnityAction<UsercentricsUpdatedConsentEvent> callback)
		{
			if (callback != null) onConsentUpdatedCallbacks.Add(callback); ensureSupportedPlatform(); UsercentricsPlatform.SubscribeOnConsentUpdated();
		}

		public void DisposeOnConsentUpdatedSubscription()
		{
			onConsentUpdatedCallbacks.Clear(); ensureSupportedPlatform(); UsercentricsPlatform.DisposeOnConsentUpdatedSubscription();
		}

		public void SubscribeOnConsentMediation(UnityAction<UsercentricsMediationEvent> callback)
		{
			if (callback != null) onConsentMediationCallbacks.Add(callback); ensureSupportedPlatform(); UsercentricsPlatform.SubscribeOnConsentMediation();
		}

		public void DisposeOnConsentMediationSubscription()
		{
			onConsentMediationCallbacks.Clear(); ensureSupportedPlatform(); UsercentricsPlatform.DisposeOnConsentMediationSubscription();
		}

		public void Track(UsercentricsAnalyticsEventType eventType)
		{
			ensureSupportedPlatform(); ensureInitialized(); UsercentricsPlatform.Track((int)eventType);
		}

		public void SetCmpId(int cmpId)
		{
			ensureSupportedPlatform(); ensureInitialized(); UsercentricsPlatform.SetCmpId(cmpId);
		}

		public CmpData GetCmpData()
		{
			ensureSupportedPlatform(); ensureInitialized(); return JsonUtility.FromJson<CmpData>(UsercentricsPlatform.GetCmpData());
		}

		public void SetABTestingVariant(string variant)
		{
			ensureSupportedPlatform(); ensureInitialized(); UsercentricsPlatform.SetABTestingVariant(variant);
		}

		public string GetABTestingVariant()
		{
			ensureSupportedPlatform(); ensureInitialized(); return UsercentricsPlatform.GetABTestingVariant();
		}

		public AdditionalConsentModeData GetAdditionalConsentModeData()
		{
			ensureSupportedPlatform(); ensureInitialized(); return JsonUtility.FromJson<AdditionalConsentModeData>(UsercentricsPlatform.GetAdditionalConsentModeData());
		}

		public List<UsercentricsServiceConsent> GetConsents()
		{
			ensureSupportedPlatform(); ensureInitialized(); return JsonUtility.FromJson<UsercentricsConsentsHolder>(UsercentricsPlatform.GetConsents()).consents;
		}

		public void ClearUserSession(UnityAction<UsercentricsReadyStatus> successCallback, UnityAction<string> errorCallback)
		{
			ensureSupportedPlatform(); clearSessionSuccessCallback = successCallback; clearSessionErrorCallback = errorCallback; UsercentricsPlatform.ClearUserSession();
		}

		private void ensureInitialized()
		{
			if (!IsInitialized) throw new NotInitializedException();
		}

		private void ensureSupportedPlatform()
		{
			if (!IsPlatformSupported()) throw new PlatformSupportException();
		}

		private void ShowEditorNotSupportedDialogIfNeeded()
		{
			return;
		}

		private void logDebug(string message)
		{
			Debug.Log("Usercentrics: " + message);
		}

		private BannerSettings BuildBannerSettingsWithOptions(BannerSettings bannerSettings)
		{
			if (bannerSettings == null) bannerSettings = new BannerSettings();
			if (bannerSettings.generalStyleSettings == null) bannerSettings.generalStyleSettings = GetGeneralStyleSettingsFromOptions();
			return bannerSettings;
		}

		private GeneralStyleSettings GetGeneralStyleSettingsFromOptions()
		{
			return new GeneralStyleSettings();
		}

		internal void HandleInitSuccess(string rawUsercentricsReadyStatus)
		{
			IsInitialized = true;
			initializeCallback?.Invoke(JsonUtility.FromJson<UsercentricsReadyStatus>(rawUsercentricsReadyStatus));
		}

		internal void HandleInitError(string errorMessage)
		{
			initializeErrorCallback?.Invoke(errorMessage);
		}

		internal void HandleBannerResponse(string rawUsercentricsConsentUserResponse)
		{
			onDismissCallback?.Invoke(JsonUtility.FromJson<UsercentricsConsentUserResponse>(rawUsercentricsConsentUserResponse));
		}

		internal void HandleRestoreSuccess(string rawUsercentricsReadyStatus)
		{
			restoreSessionSuccessCallback?.Invoke(JsonUtility.FromJson<UsercentricsReadyStatus>(rawUsercentricsReadyStatus));
		}

		internal void HandleRestoreError(string errorMessage)
		{
			restoreSessionErrorCallback?.Invoke(errorMessage);
		}

		internal void HandleTCFData(string rawTCFData)
		{
			tcfDataCallback?.Invoke(JsonUtility.FromJson<TCFData>(rawTCFData));
		}

		internal void HandleOnConsentUpdated(string rawUsercentricsUpdatedConsentEvent)
		{
			UsercentricsUpdatedConsentEvent value = JsonUtility.FromJson<UsercentricsUpdatedConsentEvent>(rawUsercentricsUpdatedConsentEvent);
			foreach (UnityAction<UsercentricsUpdatedConsentEvent> callback in onConsentUpdatedCallbacks) callback?.Invoke(value);
		}

		internal void HandleOnConsentMediation(string rawUsercentricsMediationEvent)
		{
			UsercentricsMediationEvent value = JsonUtility.FromJson<UsercentricsMediationEvent>(rawUsercentricsMediationEvent);
			foreach (UnityAction<UsercentricsMediationEvent> callback in onConsentMediationCallbacks) callback?.Invoke(value);
		}

		internal void HandleClearSuccess(string rawUsercentricsReadyStatus)
		{
			clearSessionSuccessCallback?.Invoke(JsonUtility.FromJson<UsercentricsReadyStatus>(rawUsercentricsReadyStatus));
		}

		internal void HandleClearError(string errorMessage)
		{
			clearSessionErrorCallback?.Invoke(errorMessage);
		}
	}
}
