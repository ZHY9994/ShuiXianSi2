using System;
using UnityEngine;

[Serializable]
public class BoosterData
{
	[Serializable]
	public class SlotData
	{
		public bool Random = true;

		public eWeaponType WeaponType;

		public int AddCount = 3;
	}

	[Serializable]
	public class SlotDatas
	{
		public SlotData[] Slots = new SlotData[5];
	}

	[Range(0f, 1f)]
	public float BoosterRewardProbability = 0.2f;

	public Sprite[] BoosterSprites;

	public BoosterRevealAnimData RevealAnimData;

	public DictionarySlotData DictionarySlotData;

	public bool ShouldDisplayBoosterReward()
	{
		if (Wayne.Game.Utils.ShopUtils.IsAnyItemBeingUnlocked())
		{
			return false;
		}
		int level = KobGamesSDKSlim.StorageManager.Instance.CurrentLevel;
		return IsThereAnOverrideAvailable(level) || UnityEngine.Random.value <= BoosterRewardProbability;
	}

	public SlotDatas GetOverrideSlotData()
	{
		return DictionarySlotData[KobGamesSDKSlim.StorageManager.Instance.CurrentLevel - 1];
	}

	public bool IsThereAnOverrideAvailable(int i_Level)
	{
		foreach (int level in DictionarySlotData.Keys)
		{
			if (level == i_Level)
			{
				return true;
			}
		}
		return false;
	}
}
