using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim.UI
{
	public class ToggleButton : MonoBehaviour
	{
		public delegate void ToggleCallback(bool i_State);

		[SerializeField]
		private Button m_Button;

		[SerializeField]
		private TextMeshProUGUI m_Title;

		[SerializeField]
		private ImageToggle m_ToggleImage;

		[SerializeField]
		private Transform m_OffPosition;

		[SerializeField]
		private Transform m_OnPosition;

		private bool m_isOn;

		private Transform m_DummyTransform;

		public event ToggleCallback OnToggle = delegate { };

		protected void SetRefs()
		{
			m_Button = GetComponentInChildren<Button>();
			m_Title = transform.FindDeepChild<TextMeshProUGUI>("Title");
			m_ToggleImage = GetComponentInChildren<ImageToggle>();
			m_OffPosition = transform.FindDeepChild("offPosition");
			m_OnPosition = transform.FindDeepChild("onPosition");
		}

		protected virtual void OnEnable()
		{
			m_Button?.onClick.AddListener(OnButtonDown);
			SetState(m_isOn, false);
		}

		protected virtual void OnDisable()
		{
			m_Button?.onClick.RemoveAllListeners();
		}

		public virtual void Set(string i_Title, bool i_State, ToggleCallback i_Callback)
		{
			if (m_Title == null)
				throw new NullReferenceException();
			m_Title.text = i_Title;
			SetState(i_State, false);
			OnToggle = i_Callback;
		}

		protected virtual void SetState(bool i_SetTrue, bool i_Animate)
		{
			DG.Tweening.DOTween.Kill(this);
			m_DummyTransform = i_SetTrue ? m_OnPosition : m_OffPosition;
			if (i_Animate && m_ToggleImage != null && m_DummyTransform != null)
			{
				m_ToggleImage.transform.DOLocalMove(m_DummyTransform.localPosition, 0.25f).SetEase(DG.Tweening.Ease.OutCirc).SetId(this);
			}
			else if (m_ToggleImage != null && m_DummyTransform != null)
			{
				m_ToggleImage.transform.localPosition = m_DummyTransform.localPosition;
			}
			m_ToggleImage?.Set(i_SetTrue);
			m_isOn = i_SetTrue;
			OnToggle?.Invoke(i_SetTrue);
		}

		protected virtual void OnButtonDown()
		{
			DG.Tweening.DOTween.Kill(this);
			SetState(!m_isOn, true);
		}
	}
}
