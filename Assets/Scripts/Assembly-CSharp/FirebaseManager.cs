using System.Threading.Tasks;
//using Firebase.Analytics;
//using Firebase.Extensions;
//using Firebase.RemoteConfig;
using KobGamesSDKSlim;

public class FirebaseManager : Singleton<FirebaseManager>
{
	//private FirebaseApp m_App;

	public static bool IsInit { get; private set; }

	public override void OnAwakeEvent()
	{
		base.OnAwakeEvent();
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}

	public void Initialize()
	{
		/*FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
		{
			if (task.Result != DependencyStatus.Available)
			{
				Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
				IsInit = false;
				return;
			}
			m_App = FirebaseApp.DefaultInstance;
			FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
			FetchDataAsync();
			IsInit = true;
		});*/
	}

	private void onAdOnAdRevenuePaidEventRewarded(string adUnitId, object adInfo)
	{
		callEvents(adInfo);
	}

	private void OnAdRevenuePaidEvent(string adUnitId, object adInfo)
	{
		callEvents(adInfo);
	}

	private void callEvents(object adInfo)
	{
		return;
	}

	public Task FetchDataAsync()
	{
		/*FirebaseRemoteConfig remoteConfig = FirebaseRemoteConfig.DefaultInstance;
		return remoteConfig.FetchAsync(System.TimeSpan.Zero).ContinueWithOnMainThread(FetchComplete);*/
		return null;
	}

	private void FetchComplete(Task fetchTask)
	{
		/*if (fetchTask == null || !fetchTask.IsCompleted)
			return;
		FirebaseRemoteConfig remoteConfig = FirebaseRemoteConfig.DefaultInstance;
		if (remoteConfig.Info.LastFetchStatus != LastFetchStatus.Success)
			return;
		remoteConfig.ActivateAsync().ContinueWithOnMainThread(_ =>
		{
			RemoteSettingsManager.Instance.UpdateRemoteConfig(
				(int)remoteConfig.GetValue("first_interstitial_level").DoubleValue,
				(int)remoteConfig.GetValue("interstitial_cooldown").DoubleValue);
		});*/
	}
}
