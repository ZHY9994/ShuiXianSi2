namespace KobGamesSDKSlim.Ads
{
	public interface IRewardVideo
	{
		void Initialize();

		bool IsRewardVideoAvailable();

		void ShowRewardVideo(RewardVideoEvents i_RewardVideoEvents, bool i_IsDummyAd = false);

		void SimulateShownSuccessAndClose();

		void SimulateFailedRewardVideoLoad();
	}
}
