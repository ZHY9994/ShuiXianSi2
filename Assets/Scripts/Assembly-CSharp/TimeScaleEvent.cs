using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[StructLayout((LayoutKind)0, Size = 1)]
public struct TimeScaleEvent
{
	private static event Action<TimeScaleMethods, float, float, bool, float, bool> OnEvent = delegate { };

	public static void Register(Action<TimeScaleMethods, float, float, bool, float, bool> i_CallBack)
	{
		if (i_CallBack != null)
			OnEvent += i_CallBack;
	}

	public static void UnRegister(Action<TimeScaleMethods, float, float, bool, float, bool> i_CallBack)
	{
		OnEvent -= i_CallBack;
	}

	public static void Trigger(TimeScaleMethods i__TimeScaleMethod, float i_TimeScale, float i_Duration, bool i_Lerp, float i_lerpSpeed, bool i_infinite)
	{
		OnEvent?.Invoke(i__TimeScaleMethod, i_TimeScale, i_Duration, i_Lerp, i_lerpSpeed, i_infinite);
	}
}
