using System;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class UpgradeDataEditor
	{
		[Serializable]
		public class UpgradeData
		{
			public int RequiredNumberOfCards;

			public int Price;

			[Range(1f, 5f)]
			public float Multiplier;
		}

		[Serializable]
		public class UpgradeDictionary : UnitySerializedDictionary<int, UpgradeData>
		{
		}

		[SerializeField]
		private UpgradeDictionary m_UpgradeData = new UpgradeDictionary();

		public int MaxUpgrade => m_UpgradeData.Keys.Count;

		public int Price(int i_Level)
		{
			return GetUpgradeData(i_Level)?.Price ?? 0;
		}
		public float Multiplier(int i_Level)
		{
			return GetUpgradeData(i_Level)?.Multiplier ?? 0f;
		}

		public UpgradeData GetUpgradeData(int i_Level)
		{
			return m_UpgradeData != null && m_UpgradeData.TryGetValue(i_Level, out var value) ? value : null;
		}
	}
}
