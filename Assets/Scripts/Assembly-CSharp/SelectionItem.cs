using DG.Tweening;
using KobGamesSDKSlim.Shop;
using KobGamesSDKSlim;
using UnityEngine;
using UnityEngine.UI;
using Wayne.Game.Shops;
using Wayne.Game.Shops.Weapons;

public class SelectionItem : MonoBehaviour
{
	[SerializeField]
	private WeaponRarityItemHandler m_RarityController;

	[SerializeField]
	private Button m_Button;

	public ShopSettings m_ShopSettings => GameConfig.Instance.Shop.ShopSettings;

	public TItemData Item { get; private set; }

	private void OnEnable()
	{
		m_Button.onClick.AddListener(onButtonClick);
	}

	private void OnDisable()
	{
		m_Button.onClick.RemoveAllListeners();
	}

	private void onButtonClick()
	{
		playSelectAnim();
	}

	public void InitializeSelection(WeaponItemData i_ItemData)
	{
		Item = i_ItemData;
		m_RarityController.Initialize(i_ItemData);
	}

	private void playSelectAnim()
	{
		ShopSettings.PunchTweenParameters punch = m_ShopSettings.Tweens.PunchOnSelect;
		PunchScale(1f, punch.Power, punch.Duration, punch.Vibration, punch.Elasticity, 0f);
	}

	public Tween PunchScale(float i_FromScale, float i_ScalePower, float i_ScaleDuration, int i_Vibration, float i_Elasticity, float i_Delay)
	{
		TweenCallback onStart = () => transform.localScale = Vector3.one * i_FromScale;
		TweenCallback onKill = () => transform.localScale = Vector3.one;
		transform.DOKill();
		return transform.DOPunchScale(Vector3.one * i_ScalePower, i_ScaleDuration, i_Vibration, i_Elasticity)
			.OnStart(onStart)
			.SetDelay(i_Delay)
			.OnKill(onKill);
	}
}
