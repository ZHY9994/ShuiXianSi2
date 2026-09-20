using System;
using MoreMountains.FeedbacksForThirdParty;
using Taptics;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class HapticManager : MonoBehaviour
	{
		public void Haptic(MMFeedbackHaptics.HapticTypes i_Haptic)
		{
			if (!StorageManager.Instance.IsVibrationOn)
				return;
			Lofelt.NiceVibrations.HapticPatterns.PlayPreset((Lofelt.NiceVibrations.HapticPatterns.PresetType)i_Haptic);
		}

		[Obsolete("Legacy. Just a wrapper for older projects.")]
		public void Haptic(HapticTypes i_Haptic)
		{
			Haptic((MMFeedbackHaptics.HapticTypes)(int)i_Haptic);
		}
	}
}
