using System;
using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

[Serializable]
public class FaceDataEditor
{
	[Serializable]
	private class FaceTextureDict : UnitySerializedDictionary<string, FaceTextureData>
	{
	}

	[SerializeField]
	private FaceTextureDict m_FaceTextureDict = new FaceTextureDict();

	public Texture GetFace(string i_Name, eFaceType i_Type)
	{
		return m_FaceTextureDict[i_Name].GetRandomFace(i_Type);
	}

	private void setupDictionary()
	{
		m_FaceTextureDict.Clear();
		foreach (SkinItemData skinItem in SkinShopManager.Instance.SkinItems)
			m_FaceTextureDict.Add(skinItem.Name, null);
	}
}
