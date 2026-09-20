using System.Collections.Generic;
using KobGamesSDKSlim.Shop;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace KobGamesSDKSlim
{
	public class SkinSelector : MonoBehaviour
	{
		[SerializeField]
		private Face[] m_Faces;

		[SerializeField]
		private List<Skin> m_Skins = new List<Skin>();

		[Tooltip("True - If we want to Manually set AI skins")]
		[SerializeField]
		private bool m_IsManualSelection;

		[SerializeField]
		private SkinnedMeshRenderer m_MainSkin;

		[SerializeField]
		private eOutlineType m_Outline;

		private readonly HashSet<string> m_LoadingSkins = new HashSet<string>();

		private bool IsAllowPossibleSelection => false;

		protected virtual void OnEnable()
		{
			if (!m_IsManualSelection)
			{
				Wayne.Game.Shops.SkinShopManager.OnSelectItem += onSelectItem;
				if (Wayne.Game.Shops.SkinShopManager.Instance != null)
					onSelectItem(Wayne.Game.Shops.SkinShopManager.Instance.GetSelectedItem());
			}
		}

		protected virtual void OnDisable()
		{
			if (!m_IsManualSelection) Wayne.Game.Shops.SkinShopManager.OnSelectItem -= onSelectItem;
		}

		protected virtual void onSelectItem(TItemData i_ItemData)
		{
			if (i_ItemData == null || !i_ItemData.IsSelected) return;
			Face face = m_Faces == null ? null : System.Linq.Enumerable.FirstOrDefault(m_Faces, x => x != null && x.IsItemToActivate(i_ItemData.Name));
			Skin skin = m_Skins == null ? null : System.Linq.Enumerable.FirstOrDefault(m_Skins, x => x != null && x.IsItemToActivate(i_ItemData.Name));
			if (skin == null)
			{
				DisableAllFaces();
				if (face != null) face.Activate();
				spawnSkin(i_ItemData.Name);
				return;
			}
			DisableAllItems();
			skin.Activate();
			if (face != null) face.Activate();
		}

		public void SelectRandom()
		{
			if (m_Faces == null || m_Faces.Length == 0) throw new System.NullReferenceException(nameof(m_Faces));
			Face face = m_Faces[Random.Range(0, m_Faces.Length)];
			if (face == null) throw new System.NullReferenceException(nameof(face));
			face.Activate();
		}

		private void selectItem(SkinSelectorItem i_Skin)
		{
			if (i_Skin != null) i_Skin.Activate();
		}

		public void DisableAllItems()
		{
			DisableAllFaces();
			DisableAllSkins();
		}

		private void DisableAllFaces()
		{
			if (m_Faces == null)
				return;
			for (int i = 0; i < m_Faces.Length; i++)
				if (m_Faces[i] != null) m_Faces[i].Deactivate();
		}

		private void DisableAllSkins()
		{
			if (m_Skins == null)
				return;
			for (int i = 0; i < m_Skins.Count; i++)
				if (m_Skins[i] != null) m_Skins[i].Deactivate();
		}

		private void spawnSkin(string i_String)
		{
			if (m_LoadingSkins.Contains(i_String))
				return;
			SkinAddressableData.SkinData skinAsset = GameConfig.Instance.Addressables.Skin.GetSkin(i_String);
			if (skinAsset == null)
				return;
			if (skinAsset.Prefab != null)
			{
				setupInstantiatedSkin(Instantiate(skinAsset.Prefab, transform), i_String, skinAsset.Type);
				return;
			}
			if (skinAsset.AssetReference == null)
				return;
			m_LoadingSkins.Add(i_String);
			AsyncOperationHandle<GameObject> result = skinAsset.AssetReference.InstantiateAsync(transform);
			result.Completed += operation => onSkinInstantiated(operation, i_String, skinAsset.Type);
		}

		private void onSkinInstantiated(AsyncOperationHandle<GameObject> i_Operation, string i_SkinName, eSkinType i_SkinType)
		{
			m_LoadingSkins.Remove(i_SkinName);
			if (i_Operation.Status != AsyncOperationStatus.Succeeded || i_Operation.Result == null)
				return;
			GameObject go = i_Operation.Result;
			setupInstantiatedSkin(go, i_SkinName, i_SkinType);
		}

		private void setupInstantiatedSkin(GameObject go, string i_SkinName, eSkinType i_SkinType)
		{
			if (go == null)
				return;
			Skin skin = go.GetComponent<Skin>();
			if (skin == null)
				return;
			Material outline = GameConfig.Instance.GamePlay.UnitData.GeneralUnitParameters.Outlines.GetOutline(i_SkinType, m_Outline);
			skin.AssignSkin(m_MainSkin, outline, gameObject.layer);
			if (m_Skins == null)
				m_Skins = new List<Skin>();
			m_Skins.Add(skin);
			TItemData selectedItem = Wayne.Game.Shops.SkinShopManager.Instance != null ? Wayne.Game.Shops.SkinShopManager.Instance.GetSelectedItem() : null;
			if (selectedItem != null && selectedItem.IsSelected && string.Equals(selectedItem.Name, i_SkinName))
			{
				Face face = m_Faces == null ? null : System.Linq.Enumerable.FirstOrDefault(m_Faces, x => x != null && x.IsItemToActivate(i_SkinName));
				DisableAllItems();
				skin.Activate();
				if (face != null) face.Activate();
			}
			else
			{
				skin.Deactivate();
			}
		}

		public virtual void SetRefs()
		{
			m_Faces = GetComponentsInChildren<Face>(true);
			m_Skins = new List<Skin>(GetComponentsInChildren<Skin>(true));
		}

		private void OnValidate()
		{
			SetRefs();
		}
	}
}
