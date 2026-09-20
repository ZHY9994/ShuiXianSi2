using DG.Tweening;
using KobGamesSDKSlim.Shop;
using KobGamesSDKSlim;
using UnityEngine;
using Wayne.Game.Shops;

public class BoosterSlot : MonoBehaviour
{
	[SerializeField]
	private Transform m_Root;

	[SerializeField]
	private BoosterRarityController m_RarityController;

	private Tween m_ScaleTween;

	public ShopSettings m_ShopSettings => GameConfig.Instance.Shop.ShopSettings;

	public bool IsSlotSet { get; private set; }

	private void OnEnable()
	{
		IsSlotSet = false;
	}

	public void Initialize(WeaponItemData i_Item, int i_AddedCount, int i_Index)
	{
		if (i_Item != null)
		{
			m_RarityController.Initialize(i_Item, i_AddedCount);
			ScaleAnim(i_Index);
			i_Item.AddCard(i_AddedCount);
		}
	}

	public void SetSlot()
	{
		IsSlotSet = true;
	}

	private void ScaleAnim(int i_Index)
	{
		m_ScaleTween?.Kill();
		m_Root.localScale = Vector3.zero;
		m_ScaleTween = m_Root.DOScale(1f, m_ShopSettings.Appear.AppearDuration)
			.From(0f)
			.SetEase(m_ShopSettings.Appear.AppearEase)
			.SetDelay(m_ShopSettings.Appear.AppearDelay.Evaluate(i_Index));
	}
}
