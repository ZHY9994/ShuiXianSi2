using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class FirstLayerStyleSettings
	{
		public UsercentricsLayout layout;

		public HeaderImageSettings headerImage;

		public TitleSettings title;

		public MessageSettings message;

		public ButtonLayout buttonLayout;

		public string backgroundColor;

		public float cornerRadius;

		public string overlayColor;

		public float overlayAlpha;

		public FirstLayerStyleSettings(UsercentricsLayout layout)
		{
			this.layout = layout;
			this.headerImage = null;
			this.title = null;
			this.message = null;
			this.buttonLayout = null;
			this.backgroundColor = "";
			this.cornerRadius = 0f;
			this.overlayColor = "";
			this.overlayAlpha = 1f;
			this.layout = layout;
		}

		public FirstLayerStyleSettings(UsercentricsLayout layout, HeaderImageSettings headerImage = null, TitleSettings title = null, MessageSettings message = null, ButtonLayout buttonLayout = null, string backgroundColor = "", float cornerRadius = 0f, string overlayColor = "", float overlayAlpha = 1f)
		{
			this.layout = layout;
			this.headerImage = headerImage;
			this.title = title;
			this.message = message;
			this.buttonLayout = buttonLayout;
			this.backgroundColor = backgroundColor;
			this.cornerRadius = cornerRadius;
			this.overlayColor = overlayColor;
			this.overlayAlpha = overlayAlpha;
			this.layout = layout;
			this.headerImage = headerImage;
			this.title = title;
			this.message = message;
			this.buttonLayout = buttonLayout;
			this.backgroundColor = backgroundColor;
			this.cornerRadius = cornerRadius;
			this.overlayColor = overlayColor;
			this.overlayAlpha = overlayAlpha;
		}
	}
}
