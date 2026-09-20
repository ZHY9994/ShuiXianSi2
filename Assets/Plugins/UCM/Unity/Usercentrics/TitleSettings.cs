using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class TitleSettings
	{
		public SectionAlignment alignment;

		public float textSize;

		public string textColor;

		public TitleSettings(SectionAlignment alignment, float textSize = 0f, string textColor = "")
		{
			this.alignment = alignment;
			this.textSize = textSize;
			this.textColor = textColor;
		}
	}
}
