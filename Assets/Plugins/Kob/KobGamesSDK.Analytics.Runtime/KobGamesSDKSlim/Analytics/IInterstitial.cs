using KobGamesSDKSlim.Ads;

namespace KobGamesSDKSlim.Analytics
{
	public interface IInterstitial
	{
		void OnInterstitialAvailability(string i_Placement, eAdsAvailabilityResult i_AvailabilityResult, DynamicDataBase i_DynamicData);

		void OnInterstitialOpen(string i_Placement, DynamicDataBase i_DynamicData);

		void OnInterstitialShownSuccess(eVideoAdsSuccessResult i_Result, string i_Placement, DynamicDataBase i_DynamicData);

		void OnInterstitialShownFail(string i_Placement, IAdNetworkError i_AdNetworkError, DynamicDataBase i_DynamicData);
	}
}
