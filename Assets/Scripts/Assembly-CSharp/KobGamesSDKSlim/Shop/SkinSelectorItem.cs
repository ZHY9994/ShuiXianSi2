using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	public class SkinSelectorItem : MonoBehaviour
	{
		[SerializeField]
		private string m_ItemName;

		[SerializeField]
		private GameObject[] m_Extras;

		public string Name => m_ItemName;

		private List<string> Names => KobGamesSDKSlim.Singleton<Wayne.Game.Shops.SkinShopManager>.Instance.GetItemNames();

		public bool IsItemToActivate(string i_NameToActivate)
		{
			return string.Equals(m_ItemName, i_NameToActivate);
		}

		public virtual void Activate()
		{
			gameObject.SetActive(true);
			if (m_Extras != null)
				foreach (GameObject extra in m_Extras)
					if (extra != null) extra.SetActive(true);
		}

		public virtual void Deactivate()
		{
			gameObject.SetActive(false);
			if (m_Extras != null)
				foreach (GameObject extra in m_Extras)
					if (extra != null) extra.SetActive(false);
		}

		protected void onValueChanged()
		{
			SkinSelector selector = GetComponentInParent<SkinSelector>();
			if (selector != null)
				selector.SendMessage("selectItem", this);
		}
	}
}
