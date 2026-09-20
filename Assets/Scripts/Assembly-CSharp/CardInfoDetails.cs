using TMPro;
using UnityEngine;
using KobGamesSDKSlim;
using Wayne.Game.Shops;

public class CardInfoDetails : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_CardCountText;

	[SerializeField]
	private TextMeshProUGUI m_MoneyText;

	public void UpdateDetails(WeaponItemData i_ItemData)
	{
		if (i_ItemData == null)
		{
			gameObject.SetActive(false);
			return;
		}
		var upgradeData = i_ItemData.UpgradeData;
		if (upgradeData == null)
		{
			if (m_CardCountText != null) m_CardCountText.gameObject.SetActive(false);
			if (m_MoneyText != null) m_MoneyText.gameObject.SetActive(false);
			return;
		}
		string upgradeColor = WayneStaticFunctions.ColorToHex(i_ItemData.IsUpgradeable ? Color.green : Color.red);
		if (m_CardCountText != null)
		{
			m_CardCountText.gameObject.SetActive(!i_ItemData.IsMaxed);
			m_CardCountText.text = string.Format("<color={0}>{1}</color>", upgradeColor, i_ItemData.CardAmount) + "/" + upgradeData.RequiredNumberOfCards;
		}
		if (m_MoneyText != null && StorageManager.Instance != null)
		{
			m_MoneyText.gameObject.SetActive(!i_ItemData.IsMaxed);
			int coins = StorageManager.Instance.CoinsAmount;
			string moneyColor = WayneStaticFunctions.ColorToHex(coins >= upgradeData.Price ? Color.green : Color.red);
			m_MoneyText.text = string.Format("<color={0}>{1}</color>", moneyColor, coins) + "/" + upgradeData.Price;
		}
	}

	public void Visibility(bool i_Visibility)
	{
		gameObject.SetActive(i_Visibility);
	}
}
