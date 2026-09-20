using System;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;

[Serializable]
public class HapticData
{
	public eHapticEvent HapticEvent;

	[Range(0f, 5f)]
	public float Duration;

	public bool IsWork = true;

	public bool Override;

	public MMFeedbackHaptics.HapticTypes HapticType = (MMFeedbackHaptics.HapticTypes)4;
}
