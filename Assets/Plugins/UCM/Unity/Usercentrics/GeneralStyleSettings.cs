using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class GeneralStyleSettings
	{
		public bool androidDisableSystemBackButton;

		public string androidStatusBarColor;

		public bool androidWindowFullscreen;

		public string textColor;

		public string layerBackgroundColor;

		public string layerBackgroundSecondaryColor;

		public string linkColor;

		public string tabColor;

		public string bordersColor;

		public ToggleStyleSettings toggleStyleSettings;

		public string logoImageUrl;

		public LegalLinksSettings links;

		private GeneralStyleSettings()
		{
			return;
		}

		public GeneralStyleSettings(bool androidDisableSystemBackButton = true, string androidStatusBarColor = "", bool androidWindowFullscreen = true, string textColor = "", string layerBackgroundColor = "", string layerBackgroundSecondaryColor = "", string linkColor = "", string tabColor = "", string bordersColor = "", ToggleStyleSettings toggleStyleSettings = null, string logoImageUrl = "", LegalLinksSettings links = LegalLinksSettings.Undefined)
		{
			this.androidDisableSystemBackButton = androidDisableSystemBackButton;
			this.androidStatusBarColor = androidStatusBarColor;
			this.androidWindowFullscreen = androidWindowFullscreen;
			this.textColor = textColor;
			this.layerBackgroundColor = layerBackgroundColor;
			this.layerBackgroundSecondaryColor = layerBackgroundSecondaryColor;
			this.linkColor = linkColor;
			this.tabColor = tabColor;
			this.bordersColor = bordersColor;
			this.toggleStyleSettings = toggleStyleSettings;
			this.logoImageUrl = logoImageUrl;
			this.links = links;
		}
	}
}
