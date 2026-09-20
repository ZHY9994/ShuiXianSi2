using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class BannerSettings
	{
		public GeneralStyleSettings generalStyleSettings;

		public FirstLayerStyleSettings firstLayerStyleSettings;

		public SecondLayerStyleSettings secondLayerStyleSettings;

		public string variantName;

		public BannerSettings(GeneralStyleSettings generalStyleSettings = null, FirstLayerStyleSettings firstLayerStyleSettings = null, SecondLayerStyleSettings secondLayerStyleSettings = null, string variantName = "")
		{
			this.generalStyleSettings = generalStyleSettings;
			this.firstLayerStyleSettings = firstLayerStyleSettings;
			this.secondLayerStyleSettings = secondLayerStyleSettings;
			this.variantName = variantName;
		}
	}
}
