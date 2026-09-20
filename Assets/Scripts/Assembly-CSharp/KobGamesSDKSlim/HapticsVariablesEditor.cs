using System;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class HapticsVariablesEditor : HapticsVariablesEditorBase
	{
		public MMFeedbackHaptics.HapticTypes WeaponHit;

		public MMFeedbackHaptics.HapticTypes WeaponExplosion;

		public MMFeedbackHaptics.HapticTypes CharacterDeath;

		[SerializeField]
		private HapticData[] m_HapticData;

		public HapticData GetHapticData(eHapticEvent i_Event)
		{
			if (m_HapticData == null)
				return null;
			for (int i = 0; i < m_HapticData.Length; i++)
			{
				HapticData data = m_HapticData[i];
				if (data != null && data.HapticEvent == i_Event)
					return data;
			}
			return null;
		}
	}
}
