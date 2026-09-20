using DG.Tweening;
using KobGamesSDKSlim;
using UnityEngine;

public class HeadShotEffect : MonoBehaviour
{
	private Sequence m_Sequence;

	private EffectsDataEditor.HeadShotParameters m_Parameters => GameConfig.Instance.GamePlay.Effects.HeadShot;

	private void OnEnable()
	{
		playAnimation();
	}

	private void OnDisable()
	{
		m_Sequence?.Kill();
	}

	private void playAnimation()
	{
		m_Sequence?.Kill();
		m_Sequence = DOTween.Sequence().SetUpdate(true);
		Vector3 angles = Vector3.zero;
		angles.x = Random.Range(m_Parameters.RotationMinMax.x, m_Parameters.RotationMinMax.y);
		if (Random.value <= 0.5f)
			angles.x = -angles.x;
		transform.localEulerAngles = angles;
		transform.localScale = Vector3.zero;
		m_Sequence.Append(transform.DOLocalMove(m_Parameters.MoveUp.Vector, m_Parameters.MoveUp.Duration).SetRelative(true).SetEase(m_Parameters.MoveUp.Ease).SetDelay(m_Parameters.MoveUp.Delay));
		m_Sequence.Join(transform.DOScale(m_Parameters.ScaleOut.Vector, m_Parameters.ScaleOut.Duration).SetEase(m_Parameters.ScaleOut.Ease).SetDelay(m_Parameters.ScaleOut.Delay));
		m_Sequence.Append(transform.DOScale(m_Parameters.ScaleScaleIn.Vector, m_Parameters.ScaleScaleIn.Duration).SetEase(m_Parameters.ScaleScaleIn.Ease).SetDelay(m_Parameters.ScaleScaleIn.Delay));
		m_Sequence.OnComplete(() => PoolManager.Instance.Queue(ePoolType.HeadShotVFX, gameObject));
	}
}
