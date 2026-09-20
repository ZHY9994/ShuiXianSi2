using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

public class OpenDeckButton : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Root;

	[SerializeField]
	private ExtendedButton m_Button;

	private void OnEnable()
	{
		m_Button.Setup(onClick);
		WeaponShopManager.OnShopOpen += onShopOpen;
		WeaponShopManager.OnShopClose += onShopClose;
		DeckManager.OnDeckOpen += onDeckOpen;
		DeckManager.OnDeckClose += onDeckClose;
		m_Root.SetActive(false);
	}

	private void OnDisable()
	{
		WeaponShopManager.OnShopOpen -= onShopOpen;
		WeaponShopManager.OnShopClose -= onShopClose;
		DeckManager.OnDeckOpen -= onDeckOpen;
		DeckManager.OnDeckClose -= onDeckClose;
	}

	private void onShopOpen()
	{
		visibilty(true);
	}

	private void onShopClose()
	{
		visibilty(false);
	}

	private void onDeckOpen()
	{
		visibilty(false);
	}

	private void onDeckClose()
	{
		visibilty(true);
	}

	private void onClick()
	{
		DeckManager.Instance.OpenDeck(eDeckState.Idle);
	}

	private void visibilty(bool i_Visilbity)
	{
		m_Root.SetActive(i_Visilbity);
		if (i_Visilbity) m_Button.Setup(onClick);
	}

	private void SetRefs()
	{
		m_Root = transform.FindDeepChild<GameObject>("Root");
		m_Button = GetComponentInChildren<ExtendedButton>(true);
	}
}
