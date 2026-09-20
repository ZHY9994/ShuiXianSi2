using UnityEngine;

public class WayneTimeModifier : MonoBehaviour
{
	[TextArea]
	public string TIP = "You need 'Wayne Time Manager' For this to work";

	[Header("Time Parameters")]
	public float TargetTimeScale = 1f;

	public float TimeScaleDuration = 1f;

	public bool ShouldLerp;

	public float TimeScaleLerpSpeed = 1f;

	public void SlowWithDuration()
	{
		TimeScaleEvent.Trigger(TimeScaleMethods.For, TargetTimeScale, TimeScaleDuration, ShouldLerp, TimeScaleLerpSpeed, false);
	}

	public void SlowMoWithoutDuration()
	{
		TimeScaleEvent.Trigger(TimeScaleMethods.For, TargetTimeScale, 0f, false, 0f, true);
	}

	public void ResetTimeScale()
	{
		TimeScaleEvent.Trigger(TimeScaleMethods.Reset, TargetTimeScale, 0f, false, 0f, false);
	}
}
