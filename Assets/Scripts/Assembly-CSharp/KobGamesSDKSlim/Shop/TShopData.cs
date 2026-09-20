using System;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	[Serializable]
	public abstract class TShopData
	{
		public TabData ShopTabData;

		public Vector2 PreviewOffset;

		public Vector2 PreviewSize;

		public bool IsSelectable;

		public eItemRarity[] UsedRarity;

		protected TShopData()
		{
		}
	}
}
