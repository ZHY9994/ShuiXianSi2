using UnityEngine;

namespace KobGamesSDKSlim
{
	public abstract class LevelBase : MonoBehaviour
	{
		[SerializeField]
		private LevelData m_LevelData;

		public LevelData LevelData => m_LevelData;

		protected LevelBase()
		{
			m_LevelData = new LevelData();
		}

		protected virtual void OnValidate()
		{
			if (m_LevelData == null || m_LevelData.OverrideDefaultLevelData)
				return;
			m_LevelData.SetOptionalData(gameObject.name, null, null, null, null);
		}
	}
}
