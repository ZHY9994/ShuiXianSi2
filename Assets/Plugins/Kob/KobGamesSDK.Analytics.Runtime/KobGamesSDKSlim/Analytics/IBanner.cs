using KobGamesSDKSlim.Ads;

namespace KobGamesSDKSlim.Analytics
{
	public interface IBanner
	{
		void OnBannerShownSuccess(string i_Placement, DynamicDataBase i_DynamicData);

		void OnBannerShownFailed(string i_Placement, IAdNetworkError i_AdNetworkError, DynamicDataBase i_DynamicData);
	}
}
