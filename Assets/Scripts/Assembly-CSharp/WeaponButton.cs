using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

public class WeaponButton : MonoBehaviour
{
	[SerializeField]
	private ExtendedButton m_Button;

	[SerializeField]
	private WeaponSelectionManager m_Manager;

	[SerializeField]
	private GameObject m_Blocker;

	[SerializeField]
	private GameObject m_HandAnim;

	[SerializeField]
	private WeaponButtonRarity[] m_Rarities;

	private WeaponItemData m_ItemData;

	public int Index;

	public event Action<WeaponItemData, int> OnWeaponSelected = delegate { };

	private void OnEnable()
	{
		m_HandAnim.SetActive(false);
		m_Blocker.SetActive(false);
		m_Button.Setup(onClick);
		tutorial();
	}

	private void onClick()
	{
		SoundManager.Instance.PlayGeneralSFXByType(eSFXType.WeaponSelect);
		if (m_ItemData.IsItemNew)
			m_ItemData.ItemUsed();
		OnWeaponSelected?.Invoke(m_ItemData, Index);
	}

	public void UpdateButton(WeaponItemData i_ItemData, int i_Index)
	{
		m_ItemData = i_ItemData;
		Index = i_Index;
		updateRarity(m_ItemData);
		tutorial();
	}

	private void tutorial()
	{
		if (!StorageManager.Instance.WeaponSelectionTutorial || !StorageManager.Instance.IsFirstLaunch || StorageManager.Instance.CurrentLevel > 1)
		{
			m_Blocker.SetActive(false);
			m_HandAnim.SetActive(false);
			return;
		}
		m_Blocker.SetActive(true);
		m_HandAnim.SetActive(false);
		if (m_ItemData == null)
			return;
		if ((int)m_ItemData.WeaponType == 1)
		{
			m_Blocker.SetActive(m_Manager.ThrownCount < 1);
			m_HandAnim.SetActive(m_Manager.ThrownCount == 1);
		}
		else if ((int)m_ItemData.WeaponType == 0)
		{
			m_Blocker.SetActive(m_Manager.ThrownCount > 0);
			m_HandAnim.SetActive(m_Manager.ThrownCount == 0);
		}
	}

	private void updateRarity(WeaponItemData i_Item)
	{
		foreach (WeaponButtonRarity rarity in m_Rarities)
			rarity.InitializeRarity(i_Item);
	}

	private void SetRef()
	{
		m_Button = GetComponentInChildren<ExtendedButton>();
		m_Manager = GetComponentInParent<WeaponSelectionManager>();
		m_Rarities = GetComponentsInChildren<WeaponButtonRarity>(true);
		m_Blocker = transform.FindDeepChild<GameObject>("Blocker");
		m_HandAnim = transform.FindDeepChild<GameObject>("Hand Anim");
	}
}
