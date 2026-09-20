using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	public class AdsBannerBgPanel : MonoBehaviour
	{
		public delegate void StateUpdatedListener(AdsBannerBgPanel i_BannerBgPanel);

		private static StateUpdatedListener m_StateUpdatedListeners;

		public Image BannerBgPanelImage;

		[SerializeField]
		private RectTransform m_RectTransform;

		[SerializeField]
		private Canvas m_AdsCanvas;

		private Canvas m_MainCanvasCache;

		private int m_OldWidth;

		private int m_Oldheight;

		private bool m_IsBannerShown;

		private Vector2 m_SizeDeltaDummy = Vector2.zero;

		private float m_DPI;

		private int m_ScreenHeight;

		private Camera m_MainCamera;

		private Canvas m_MainCanvas => m_MainCanvasCache != null ? m_MainCanvasCache : (m_MainCanvasCache = GetComponentInParent<Canvas>());

		public float MainHeight => GetHeight(m_MainCanvas);

		public static event StateUpdatedListener StateUpdatedListeners
		{
			[CompilerGenerated]
			add
			{
				StateUpdatedListener current;
				StateUpdatedListener updated;
				do
				{
					current = m_StateUpdatedListeners;
					updated = (StateUpdatedListener)Delegate.Combine(current, value);
				}
				while (Interlocked.CompareExchange(ref m_StateUpdatedListeners, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				StateUpdatedListener current;
				StateUpdatedListener updated;
				do
				{
					current = m_StateUpdatedListeners;
					updated = (StateUpdatedListener)Delegate.Remove(current, value);
				}
				while (Interlocked.CompareExchange(ref m_StateUpdatedListeners, updated, current) != current);
			}
		}

		private void OnValidate()
		{
			if (BannerBgPanelImage == null) BannerBgPanelImage = GetComponentInChildren<Image>();
			if (m_RectTransform == null && BannerBgPanelImage != null) m_RectTransform = BannerBgPanelImage.GetComponent<RectTransform>();
			if (m_AdsCanvas == null) m_AdsCanvas = GetComponentInParent<Canvas>();
		}

		public float GetHeight()
		{
			return GetHeight(m_MainCanvas);
		}

		public float GetHeight(Canvas i_MainCanvas)
		{
			if (i_MainCanvas == null) return 0f;
			return getBannerSize() / (i_MainCanvas.scaleFactor > 0f ? i_MainCanvas.scaleFactor : 1f);
		}

		private void Awake()
		{
			if (BannerBgPanelImage != null) BannerBgPanelImage.enabled = false;
		}

		private void OnEnable()
		{
			return;
		}

		private void OnDestroy()
		{
			RemoteSettingsManager.OnFirebaseRemotConfigUpdated -= onFirebaseRemotConfigUpdated;
		}

		private void onFirebaseRemotConfigUpdated(bool i_Success)
		{
			return;
		}

		public void UpdateBannerBgPanelImage()
		{
			return;
		}

		private void Update()
		{
			if (m_MainCamera == null) m_MainCamera = Camera.main;
			if (m_MainCamera == null) return;
			int width = m_MainCamera.pixelWidth;
			int height = m_MainCamera.pixelHeight;
			if (width == m_OldWidth && height == m_Oldheight) return;
			m_OldWidth = width;
			m_Oldheight = height;
			if (m_IsBannerShown) OnBannerShown(null);
		}

		public void OnBannerShown(string i_PlacementId)
		{
			m_IsBannerShown = true;
			if (BannerBgPanelImage != null)
			{
				BannerBgPanelImage.enabled = true;
				updateRectTransform();
			}
		}

		public void OnBannerHidden(string i_PlacementId)
		{
			m_IsBannerShown = false;
			if (BannerBgPanelImage != null) BannerBgPanelImage.enabled = false;
		}

		private void updateRectTransform()
		{
			if (m_RectTransform == null) return;
			m_SizeDeltaDummy = new Vector2(m_RectTransform.sizeDelta.x, getBannerSize());
			m_RectTransform.sizeDelta = m_SizeDeltaDummy;
		}

		private int getBannerSize()
		{
			return Mathf.RoundToInt(Screen.dpi > 0f ? 50f * Screen.dpi / 160f : 50f);
		}
	}
}
