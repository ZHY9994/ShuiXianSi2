using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[StructLayout((LayoutKind)0, Size = 1)]
public struct FreezeFrameEvent
{
	private static Action<float> OnEvent = delegate { };

	public static void Register(Action<float> i_CallBack)
	{
		OnEvent += i_CallBack;
	}

	public static void UnRegister(Action<float> i_CallBack)
	{
		OnEvent -= i_CallBack;
	}

	public static void Trigger(float i_Duration)
	{
		OnEvent?.Invoke(i_Duration);
	}
}
