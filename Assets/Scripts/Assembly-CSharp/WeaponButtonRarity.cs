using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wayne.Game.Shops;

public class WeaponButtonRarity : MonoBehaviour
{
	public eItemRarity Rarity;

	[SerializeField]
	private GameObject m_Root;

	[SerializeField]
	private Image m_Icon;

	[SerializeField]
	private TextMeshProUGUI m_LevelTxt;

	public WeaponItemData ItemData;

	public void InitializeRarity(WeaponItemData i_Item)
	{
		ItemData = i_Item;
		if (ItemData == null || ItemData.ItemRarity != Rarity)
		{
			hide();
			return;
		}
		show();
		m_Icon.sprite = i_Item.ShopSprite;
		m_Icon.rectTransform.sizeDelta = WeaponShopManager.Instance.ShopData.PreviewSize;
		updateLevelTxt();
	}

	private void updateLevelTxt()
	{
		string levelWord = LocalizationUtility.GetWordLocalizedString("lvl").ToLower();
		m_LevelTxt.fontSizeMin = LocalizationUtility.GetLocalType() == eLocaleType.EN ? 32.8f : 10f;
		m_LevelTxt.SetText(string.Format("{0} {1}", levelWord, ItemData.Level));
	}

	private void hide()
	{
		gameObject.SetActive(false);
	}

	private void show()
	{
		gameObject.SetActive(true);
	}

	private void SetRefs()
	{
		m_Root = transform.FindDeepChild<GameObject>("Root");
		m_Icon = transform.FindDeepChild<Image>("Icon");
		m_LevelTxt = transform.FindDeepChild<TextMeshProUGUI>("Level Text");
	}
}
