using System;

namespace KobGamesSDKSlim.Ads
{
	public interface IMediationNetwork
	{
		bool IsInit { get; set; }

		IBanner Banner { get; }

		IInterstitial Interstitial { get; }

		IRewardVideo RewardVideo { get; }

		event Action OnInterstitialAdReadyEvent;

		event Action OnInitializedCallback;

		void Init(Action i_InitializedCallback);

		void OnValidate();

		void SetRefs();

		bool IsInterstitialAvailable();

		bool IsRewardVideoAvailable();

		void ShowBanner(BannerEvents i_BannerEvents);

		void ShowInterstitial(InterstitialEvents i_InterstitialEvents, bool i_IsDummyAd = false);

		void ShowRewardVideo(RewardVideoEvents i_RewardVideoEvents, bool i_IsDummyAd = false);

		void HideBanner();

		void DestroyBanner();

		void LoadInterstitial();

		void ShowMediationDebugger();

		void OnApplicationPause(bool i_Pause);
	}
}
