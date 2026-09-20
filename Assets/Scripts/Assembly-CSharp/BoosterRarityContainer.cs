using KobGamesSDKSlim.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wayne.Game.Helpers;

public class BoosterRarityContainer : MonoBehaviour
{
	[SerializeField]
	private GameObject[] m_BGs;

	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private TextMeshProUGUI m_CountText;

	public void Initialize(TItemData i_Item, int i_Count)
	{
		hideBGs();
		m_BGs[i_Count > 1 ? 1 : 0].SetActive(true);
		m_Icon.sprite = i_Item.ShopSprite;
		m_CountText.text = "x" + i_Count;
	}

	private void hideBGs()
	{
		foreach (GameObject bg in m_BGs)
			bg.SetActive(false);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	private void SetRefs()
	{
		Transform bgs = transform.FindDeepChild<Transform>("BGs");
		Image[] images = bgs.GetComponentsInChildren<Image>();
		m_BGs = new GameObject[images.Length];
		for (int i = 0; i < images.Length; i++)
			m_BGs[i] = images[i].gameObject;
		m_Icon = transform.FindDeepChild<Image>("Icon");
		m_CountText = transform.FindDeepChild<TextMeshProUGUI>("Card Count");
	}
}
