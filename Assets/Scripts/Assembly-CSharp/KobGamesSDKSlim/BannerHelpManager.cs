using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class BannerHelpManager : Singleton<BannerHelpManager>
	{
		private bool m_IsHaveBanner;

		private new void OnEnable()
		{
			AdsManagerBase.OnBannerShown += bannerShowCallback;
			AdsManagerBase.OnBannerHidden += bannerHiddenCallback;
		}

		public override void OnDisable()
		{
			base.OnDisable();
			AdsManagerBase.OnBannerShown -= bannerShowCallback;
			AdsManagerBase.OnBannerHidden -= bannerHiddenCallback;
		}

		private void bannerShowCallback()
		{
			m_IsHaveBanner = true;
		}

		private void bannerHiddenCallback()
		{
			m_IsHaveBanner = false;
		}

		public float GetBannerSize(RectTransform i_Canvas)
		{
			if (!m_IsHaveBanner)
				return 0f;
			return getBannerSize() / i_Canvas.localScale.y;
		}

		private int getBannerSize()
		{
			float dpiScale = Screen.dpi / 160f;
			float height = Screen.height;
			float size = height > dpiScale * 720f ? dpiScale * 88f : height > dpiScale * 400f ? dpiScale * 44f : dpiScale * 32f;
			return (int)Math.Round(size, MidpointRounding.ToEven);
		}
	}
}
