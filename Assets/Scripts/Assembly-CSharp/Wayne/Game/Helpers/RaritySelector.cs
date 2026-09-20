using System;
using KobGamesSDKSlim.Shop;
using UnityEngine;

namespace Wayne.Game.Helpers
{
	public class RaritySelector : MonoBehaviour
	{
		[Serializable]
		private class RarityData
		{
			[SerializeField]
			private eItemRarity m_Rarity;

			[SerializeField]
			private GameObject[] m_Gos;

			public eItemRarity Rarity => m_Rarity;

			public void UpdateVisibility(eItemRarity i_Rarity)
			{
				foreach (GameObject go in m_Gos)
					go.SetActive(i_Rarity == m_Rarity);
			}
		}

		[SerializeField]
		private RarityData[] m_RarityData;

		public void UpdateSelection(eItemRarity i_Rarity)
		{
				foreach (RarityData rarityData in m_RarityData)
					rarityData.UpdateVisibility(i_Rarity);
		}
	}
}
