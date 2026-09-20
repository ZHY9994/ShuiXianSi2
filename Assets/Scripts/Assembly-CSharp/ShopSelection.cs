using DG.Tweening;
using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

public class ShopSelection : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_Self;

	[SerializeField]
	private Vector2 m_OriginalSizeDelta;

	[SerializeField]
	private GameObject m_Root;

	[SerializeField]
	private SelectionItem m_SelectionItem;

	private Tween m_Tween;

	private MenuVariablesEditor.ShopDeckParameters m_Parameters => GameConfig.Instance.Menus.ShopDeckParameter;

	private void OnEnable()
	{
		updateSelection();
		DeckManager.OnDeckUpdate += onDeckUpdate;
	}

	private void OnDisable()
	{
		DeckManager.OnDeckUpdate -= onDeckUpdate;
		if (m_Tween != null)
		{
			m_Tween.Kill();
		}
	}

	private void onDeckUpdate()
	{
		updateSelection();
	}

	private void updateSelection()
	{
		playAnim();
		if (DeckManager.Instance == null || m_Root == null)
		{
			return;
		}
		m_Root.SetActive(DeckManager.Instance.State == eDeckState.Selection);
		if (WeaponShopManager.Instance == null || m_SelectionItem == null)
		{
			return;
		}
		m_SelectionItem.InitializeSelection(WeaponShopManager.Instance.GetSelectedItem());
	}

	private void playAnim()
	{
		if (m_Tween != null)
		{
			m_Tween.Kill();
		}
		if (m_Parameters == null)
		{
			return;
		}
		if (!m_Parameters.IsAnimateSelection)
		{
			if (m_Self != null)
			{
				m_Self.sizeDelta = m_OriginalSizeDelta;
			}
		}
		if (m_Parameters.IsAnimateSelection && DeckManager.Instance != null && DeckManager.Instance.State == eDeckState.Selection && m_Self != null)
		{
			m_Self.sizeDelta = m_OriginalSizeDelta;
		}
		if (!m_Parameters.IsAnimateSelection || DeckManager.Instance == null || DeckManager.Instance.State != eDeckState.Idle)
		{
			return;
		}
		if (m_Self != null)
		{
			m_Tween = m_Self.DOSizeDelta(new Vector2(m_OriginalSizeDelta.x, 0f), m_Parameters.SelectionCloseDuration)
				.SetEase(m_Parameters.SelectionCloseEase);
		}
	}

	private void SetRefs()
	{
		m_SelectionItem = GetComponentInChildren<SelectionItem>();
		m_Root = transform.FindDeepChild<GameObject>("Root");
	}
}
