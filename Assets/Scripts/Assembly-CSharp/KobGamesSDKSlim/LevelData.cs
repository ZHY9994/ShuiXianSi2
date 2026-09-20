using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class LevelData
	{
		[SerializeField]
		private bool m_OverrideDefaultLevelData;

		[SerializeField]
		private string m_LevelName = string.Empty;

		[SerializeField]
		private string m_LevelDifficulty = "normal";

		[SerializeField]
		private string m_LevelType = "normal";

		[SerializeField]
		private string m_GameMode = "normal";

		[SerializeField]
		private int m_LevelNumber;

		[SerializeField]
		private bool m_LevelRandom;

		[SerializeField]
		private int m_LevelLoop = 1;

		[SerializeField]
		private int m_LevelDuration;

		[SerializeField]
		private int m_LevelProgress;

		[SerializeField]
		private int m_ContinueAmount;

		[SerializeField]
		private int m_LevelCount;

		public string LevelName => m_LevelName;

		public string LevelDifficult => m_LevelDifficulty;

		public string LevelType => m_LevelType;

		public string GameMode => m_GameMode;

		public int LevelNumber => m_LevelNumber;

		public bool LevelRandom => m_LevelRandom;

		public int LevelLoop => m_LevelLoop;

		public int LevelDuration => m_LevelDuration;

		public int LevelProgress => m_LevelProgress;

		public int ContinueAmount => m_ContinueAmount;

		public int LevelCount => m_LevelCount;

		public bool OverrideDefaultLevelData => m_OverrideDefaultLevelData;

		public ValueDropdownList<string> LevelDifficultyDropDown => new ValueDropdownList<string> { { "Easy", "Easy" }, { "Normal", "Normal" }, { "Hard", "Hard" } };

		public ValueDropdownList<string> LevelTypeDropDown => new ValueDropdownList<string>();

		public void SetMandatoryDataOnStart(string i_GameMode, int i_LevelNumber)
		{
			m_GameMode = i_GameMode;
			m_LevelNumber = i_LevelNumber;
		}

		public void SetMandatoryDataOnEnd(int i_LevelDuration, int i_LevelProgress, int i_ContinueAmount)
		{
			m_LevelDuration = i_LevelDuration;
			m_LevelProgress = i_LevelProgress;
			m_ContinueAmount = i_ContinueAmount;
		}

		public void SetOptionalData(string i_LevelName = null, string i_LevelDifficulty = null, string i_LevelType = null, bool? i_LevelRandom = null, int? i_LevelLoop = null)
		{
			if (i_LevelName != null) m_LevelName = i_LevelName;
			if (i_LevelDifficulty != null) m_LevelDifficulty = i_LevelDifficulty;
			if (i_LevelType != null) m_LevelType = i_LevelType;
			if (i_LevelRandom.HasValue) m_LevelRandom = i_LevelRandom.Value;
			if (i_LevelLoop.HasValue) m_LevelLoop = i_LevelLoop.Value;
		}
	}
}
