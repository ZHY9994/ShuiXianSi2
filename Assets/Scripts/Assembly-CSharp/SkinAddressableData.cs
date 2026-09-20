using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Wayne.Game.Shops;

[Serializable]
public class SkinAddressableData
{
	[Serializable]
	public class SkinData
	{
		public string Name;

		public eSkinType Type;

		public GameObject Prefab;

		public AssetReferenceGameObject AssetReference;
	}

	[SerializeField]
	private List<SkinData> m_Skins;

	public SkinData GetSkin(string i_Name)
	{
		return m_Skins.FirstOrDefault(x => x.Name == i_Name);
	}

	private void populate()
	{
		m_Skins.Clear();
		foreach (string name in SkinShopManager.Instance.GetItemNames())
			m_Skins.Add(new SkinData { Name = name });
		m_Skins.Sort((x, y) => x.Name.CompareTo(y.Name));
	}
}
