using UnityEngine;

public class OfferPanelManager : MonoBehaviour
{
	[SerializeField]
	private OfferPanel[] m_OfferPanels;

	private eOfferType m_OfferType => SpecialOfferManager.Instance.OfferType;

	private void OnEnable()
	{
		OpenOfferPanel();
	}

	private void OpenOfferPanel()
	{
		DisableAllPanels();
		foreach (OfferPanel panel in m_OfferPanels)
			if (panel.OfferType == m_OfferType) { panel.PanelVisibility(true); break; }
	}

	private void DisableAllPanels()
	{
		foreach (OfferPanel panel in m_OfferPanels)
			panel.PanelVisibility(false);
	}

	private void SetRefs()
	{
		m_OfferPanels = GetComponentsInChildren<OfferPanel>();
	}
}
