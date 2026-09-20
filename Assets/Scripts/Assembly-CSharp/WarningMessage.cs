using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WarningMessage : MonoBehaviour
{
	[SerializeField]
	private float ShowFadeDuration;

	[SerializeField]
	private float MoveOffset;

	[SerializeField]
	private float MoveDuration;

	[SerializeField]
	private Color m_BackInitialColor;

	[SerializeField]
	private Image m_BackImage;

	[SerializeField]
	private TextMeshProUGUI m_WarningText;

	[SerializeField]
	private RectTransform m_Base;

	private Tween m_Tween1;

	private Tween m_Tween2;

	private Tween m_Tween3;

	private Tween m_Tween4;

	private Tween m_Tween5;

	private void OnEnable()
	{
		m_Base.gameObject.SetActive(false);
	}

	public void SetText(string i_Text)
	{
		m_WarningText.SetText(i_Text);
	}

	public void Show()
	{
		m_Base.gameObject.SetActive(true);
		m_Base.anchoredPosition = Vector2.zero;
		m_Base.transform.localPosition = Vector3.zero;
		m_Tween1?.Kill();
		m_Tween2?.Kill();
		m_Tween3?.Kill();
		m_Tween4?.Kill();
		m_Tween5?.Kill();

		m_Tween1 = m_BackImage.DOFade(m_BackInitialColor.a, ShowFadeDuration).From(0f);
		m_Tween2 = m_WarningText.DOFade(1f, ShowFadeDuration).From(0f);
		m_Tween3 = m_Base.DOAnchorPos(Vector2.up * MoveOffset, MoveDuration).SetRelative();
		m_Tween4 = m_BackImage.DOFade(0f, ShowFadeDuration).SetDelay(MoveDuration - ShowFadeDuration);
		m_Tween5 = m_WarningText.DOFade(0f, ShowFadeDuration)
			.SetDelay(MoveDuration - ShowFadeDuration)
			.OnComplete(() => m_Base.gameObject.SetActive(false));
	}
}
