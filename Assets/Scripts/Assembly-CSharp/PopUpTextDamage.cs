using DG.Tweening;
using KobGamesSDKSlim;
using TMPro;
using UnityEngine;

public class PopUpTextDamage : MonoBehaviour
{
	[SerializeField]
	private TextMeshPro m_Text;

	private Sequence m_Sequence;

	private DamagePopUpDatas m_Parameters => GameConfig.Instance.GamePlay.Effects.DamagePopUp;

	private void OnEnable()
	{
		transform.localScale = Vector3.zero;
	}

	public void Initialize(float i_Damage)
	{
		applyOffset();
		m_Text.color = m_Parameters.DamageColor;
		m_Text.SetText(Mathf.RoundToInt(i_Damage).ToString());
		playAnimation();
	}

	private void Move()
	{
		Vector3 position = transform.position;
		transform.DOMove(position + Vector3.up * Random.Range(0.5f, 1f), 0.6f);
	}

	private void playAnimation()
	{
		if (m_Sequence != null)
		{
			m_Sequence.Kill();
		}
		m_Sequence = DOTween.Sequence().SetUpdate(true);
		transform.localScale = Vector3.zero;
		Vector3 position = transform.position + m_Parameters.MoveUp.Vector;
		Tween move = transform.DOLocalMove(position, m_Parameters.MoveUp.Duration)
			.SetEase(m_Parameters.MoveUp.Ease)
			.SetDelay(m_Parameters.MoveUp.Delay);
		m_Sequence.Append(move);
		Tween scaleOut = transform.DOScale(m_Parameters.ScaleOut.Vector, m_Parameters.ScaleOut.Duration)
			.SetEase(m_Parameters.ScaleOut.Ease)
			.SetDelay(m_Parameters.ScaleOut.Delay);
		m_Sequence.Join(scaleOut);
		Tween scaleIn = transform.DOScale(m_Parameters.ScaleIn.Vector, m_Parameters.ScaleIn.Duration)
			.SetEase(m_Parameters.ScaleIn.Ease)
			.SetDelay(m_Parameters.ScaleIn.Delay);
		m_Sequence.Append(scaleIn);
		m_Sequence.OnComplete(() => PoolManager.Instance.Queue(ePoolType.DamagePopUp, gameObject));
	}

	private void applyOffset()
	{
		Vector3 position = transform.position;
		position.z += m_Parameters.MinMaxZOffset.RandomValue();
		position.y += m_Parameters.MinMaxYOffset.RandomValue();
		transform.localPosition = position;
	}

	private void SetRefs()
	{
		m_Text = GetComponentInChildren<TextMeshPro>();
	}
}
