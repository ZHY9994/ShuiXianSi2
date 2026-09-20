using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
	[SerializeField]
	private Image m_Image;

	public float FadeInDUration;

	public float FadeDelay;

	public float FadeOutDuration;

	private Sequence m_LevelCompleteSequence;

	public Image FadeImage => m_Image;

	private void SetRefs()
	{
		m_Image = GetComponentInChildren<Image>();
	}
}
