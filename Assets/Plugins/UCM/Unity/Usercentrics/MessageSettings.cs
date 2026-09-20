using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class MessageSettings
	{
		public float textSize;

		public SectionAlignment alignment;

		public string textColor;

		public string linkTextColor;

		public bool underlineLink;

		public MessageSettings(float textSize, SectionAlignment alignment, string textColor, string linkTextColor, bool underlineLink)
		{
			this.textSize = textSize;
			this.alignment = alignment;
			this.textColor = textColor;
			this.linkTextColor = linkTextColor;
			this.underlineLink = underlineLink;
		}
	}
}
