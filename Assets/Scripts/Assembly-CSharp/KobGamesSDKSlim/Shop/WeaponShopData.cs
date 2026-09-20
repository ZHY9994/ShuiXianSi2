using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Wayne.Game.Shops;

namespace KobGamesSDKSlim.Shop
{
	[Serializable]
	public class WeaponShopData : TShopData
	{
		[Serializable]
		private class ItemRarityItemDataArrayDictionary : UnitySerializedDictionary<eItemRarity, ItemDataArray>
		{
		}

		[Serializable]
		private class ItemDataArray
		{
			public WeaponItemData[] Items;
		}

		[SerializeField]
		private List<WeaponItemData> m_Items;

		[SerializeField]
		private ItemRarityItemDataArrayDictionary m_ItemsDictionary;

		public WeaponItemData[] Items => m_Items.ToArray();

		public WeaponItemData[] GetItemsByRarity(eItemRarity i_ItemRarity)
		{
			return m_ItemsDictionary[i_ItemRarity].Items.ToArray();
		}

		public void LockAllItems()
		{
			Singleton<Wayne.Game.Shops.WeaponShopManager>.Instance.LockAllItems();
		}

		public void UnlockAllItems()
		{
			Singleton<Wayne.Game.Shops.WeaponShopManager>.Instance.UnlockAllItems();
		}

		public void OnValidate()
		{
			return;
		}

		private void onValueChanged()
		{
			m_ItemsDictionary = new ItemRarityItemDataArrayDictionary();
			if (m_Items != null)
			{
				for (var i = 0; i < m_Items.Count; i++) m_Items[i].Index = i;
				foreach (var group in m_Items.GroupBy(x => x.ItemRarity))
				{
					var data = new ItemDataArray { Items = group.ToArray() };
					for (var i = 0; i < data.Items.Length; i++) data.Items[i].Index = i;
					m_ItemsDictionary.Add(group.Key, data);
				}
			}
			UsedRarity = m_ItemsDictionary.Keys.ToArray();
		}
	}
}
