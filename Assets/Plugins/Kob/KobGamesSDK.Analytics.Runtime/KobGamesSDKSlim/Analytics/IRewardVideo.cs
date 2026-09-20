using KobGamesSDKSlim.Ads;

namespace KobGamesSDKSlim.Analytics
{
	public interface IRewardVideo
	{
		void OnRewardVideoAvailability(string i_Placement, eAdsAvailabilityResult i_AvailabilityResult, DynamicDataBase i_DynamicData);

		void OnRewardVideoOpen(string i_Placement, DynamicDataBase i_DynamicData);

		void OnRewardVideoShownSuccess(eVideoAdsSuccessResult i_Result, string i_Placement, DynamicDataBase i_DynamicData);

		void OnRewardVideoShownFail(string i_Placement, IAdNetworkError i_AdNetworkError, DynamicDataBase i_DynamicData);
	}
}
