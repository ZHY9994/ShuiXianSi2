using DG.Tweening;
using KobGamesSDKSlim;
using UnityEngine;

public class UIShake : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_Self;

	[SerializeField]
	private Vector3 m_AnchoredPosition;

	private Sequence m_Sequence;

	private BoosterRevealAnimData m_AnimData => GameConfig.Instance.GamePlay.BoosterData.RevealAnimData;

	private void OnEnable()
	{
		m_Self.anchoredPosition3D = Vector3.zero;
		transform.rotation = Quaternion.identity;
		Screen_BoosterReward.OnBoosterOpen += onBoosterOpen;
	}

	private void OnDisable()
	{
		Screen_BoosterReward.OnBoosterOpen -= onBoosterOpen;
	}

	private void onBoosterOpen()
	{
		shake();
	}

	private void shake()
	{
		if (m_Sequence != null)
		{
			m_Sequence.Kill();
		}
		m_Sequence = DOTween.Sequence();
		Vector3 strength = m_AnimData.ShakeStrength;
		m_Sequence.Append(m_Self.DOShakePosition(m_AnimData.ShakeDuration, strength, m_AnimData.Vibrator, 90f, false, true, ShakeRandomnessMode.Full));
		m_Sequence.Join(m_Self.DOShakeRotation(m_AnimData.ShakeDuration, strength, m_AnimData.Vibrator, 90f, true, ShakeRandomnessMode.Full));
	}

	private void SetRefs()
	{
		m_Self = GetComponent<RectTransform>();
		m_AnchoredPosition = m_Self.localPosition;
	}
}
