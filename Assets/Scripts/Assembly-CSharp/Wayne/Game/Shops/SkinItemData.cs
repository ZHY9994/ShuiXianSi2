using System;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;

namespace Wayne.Game.Shops
{
	[Serializable]
	public class SkinItemData : TItemData
	{
		public override eItemType ItemType => eItemType.Skin;

		public override bool IsSelected => SkinShopManager.Instance.GetSelectedItem().Index == Index;

		protected override void unlock(bool i_IsVIP = false)
		{
			AnalyticsManager.Instance.SkinUnlock(this, i_IsVIP);
		}
	}
}
