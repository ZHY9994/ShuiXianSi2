using System;

[Serializable]
public class TimeManagerData
{
	public bool UseSlowMo = true;

	public bool ShouldLerp;

	public float TargetScale = 0.1f;

	public float TimeScaleDuration = 0.4f;

	public float TimeScaleLerpSpeed = 1f;
}
