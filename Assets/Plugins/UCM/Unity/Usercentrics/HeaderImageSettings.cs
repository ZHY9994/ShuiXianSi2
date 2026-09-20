using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class HeaderImageSettings
	{
		public HeaderImageType imageType;

		public string imageUrl;

		public SectionAlignment alignment;

		public float height;

		public static HeaderImageSettings Extended(string imageUrl)
		{
			return new HeaderImageSettings(HeaderImageType.Extended, imageUrl);
		}

		public static HeaderImageSettings Hidden()
		{
			return new HeaderImageSettings(HeaderImageType.Hidden);
		}

		public static HeaderImageSettings Custom(string imageUrl, SectionAlignment alignment, float height = 0f)
		{
			return new HeaderImageSettings(HeaderImageType.Custom, imageUrl, alignment, height);
		}

		private HeaderImageSettings(HeaderImageType imageType, string imageUrl = "")
		{
			this.imageType = imageType;
			this.imageUrl = imageUrl;
		}

		private HeaderImageSettings(HeaderImageType imageType, string imageUrl, SectionAlignment alignment, float height = 0f)
		{
			this.imageType = imageType;
			this.imageUrl = imageUrl;
			this.alignment = alignment;
			this.height = height;
		}
	}
}
