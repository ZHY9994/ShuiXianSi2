using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
	public string SceneName;

	public Slider Slider;

	public float DelayToSwitchScene = 2f;

	private bool m_IsInitStop;

	private bool m_IsSwitchAllowed;

	private void Awake()
	{
		StartCoroutine(LoadScene());
		StartCoroutine(Progress());
	}

	private IEnumerator LoadScene()
	{
		yield return null;
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
		asyncOperation.allowSceneActivation = false;
		while (!asyncOperation.isDone)
		{
			if (asyncOperation.progress < 0.9f)
			{
				yield return null;
				continue;
			}
			if (!m_IsInitStop)
			{
				m_IsInitStop = true;
				StartCoroutine("StopProgress");
			}
			if (!m_IsSwitchAllowed)
			{
				yield return null;
				continue;
			}
			asyncOperation.allowSceneActivation = true;
			yield return null;
		}
	}

	private IEnumerator Progress()
	{
		while (Slider.value < 0.9f)
		{
			Slider.value += 0.01f;
			yield return null;
		}
		yield return null;
	}

	private IEnumerator StopProgress()
	{
		StopCoroutine(Progress());
		yield return new WaitForSeconds(DelayToSwitchScene);
		Slider.value = 1f;
		m_IsSwitchAllowed = true;
	}
}
