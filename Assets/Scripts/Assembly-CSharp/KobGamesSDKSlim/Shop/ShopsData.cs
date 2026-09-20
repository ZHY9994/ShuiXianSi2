using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	[CreateAssetMenu(fileName = "Shops Data", menuName = "KobGames/Shops Data")]
	public class ShopsData : ScriptableObject
	{
		public SkinShopData Skins;

		public WeaponShopData Weapons;

		private void OnValidate()
		{
			return;
		}

		public ShopsData()
		{
		}
	}
}
