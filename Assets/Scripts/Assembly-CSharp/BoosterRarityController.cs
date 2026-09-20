using KobGamesSDKSlim.Shop;
using UnityEngine;

public class BoosterRarityController : MonoBehaviour
{
	[SerializeField]
	private BoosterRarityContainer[] m_RarityContainers;

	public void Initialize(TItemData i_Item, int i_Count)
	{
		hideAllContainers();
		if (i_Item == null)
			return;
		BoosterRarityContainer container = m_RarityContainers[(int)i_Item.ItemRarity];
		container.Show();
		container.Initialize(i_Item, i_Count);
	}

	private void hideAllContainers()
	{
		foreach (BoosterRarityContainer container in m_RarityContainers)
			container.Hide();
	}

	private void SetRefs()
	{
		m_RarityContainers = GetComponentsInChildren<BoosterRarityContainer>(true);
	}
}
