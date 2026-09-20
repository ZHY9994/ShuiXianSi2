using UnityEngine;
using Wayne.Game.Shops;
using Wayne.Game.Shops.Weapons;

public class CardItem : MonoBehaviour
{
	[SerializeField]
	private WeaponRarityItemHandler m_RarityController;

	public WeaponRarityItemHandler RarityController => m_RarityController;

	public WeaponItemData Item { get; private set; }

	public void Initialize(WeaponItemData i_Item)
	{
		if (i_Item != null)
		{
			Item = i_Item;
			m_RarityController.Initialize(i_Item);
		}
	}

	public void UpdateCard()
	{
		m_RarityController.UpdateController();
	}
}
