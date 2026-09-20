using DG.Tweening;
using KobGamesSDKSlim;
using UnityEngine;
using UnityEngine.UI;

public class DeckUIFader : MonoBehaviour
{
	[SerializeField]
	private Image m_Image;

	private Tween m_Tween;

	private MenuVariablesEditor.ShopDeckParameters m_Parameters => GameConfig.Instance.Menus.ShopDeckParameter;

	private void OnEnable()
	{
		fadeIn();
	}

	private void OnDisable()
	{
		m_Tween?.Kill();
	}

	private void fadeIn()
	{
		if (m_Image == null)
		{
			return;
		}
		m_Tween?.Kill();
		m_Tween = m_Image.DOFade(m_Parameters.FadeInEndValue, m_Parameters.FadeInDuration)
			.SetEase(m_Parameters.FadeInEase)
			.From(m_Parameters.FadeInStartValue);
	}

	public void FadeOut()
	{
		if (m_Image == null)
		{
			return;
		}
		m_Tween?.Kill();
		m_Tween = m_Image.DOFade(0f, m_Parameters.FadeOutDuration).SetEase(m_Parameters.FadeOutEase);
	}
}
