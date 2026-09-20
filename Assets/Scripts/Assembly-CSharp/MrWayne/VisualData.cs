using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class VisualData<T, T2>
	{
		[Serializable]
		protected class SkinDictionary : UnitySerializedDictionary<T, T2>
		{
		}

		[SerializeField]
		protected SkinDictionary m_SkinData;

		public T2 GetSkinData(T i_Key)
		{
			return m_SkinData != null && m_SkinData.TryGetValue(i_Key, out var value) ? value : default(T2);
		}
		public T2 GetRandomSkinData()
		{
			if (m_SkinData == null || m_SkinData.Count == 0) return default(T2); foreach (var value in m_SkinData.Values) return value; return default(T2);
		}
	}
}
