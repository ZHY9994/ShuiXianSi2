using UnityEngine;

public class ShopItemLevelContainer : ShopItemContainer
{
	[SerializeField]
	private GameObject m_DefaultCardLevelIcon;

	[SerializeField]
	private GameObject m_UpgradeableCardLevelIcon;

	public void SetLevelIcon(bool i_IsUpgradeable)
	{
		m_DefaultCardLevelIcon.SetActive(!i_IsUpgradeable);
		m_UpgradeableCardLevelIcon.SetActive(i_IsUpgradeable);
	}
}
