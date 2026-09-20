using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim.Shop
{
	public class ShopRewardController : MonoBehaviour
	{
		[SerializeField]
		private GameObject m_Root;

		[SerializeField]
		private GameObject m_SimplePanel;

		[SerializeField]
		private Image m_FillImage;

		[SerializeField]
		private Image m_OutlineImage;

		[SerializeField]
		private Image m_CompleteFillImage;

		[SerializeField]
		private Image m_CompleteOutlineImage;

		[SerializeField]
		private TextMeshProUGUI m_PercenteText;

		[SerializeField]
		private GameObject[] m_NewSkinItems;

		[SerializeField]
		private GameObject[] m_SimpleItems;

		[SerializeField]
		private Button m_LoseNewSkinButton;

		[SerializeField]
		private Button m_GetNewSkinButton;

		[SerializeField]
		private float m_FillDuration;

		[SerializeField]
		private Ease m_FillEase;

		private float m_Progress;

		private TItemData m_PreviousItem;

		private TItemData m_NextItem;

		private void OnEnable()
		{
			m_LoseNewSkinButton?.onClick.AddListener(onLoseNewSkin);
			m_GetNewSkinButton?.onClick.AddListener(onGetNewSkin);
			showSimpleObjects();
		}

		private void OnDisable()
		{
			m_LoseNewSkinButton?.onClick.RemoveListener(onLoseNewSkin);
			m_GetNewSkinButton?.onClick.RemoveListener(onGetNewSkin);
		}

		public void Open()
		{
			return;
		}

		private void onFillComplete()
		{
			if (m_Progress >= 1f) showNewSkinObjects();
		}

		private void showSimpleObjects()
		{
			if (m_NewSkinItems != null)
				foreach (GameObject item in m_NewSkinItems) if (item != null) item.SetActive(false);
			if (m_SimpleItems != null)
				foreach (GameObject item in m_SimpleItems) if (item != null) item.SetActive(true);
		}

		private void showNewSkinObjects()
		{
			if (m_SimpleItems != null)
				foreach (GameObject item in m_SimpleItems) if (item != null) item.SetActive(false);
			if (m_NewSkinItems != null)
				foreach (GameObject item in m_NewSkinItems) if (item != null) item.SetActive(true);
		}

		private void UpdateGraphics()
		{
			if (m_NextItem == null && m_SimplePanel != null) m_SimplePanel.SetActive(false);
		}

		private void onGetNewSkin()
		{
			ADManager.Instance.ShowRewardAD(SelectItem);
		}

		private void onLoseNewSkin()
		{
			showSimpleObjects();
		}

		private void SelectItem()
		{
			return;
		}
	}
}
