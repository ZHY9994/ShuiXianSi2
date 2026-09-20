using KobGamesSDKSlim.Shop;
using KobGamesSDKSlim;
using Wayne.Game.Shops;
using UnityEngine;

public class RandomFace : MonoBehaviour
{
	[SerializeField]
	private Material m_PlayerFaceMat;

	private TItemData m_SelectedSkin => SkinShopManager.Instance.GetSelectedItem();

	protected virtual eFaceType FaceType => eFaceType.Happy;

	private void OnEnable()
	{
		randomFace();
		SkinShopManager.OnSelectItem += onSelectItem;
	}

	private void OnDisable()
	{
		SkinShopManager.OnSelectItem -= onSelectItem;
	}

	private void onSelectItem(TItemData i_Item)
	{
		randomFace();
	}

	private void randomFace()
	{
		TItemData selectedSkin = m_SelectedSkin;
		Texture face = GameConfig.Instance.GamePlay.UnitData.GeneralUnitParameters.GetRandomFace(selectedSkin.Name, FaceType);
		m_PlayerFaceMat.SetTexture(StaticString.SP_BaseTexture, face);
	}

	private void debugFace(eFaceType i_Type)
	{
		TItemData selectedSkin = m_SelectedSkin;
		Texture face = GameConfig.Instance.GamePlay.UnitData.GeneralUnitParameters.GetRandomFace(selectedSkin.Name, i_Type);
		m_PlayerFaceMat.SetTexture(StaticString.SP_BaseTexture, face);
	}
}
