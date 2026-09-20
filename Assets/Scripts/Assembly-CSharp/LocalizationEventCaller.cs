using System;

public static class LocalizationEventCaller
{
	private static event Action OnLocalChange = delegate { };

	[UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.AfterSceneLoad)]
	private static void load()
	{
		LocalizationUtility.Initialize();
		Trigger();
	}

	public static void Register(Action i_Callback)
	{
		OnLocalChange += i_Callback;
	}

	public static void UnRegister(Action i_Callback)
	{
		OnLocalChange -= i_Callback;
	}

	public static void Trigger()
	{
		OnLocalChange?.Invoke();
	}
}
