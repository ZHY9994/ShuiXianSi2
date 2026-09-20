namespace KobGamesSDKSlim.Ads
{
	public interface IBanner
	{
		void Initialize();

		void ShowBanner(BannerEvents i_BannerEvents);

		void HideBanner();

		void DestroyBanner();

		void SimulateFailedBannerLoad();

		void OnApplicationPause(bool i_Pause);
	}
}
