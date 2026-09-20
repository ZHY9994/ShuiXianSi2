using System;

namespace KobGamesSDKSlim.Shop
{
	[Serializable]
	public class TabData
	{
		[Serializable]
		public class ItemRarityShopTabDataDictionary : UnitySerializedDictionary<eItemRarity, UniqueTabData>
		{
		}

		[Serializable]
		public class UniqueTabData
		{
			public string Label;

			public int ItemsNumber;

			public int ComingSoonNumber;
		}

		public ItemRarityShopTabDataDictionary Tabs;

		private void onTabsChanged()
		{
			return;
		}
	}
}
