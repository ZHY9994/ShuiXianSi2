namespace KobGamesSDKSlim.Ads
{
	public interface IInterstitial
	{
		void Initialize();

		bool IsInterstitialAvailable();

		void ShowInterstitial(InterstitialEvents i_InterstitialEvents, bool i_IsDummyAd);

		void LoadInterstitial();

		void SimulateShownSuccessAndClose();

		void SimulateFailedInterstitialLoad();
	}
}
