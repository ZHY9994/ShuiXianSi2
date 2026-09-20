using DG.Tweening;
using UnityEngine;

public class ScaleOnEnable : MonoBehaviour
{
	[SerializeField]
	private float m_ScaleDelay;

	[SerializeField]
	private float m_ScaleDuration;

	[SerializeField]
	private Ease m_ScaleEase;

	private Vector3 m_OriginalScale;

	private Tween m_ScaleTween;

	private void Awake()
	{
		m_OriginalScale = transform.localScale;
	}

	private void OnEnable()
	{
		m_ScaleTween?.Kill();
		m_ScaleTween = transform.DOScale(m_OriginalScale, m_ScaleDuration)
			.SetDelay(m_ScaleDelay)
			.SetEase(m_ScaleEase)
			.From(0f);
	}
}
