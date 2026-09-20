using KobGamesSDKSlim.Shop;
using UnityEngine;

namespace Wayne.Game.Shops
{
	public class TTabButtonController : MonoBehaviour
	{
		[SerializeField]
		private GameObject m_Root;

		[SerializeField]
		protected TTabButton[] m_TabButtons;

		[SerializeField]
		private GameObject[] m_TabTops;

		public TTabButtonController()
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void onTabSelected(eItemRarity i_Rarity)
		{
			hideAllTops();
			if (m_TabTops == null || (int)i_Rarity >= m_TabTops.Length)
				return;
			m_TabTops[(int)i_Rarity].SetActive(true);
		}

		protected virtual void onShopOpen()
		{
			m_Root.SetActive(true);
			hideAllTops();
			hideAllButtons();
			m_TabTops[0].SetActive(true);
		}

		private void hideAllTops()
		{
			if (m_TabTops == null)
				return;
			for (int i = 0; i < m_TabTops.Length; i++)
			{
				if (m_TabTops[i] != null)
					m_TabTops[i].SetActive(false);
			}
		}

		private void hideAllButtons()
		{
			if (m_TabButtons == null)
				return;
			for (int i = 0; i < m_TabButtons.Length; i++)
			{
				if (m_TabButtons[i] != null)
					m_TabButtons[i].Hide();
			}
		}
	}
}
