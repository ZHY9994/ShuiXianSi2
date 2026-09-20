using System;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class LevelsVariablesEditor : LevelsVariablesEditorBase
	{
		public bool UseTestLevel;

		public Level m_TestLevel;

		public Level[] Levels;

		public Level GetLevel()
		{
			int currentLevel = StorageManager.Instance.CurrentLevel;
			return Levels[(currentLevel - 1) % Levels.Length];
		}

		public void SetLoopedLevel()
		{
			if (StorageManager.Instance.LevelLoop < Levels.Length - 1)
				return;
			if (StorageManager.Instance.LevelLoop % Levels.Length == 0)
				StorageManager.Instance.LevelLoop++;
		}
	}
}
