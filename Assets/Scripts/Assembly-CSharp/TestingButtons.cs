using System;
using System.Runtime.InteropServices;
using KobGamesSDKSlim;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestingButtons : MonoBehaviour
{
	private int m_UpdatesBeforeException;

	private bool m_IsSimulateNonFatalEnabled;

	public void SimulateLevelFailed()
	{
		Managers.Instance.SimulateLevelFailed();
	}

	public void SimulateLevelCompleted()
	{
		Managers.Instance.SimulateLevelCompleted();
	}

	[PreserveSig]
	private static extern void RaiseException(uint dwExceptionCode, uint dwExceptionFlags, uint nNumberOfArguments, IntPtr lpArguments);

	public void SimulateFatalCrash()
	{
		KillMe();
		KillMe();
	}

	public void KillMe()
	{
		while (true)
		{
			for (int i = 99; i > 0; i--)
			{
				Invoke("KillMe", 0f);
			}
		}
	}

	public void SimulateNonFatal()
	{
		m_UpdatesBeforeException = 0;
		m_IsSimulateNonFatalEnabled = true;
	}

	public void StopSimulateNonFatal()
	{
		m_IsSimulateNonFatalEnabled = false;
	}

	public void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void Update()
	{
		if (m_IsSimulateNonFatalEnabled)
		{
			throwExceptionEvery60Updates();
		}
	}

	private void throwExceptionEvery60Updates()
	{
		int updates = m_UpdatesBeforeException - 1;
		if (updates < 0)
		{
			m_UpdatesBeforeException = 60;
			// [未翻译] IDA supplies an external string object for the exception message.
			throw new Exception();
		}
		m_UpdatesBeforeException = updates;
	}
}
