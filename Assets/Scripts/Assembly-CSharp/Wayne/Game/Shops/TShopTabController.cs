using System.Collections.Generic;
using KobGamesSDKSlim.Shop;
using UnityEngine;

namespace Wayne.Game.Shops
{
	public class TShopTabController : MonoBehaviour
	{
		[SerializeField]
		protected TShopTab[] m_AllTabs;

		protected Dictionary<eItemRarity, TShopTab> m_Tabs = new Dictionary<eItemRarity, TShopTab>();

		public TShopTab ActiveTab { get; private set; }

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void onShopOpen()
		{
			initialize(true);
		}

		protected virtual void onUpdateShop()
		{
			initialize(false);
		}

		protected virtual void onReInitializeShop()
		{
			initialize(true);
		}

		protected virtual void initialize(bool i_InvokeEvent = false)
		{
		}

		protected void clearTabs()
		{
			if (m_AllTabs != null)
			{
				for (int i = 0; i < m_AllTabs.Length; i++)
				{
					if (m_AllTabs[i] != null)
						m_AllTabs[i].gameObject.SetActive(false);
				}
			}

			if (m_Tabs != null)
				m_Tabs.Clear();
			ActiveTab = null;
		}

		protected void selectTab(TShopTab i_Tab)
		{
			if (ActiveTab == null || !ActiveTab.Equals(i_Tab))
				ActiveTab = i_Tab;
		}

		protected virtual void SetRefs()
		{
			m_AllTabs = GetComponentsInChildren<TShopTab>();
		}
	}
}
