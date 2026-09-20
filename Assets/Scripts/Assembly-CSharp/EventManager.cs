using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class EventManager
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct OnPlayerStartAimEvent
	{
		private static Action onEvent;

		public static void Register(Action i_Callback)
		{
			onEvent += i_Callback;
		}

		public static void UnRegister(Action i_Callback)
		{
			onEvent -= i_Callback;
		}

		public static void Trigger()
		{
			onEvent?.Invoke();
		}
	}

	[StructLayout((LayoutKind)0, Size = 1)]
	public struct OnPlayerAimingEvent
	{
		private static Action<float, float> onEvent;

		public static void Register(Action<float, float> i_Callback)
		{
			onEvent += i_Callback;
		}

		public static void UnRegister(Action<float, float> i_Callback)
		{
			onEvent -= i_Callback;
		}

		public static void Trigger(float i_Angle, float i_Force)
		{
			onEvent?.Invoke(i_Angle, i_Force);
		}
	}

	[StructLayout((LayoutKind)0, Size = 1)]
	public struct OnPlayerEndAimEvent
	{
		private static Action onEvent;

		public static void Register(Action i_Callback)
		{
			onEvent += i_Callback;
		}

		public static void UnRegister(Action i_Callback)
		{
			onEvent -= i_Callback;
		}

		public static void Trigger()
		{
			onEvent?.Invoke();
		}
	}

	[StructLayout((LayoutKind)0, Size = 1)]
	public struct OnDeckWeaponChanged
	{
		private static Action onEvent;

		public static void Register(Action i_Callback)
		{
			onEvent += i_Callback;
		}

		public static void UnRegister(Action i_Callback)
		{
			onEvent -= i_Callback;
		}

		public static void Trigger()
		{
			onEvent?.Invoke();
		}
	}

	[StructLayout((LayoutKind)0, Size = 1)]
	public struct OnShopCloseClicked
	{
		private static Action onEvent;

		public static void Register(Action i_Callback)
		{
			onEvent += i_Callback;
		}

		public static void UnRegister(Action i_Callback)
		{
			onEvent -= i_Callback;
		}

		public static void Trigger()
		{
			onEvent?.Invoke();
		}
	}
}
