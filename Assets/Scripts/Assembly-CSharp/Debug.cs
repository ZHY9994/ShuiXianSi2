using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Internal;

public static class Debugs
{
	public static bool isDebugBuild => UnityEngine.Debug.isDebugBuild;

	public static ILogger unityLogger => UnityEngine.Debug.unityLogger;

	[Conditional("DUMMY")]
	public static void Assert(bool condition, string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void Assert(bool condition, string message, UnityEngine.Object context)
	{
	}

	[Conditional("DUMMY")]
	public static void Assert(bool condition)
	{
	}

	[Conditional("DUMMY")]
	public static void Assert(bool condition, object message, UnityEngine.Object context)
	{
	}

	[Conditional("DUMMY")]
	public static void Assert(bool condition, string message)
	{
	}

	[Conditional("DUMMY")]
	public static void Assert(bool condition, object message)
	{
	}

	[Conditional("DUMMY")]
	public static void Assert(bool condition, UnityEngine.Object context)
	{
	}

	[Conditional("DUMMY")]
	public static void AssertFormat(bool condition, UnityEngine.Object context, string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void AssertFormat(bool condition, string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void Break()
	{
	}

	[Conditional("DUMMY")]
	public static void ClearDeveloperConsole()
	{
	}

	[Conditional("DUMMY")]
	public static void DebugBreak()
	{
	}

	[Conditional("DUMMY")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
	{
	}

	[Conditional("DUMMY")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color)
	{
	}

	[Conditional("DUMMY")]
	public static void DrawLine(Vector3 start, Vector3 end)
	{
	}

	[Conditional("DUMMY")]
	public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
	{
	}

	[Conditional("DUMMY")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
	{
	}

	[Conditional("DUMMY")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color)
	{
	}

	[Conditional("DUMMY")]
	public static void DrawRay(Vector3 start, Vector3 dir, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
	{
	}

	[Conditional("DUMMY")]
	public static void DrawRay(Vector3 start, Vector3 dir)
	{
	}

	[Conditional("DUMMY")]
	public static void Log(object message)
	{
	}

	[Conditional("DUMMY")]
	public static void Log(object message, UnityEngine.Object context)
	{
	}

	[Conditional("DUMMY")]
	public static void LogAssertion(object message, UnityEngine.Object context)
	{
	}

	[Conditional("DUMMY")]
	public static void LogAssertion(object message)
	{
	}

	[Conditional("DUMMY")]
	public static void LogAssertionFormat(UnityEngine.Object context, string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void LogAssertionFormat(string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void LogError(object message, UnityEngine.Object context)
	{
	}

	[Conditional("DUMMY")]
	public static void LogError(object message)
	{
	}

	[Conditional("DUMMY")]
	public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void LogErrorFormat(string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void LogException(Exception exception)
	{
	}

	[Conditional("DUMMY")]
	public static void LogException(Exception exception, UnityEngine.Object context)
	{
	}

	[Conditional("DUMMY")]
	public static void LogFormat(string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void LogFormat(LogType logType, LogOption logOptions, UnityEngine.Object context, string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void LogWarning(object message, UnityEngine.Object context)
	{
	}

	[Conditional("DUMMY")]
	public static void LogWarning(object message)
	{
	}

	[Conditional("DUMMY")]
	public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void LogWarningFormat(string format, params object[] args)
	{
	}

	[Conditional("DUMMY")]
	public static void LogInfo(string message, UnityEngine.Object obj = null)
	{
	}

	[Conditional("DUMMY")]
	public static void LogWarning(string message, UnityEngine.Object obj = null)
	{
	}
}
