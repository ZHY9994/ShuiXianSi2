using DG.Tweening;
using UnityEngine;

public class FadeInOnEnable : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	[SerializeField]
	private float m_FadeInDelay;

	[SerializeField]
	private float m_FadeInDuration;

	[SerializeField]
	private Ease m_FadeInEase;

	private Tween m_FadeTween;

	private void Awake()
	{
		m_CanvasGroup.alpha = 0f;
	}

	private void OnEnable()
	{
		m_CanvasGroup.alpha = 0f;
		m_FadeTween?.Kill();
		m_FadeTween = m_CanvasGroup.DOFade(1f, m_FadeInDuration).SetDelay(m_FadeInDelay).SetEase(m_FadeInEase).From(0f);
	}

	private void OnValidate()
	{
		m_CanvasGroup = GetComponentInChildren<CanvasGroup>();
	}
}
