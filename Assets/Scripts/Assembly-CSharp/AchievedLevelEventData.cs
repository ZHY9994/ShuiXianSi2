using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AchievedLevelEventData
{
	[Serializable]
	public class EventLevelData
	{
		public int Level;

		public bool Sent;
	}

	[SerializeField]
	private List<EventLevelData> m_DataList = new List<EventLevelData>();

	private List<int> m_EventLevels = new List<int>
		{
			10,
			20,
			30,
			40,
			55,
			70,
			90,
			110,
			130,
			150,
			200,
			250,
			300,
			380,
			460,
			530
		};

	public void Preload()
	{
		List<EventLevelData> data = new List<EventLevelData>();
		for (int i = 0; i < m_EventLevels.Count; i++)
		{
			data.Add(new EventLevelData
			{
				Level = m_EventLevels[i],
				Sent = false
			});
		}
		Save(data);
	}

	public void Save(List<EventLevelData> i_Data)
	{
		m_DataList.Clear();
		if (i_Data != null)
		{
			for (int i = 0; i < i_Data.Count; i++)
			{
				EventLevelData data = i_Data[i];
				m_DataList.Add(new EventLevelData
				{
					Level = data.Level,
					Sent = data.Sent
				});
			}
		}
		KobGamesSDKSlim.Singleton<KobGamesSDKSlim.StorageManager>.Instance.AchievedLevelSave = JsonUtility.ToJson(this);
	}

	public bool Evaluate(int i_Level)
	{
		if (!m_EventLevels.Contains(i_Level))
		{
			return false;
		}
		List<EventLevelData> data = m_DataList.ToList();
		int index = data.FindIndex(x => x.Level == i_Level);
		if (index == -1)
		{
			return false;
		}
		if (index > 0 && !data[index - 1].Sent)
		{
			return false;
		}
		if (data[index].Sent)
		{
			return false;
		}
		data[index].Sent = true;
		Save(data);
		return true;
	}
}
