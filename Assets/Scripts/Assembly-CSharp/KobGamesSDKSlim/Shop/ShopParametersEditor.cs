using System;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	[Serializable]
	[CreateAssetMenu(fileName = "Shop parameters", menuName = "KobGames/Shop Parameters")]
	public class ShopParametersEditor : ScriptableObject
	{
		public ShopsData Shops;

		public ShopSettings ShopSettings;

		public ShopEditorSettings ShopEditorSettings;

		public ShopParametersEditor()
		{
		}
	}
}
