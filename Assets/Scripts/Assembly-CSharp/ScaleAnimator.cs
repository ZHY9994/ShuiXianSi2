using DG.Tweening;
using KG.Game.Data;
using UnityEngine;

public class ScaleAnimator : MonoBehaviour
{
	[SerializeField]
	private ScaleAnimationArrayData m_ScaleAnimationArray;

	[SerializeField]
	private bool m_DontUseTimeScale;

	[SerializeField]
	private bool m_ShouldRotate;

	[SerializeField]
	private bool m_StartAtZeroScale;

	[SerializeField]
	private RandomRangeValue m_RandomRotation;

	private Sequence m_AnimationSeqence;

	public void Animate()
	{
		if (m_AnimationSeqence != null)
		{
			m_AnimationSeqence.Kill();
		}
		if (m_ShouldRotate)
		{
			float angle = m_RandomRotation.RandomValue();
			transform.localEulerAngles = new Vector3(0f, 0f, Random.value > 0.5f ? angle : -angle);
		}
		if (m_StartAtZeroScale)
		{
			transform.localScale = Vector3.zero;
		}
		m_AnimationSeqence = m_ScaleAnimationArray.Start(transform, 1f, "debugScale");
		m_AnimationSeqence.SetUpdate(m_DontUseTimeScale);
	}
}
