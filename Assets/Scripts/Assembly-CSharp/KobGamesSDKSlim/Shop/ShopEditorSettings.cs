using System;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	[CreateAssetMenu(fileName = "Shop Editor Settings", menuName = "KobGames/Shop Editor Settings")]
	public class ShopEditorSettings : ScriptableObject
	{
		[Serializable]
		public class ItemRarityColorDictionary : UnitySerializedDictionary<eItemRarity, Color>
		{
		}

		[Serializable]
		public class UnlockMethodColorDictionary : UnitySerializedDictionary<eItemUnlockMethod, Color>
		{
		}

		[Serializable]
		public class WeaponRarityColorDictionary : UnitySerializedDictionary<eWeaponRarity, Color>
		{
		}

		[SerializeField]
		private ItemRarityColorDictionary m_ItemRarityColorsEditor = new ItemRarityColorDictionary();

		[SerializeField]
		private UnlockMethodColorDictionary m_UnlockMethodColorsEditor = new UnlockMethodColorDictionary();

		public Color GetItemRarityColorEditor(eItemRarity i_Rarity)
		{
			if (m_ItemRarityColorsEditor != null && m_ItemRarityColorsEditor.Count > 0 && m_ItemRarityColorsEditor.ContainsKey(i_Rarity))
				return m_ItemRarityColorsEditor[i_Rarity];
			return Color.white;
		}

		public Color GetUnlockMethodColorEditor(eItemUnlockMethod i_UnlockMethod)
		{
			if (m_UnlockMethodColorsEditor != null && m_UnlockMethodColorsEditor.Count > 0 && m_UnlockMethodColorsEditor.ContainsKey(i_UnlockMethod))
				return m_UnlockMethodColorsEditor[i_UnlockMethod];
			return Color.white;
		}
	}
}
