using System;
using DG.Tweening;
using KobGamesSDKSlim;
using UnityEngine;

public class TabExpander : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_Self;

	private Sequence m_Sequence;

	private MenuVariablesEditor.DeckParameter m_Paramater => GameConfig.Instance.Menus.TabExpanderParameter;

	public void ExpandTab(Action i_Callback)
	{
		m_Sequence?.Kill();
		m_Sequence = DOTween.Sequence();
		if (m_Paramater.IsAnimate)
		{
			m_Sequence.Append(m_Self.DOSizeDelta(new Vector2(0f, m_Paramater.HideDistance), m_Paramater.HideDuration).SetEase(m_Paramater.HideEase).OnComplete(() => i_Callback?.Invoke()));
			m_Sequence.Append(m_Self.DOSizeDelta(Vector2.zero, m_Paramater.ShowDuration).SetEase(m_Paramater.ShowEase).OnComplete(Reset));
		}
	}

	public void Reset()
	{
		m_Self.sizeDelta = Vector2.zero;
	}

	private void SetRefs()
	{
		m_Self = GetComponent<RectTransform>();
	}
}
