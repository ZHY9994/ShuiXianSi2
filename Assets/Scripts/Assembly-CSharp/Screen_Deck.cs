using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using UnityEngine;
using Wayne.Game.Tutorials;

public class Screen_Deck : MenuScreenBase
{
	[SerializeField]
	private ShopDeck m_Deck;

	[SerializeField]
	private DeckUIFader m_Fader;

	[SerializeField]
	private ExtendedButton m_ExtraCloseButton;

	protected override void OnEnable()
	{
		base.OnEnable();
		if (!TutorialManager.Instance.IsShopTutorialActive)
		{
			m_ExtraCloseButton.Setup(onCloseButtoClick);
		}
		ShopDeck.OnShopDeckPreClose += onCloseButtoClick;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		ShopDeck.OnShopDeckPreClose -= onCloseButtoClick;
	}

	private void onCloseButtoClick()
	{
		m_Deck.OnButtonClose();
		m_Fader.FadeOut();
	}

	protected override void SetRefs()
	{
		base.SetRefs();
		m_Deck = GetComponentInChildren<ShopDeck>();
	}
}
