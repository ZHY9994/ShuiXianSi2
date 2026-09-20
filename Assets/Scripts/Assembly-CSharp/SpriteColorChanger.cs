using KobGamesSDKSlim;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpriteColorChanger : AnimatorBase
{
	[SerializeField]
	private SpriteRenderer m_SpriteRenderer;

	[SerializeField]
	private Image m_Image;

	[SerializeField]
	private Gradient m_Gradient;

	private void SetReferences()
	{
		m_SpriteRenderer = GetComponent<SpriteRenderer>();
		m_Image = GetComponent<Image>();
	}

	public override void StartAnimation()
	{
		base.StartAnimation();
		ResetValues();
		Tween tween = m_SpriteRenderer != null
			? m_SpriteRenderer.DOGradientColor(m_Gradient, Duration)
			: (m_Image != null ? m_Image.DOGradientColor(m_Gradient, Duration) : null);
		if (tween == null) return;
		Tween = tween.SetEase(Ease).SetRelative(IsRelative).SetLoops(LoopCount, LoopType).SetUpdate(UpdateType, UpdateFrameIndependant);
		SetId(Tween);
		if (StartTime > 0f && StartTime < 1f)
		{
			Tween.Goto(Mathf.Clamp01(StartTime) * Duration, true);
		}
	}

	public override void ResetValues()
	{
		base.ResetValues();
		if (m_SpriteRenderer != null)
		{
			m_SpriteRenderer.color = m_Gradient.Evaluate(0f);
			return;
		}
		if (m_Image != null)
		{
			m_Image.color = m_Gradient.Evaluate(0f);
		}
	}
}
