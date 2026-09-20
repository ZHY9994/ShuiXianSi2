using System;
using System.Collections.Generic;
using KobGamesSDKSlim.Shop;
using UnityEngine;

public class WeaponRaritySelector : MonoBehaviour
{
	[Serializable]
	public class RarityData<T>
	{
		public T Rarity;

		[SerializeField]
		private GameObject[] m_GOs;

		public void Hide()
		{
			if (m_GOs == null)
				return;
			for (int i = 0; i < m_GOs.Length; i++)
			{
				if (m_GOs[i] != null)
					m_GOs[i].SetActive(false);
			}
		}

		public void Show()
		{
			if (m_GOs == null)
				return;
			for (int i = 0; i < m_GOs.Length; i++)
			{
				if (m_GOs[i] != null)
					m_GOs[i].SetActive(true);
			}
		}
	}

	[SerializeField]
	private RarityData<eItemRarity>[] m_Data;

	private TItemData m_Item;

	public void Initialize(TItemData i_Item)
	{
		hideAllData();
		if (i_Item == null)
			return;
		m_Item = i_Item;
		if (m_Data == null)
			return;
		for (int i = 0; i < m_Data.Length; i++)
		{
			if (m_Data[i] != null && EqualityComparer<eItemRarity>.Default.Equals(m_Data[i].Rarity, i_Item.ItemRarity))
				m_Data[i].Show();
		}
	}

	public void Initialize(eItemRarity i_Rarity)
	{
		hideAllData();
		if (m_Data == null)
			return;
		for (int i = 0; i < m_Data.Length; i++)
		{
			if (m_Data[i] != null && EqualityComparer<eItemRarity>.Default.Equals(m_Data[i].Rarity, i_Rarity))
				m_Data[i].Show();
		}
	}

	public void UpdateSelector()
	{
		hideAllData();
		if (m_Item == null || m_Data == null)
			return;
		for (int i = 0; i < m_Data.Length; i++)
		{
			if (m_Data[i] != null && EqualityComparer<eItemRarity>.Default.Equals(m_Data[i].Rarity, m_Item.ItemRarity))
				m_Data[i].Show();
		}
	}

	private void hideAllData()
	{
		if (m_Data == null)
			return;
		for (int i = 0; i < m_Data.Length; i++)
		{
			if (m_Data[i] != null)
				m_Data[i].Hide();
		}
	}
}
