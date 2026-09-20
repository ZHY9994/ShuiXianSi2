using KobGamesSDKSlim.iAP;

namespace KobGamesSDKSlim.Analytics
{
	public interface IiAPurchases
	{
		void onPurchaseComplete(iAPProduct i_Product, DynamicDataBase i_DynamicData);
	}
}
