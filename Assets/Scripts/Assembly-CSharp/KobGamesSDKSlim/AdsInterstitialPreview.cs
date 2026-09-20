using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class AdsInterstitialPreview : MonoBehaviour
	{
		public bool IsEnabled = true;

		public int StepCount = 3;

		public float DelayPerStep = 0.5f;

		public GameObject Root;

		public TMP_Text CountText;

		private Action m_ShowAdCallback;

		private Action m_CloseCallback;

		private float m_DelayNow;

		public void Show()
		{
			Show(null, null);
		}

		public void Show(Action i_ShowAdCallback, Action i_CloseCallback = null)
		{
			if (!IsEnabled)
			{
				i_ShowAdCallback?.Invoke();
				i_CloseCallback?.Invoke();
				return;
			}
			m_ShowAdCallback = i_ShowAdCallback;
			m_CloseCallback = i_CloseCallback;
			if (CountText != null) CountText.text = StepCount.ToString();
			if (Root == null) return;
			Root.SetActive(true);
			StopAllCoroutines();
			StartCoroutine(startStepsCountDown());
		}

		public void Close()
		{
			m_CloseCallback?.Invoke();
			m_CloseCallback = null;
			if (Root != null) Root.SetActive(false);
			StopAllCoroutines();
		}

		private IEnumerator startStepsCountDown()
		{
			for (int i = StepCount; ; i--)
			{
				if (i < 0)
				{
					Close();
					yield break;
				}
				CountText.text = i.ToString();
				m_DelayNow = DelayPerStep;
				while (m_DelayNow > 0f)
				{
					yield return null;
					m_DelayNow -= Time.unscaledDeltaTime;
				}
				if (i == 1)
				{
					m_ShowAdCallback?.Invoke();
					m_ShowAdCallback = null;
				}
			}
		}

		public void SetRefs()
		{
			Transform root = transform.FindDeepChild("Root");
			Root = root != null ? root.gameObject : null;
			Transform count = transform.FindDeepChild("Count");
			CountText = count != null ? count.GetComponent<TMP_Text>() : null;
		}
	}
}
