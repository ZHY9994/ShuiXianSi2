using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ShopDeck : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_Root;

	[SerializeField]
	private DeckItem[] m_DeckSlots;

	[SerializeField]
	private ExtendedButton m_DeckCloseButton;

	private MenuVariablesEditor.DeckParameter m_Parameetrs => GameConfig.Instance.Menus.DeckParameters;

	public static event Action OnShopDeckPreClose = delegate
	{
	};

	private void OnEnable()
	{
		initializeSlots();
		openAnim();
		DeckManager.OnDeckUpdate += onUpdateDeck;
	}

	private void OnDisable()
	{
		DeckManager.OnDeckUpdate -= onUpdateDeck;
	}

	private void onUpdateDeck()
	{
		initializeSlots();
	}

	private void onClick()
	{
		OnShopDeckPreClose?.Invoke();
		m_DeckCloseButton.SetInteractable(false);
	}

	public void OnButtonClose()
	{
		closeAnim();
	}

	private void updateButton()
	{
		m_DeckCloseButton.gameObject.SetActive(DeckManager.Instance.State == eDeckState.Idle);
		if (DeckManager.Instance.State != eDeckState.Idle)
		{
			return;
		}
		m_DeckCloseButton.Setup(new UnityAction(onClick));
	}

	private void initializeSlots()
	{
		for (int i = 0; i < DeckManager.Instance.Slots.Length; i++)
		{
			m_DeckSlots[i].Initialize(DeckManager.Instance.Slots[i]);
		}
		shakeDeck(DeckManager.Instance.State == eDeckState.Selection);
		updateButton();
	}

	private void shakeDeck(bool i_Shake)
	{
		for (int i = 0; i < m_DeckSlots.Length; i++)
		{
			if (i_Shake)
			{
				m_DeckSlots[i].StartShake();
			}
			else
			{
				m_DeckSlots[i].StopShake();
			}
		}
	}

	private void openAnim()
	{
		if (!m_Parameetrs.IsAnimate || DeckManager.Instance.State == eDeckState.Selection)
		{
			m_Root.anchoredPosition = Vector2.zero;
			return;
		}
		if (DeckManager.Instance.State == eDeckState.Idle)
		{
			m_Root.DOAnchorPos(Vector2.zero, m_Parameetrs.ShowDuration)
				.SetEase(m_Parameetrs.ShowEase)
				.From(Vector2.down * m_Parameetrs.HideDistance);
		}
	}

	private void closeAnim()
	{
		if (!m_Parameetrs.IsAnimate)
		{
			m_Root.anchoredPosition = Vector2.zero;
			DeckManager.Instance.CloseDeck();
			KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.CloseMenuScreen("Screen_Deck");
			return;
		}
		if (!m_Parameetrs.IsAnimate)
		{
			return;
		}
		m_Root.DOAnchorPos(Vector2.down * m_Parameetrs.HideDistance, m_Parameetrs.HideDuration)
			.SetEase(m_Parameetrs.HideEase)
			.OnComplete(() =>
			{
				DeckManager.Instance.CloseDeck();
				KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.CloseMenuScreen("Screen_Deck");
			});
	}

	private void SetRefs()
	{
		m_DeckCloseButton = transform.FindDeepChild<ExtendedButton>("Close Button");
		m_DeckSlots = GetComponentsInChildren<DeckItem>(true);
	}
}
