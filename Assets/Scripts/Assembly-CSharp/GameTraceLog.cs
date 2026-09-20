using System.Diagnostics;

public static class GameTraceLog
{
	[Conditional("WDF_TRACE")]
	public static void Log(string message)
	{
#if UNITY_EDITOR
		UnityEngine.Debug.Log(message);
#endif
	}

	[Conditional("WDF_TRACE")]
	public static void LogError(string message)
	{
#if UNITY_EDITOR
		UnityEngine.Debug.LogError(message);
#endif
	}
}
