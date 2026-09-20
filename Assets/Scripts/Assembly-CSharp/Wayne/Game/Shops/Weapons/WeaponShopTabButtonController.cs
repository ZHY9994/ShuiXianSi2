using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim.Shop;

namespace Wayne.Game.Shops.Weapons
{
	public class WeaponShopTabButtonController : TTabButtonController
	{
		public static event Action<eItemRarity> OnTabUpdate = delegate { };

		protected override void OnEnable()
		{
			base.OnEnable();
			WeaponTabButton.OnTabSelected += onTabSelected;
			WeaponShopManager.OnShopOpen += onShopOpen;
		}

		protected override void OnDisable()
		{
			WeaponTabButton.OnTabSelected -= onTabSelected;
			WeaponShopManager.OnShopOpen -= onShopOpen;
		}

		protected override void onTabSelected(eItemRarity i_Rarity)
		{
			base.onTabSelected(i_Rarity);
			OnTabUpdate?.Invoke(i_Rarity);
		}

		protected override void onShopOpen()
		{
			base.onShopOpen();
			eItemRarity[] used = WeaponShopManager.Instance.GetUsedRarityCount();
			if (used == null) return;
			for (int i = 0; i < used.Length; i++)
			{
				for (int j = 0; j < m_TabButtons.Length; j++)
				{
					if (m_TabButtons[j].Rarity == used[i])
					{
						m_TabButtons[j].Show();
						m_TabButtons[j].Initialize(0);
						}
				}
			}
		}

	}
}
