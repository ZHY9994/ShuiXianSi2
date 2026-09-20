using DG.Tweening;
using KG.Game.Data;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
	[SerializeField]
	private ScaleAnimationArrayData m_ScaleInAniamtion;

	[SerializeField]
	private ScaleAnimationArrayData m_ScaleOutAnimation;

	private Sequence m_CurrentSequence;

	public void StartScale()
	{
		m_CurrentSequence = m_ScaleInAniamtion.Start(transform, 1f, "debugScale");
	}

	public void StartScaleOut()
	{
		m_CurrentSequence = m_ScaleOutAnimation.Start(transform, 1f, "debugScale");
	}

	public void KillSequence()
	{
		m_CurrentSequence?.Kill();
	}
}
