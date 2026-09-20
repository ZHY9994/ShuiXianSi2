using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class ButtonSettings
	{
		public ButtonType type;

		public float textSize;

		public string textColor;

		public string backgroundColor;

		public float cornerRadius;

		public bool isAllCaps;

		public ButtonSettings(ButtonType type)
		{
			this.type = type;
		}

		public ButtonSettings(ButtonType type, float textSize = 0f, string textColor = "", string backgroundColor = "", float cornerRadius = 0f, bool isAllCaps = false)
		{
			this.type = type;
			this.textSize = textSize;
			this.textColor = textColor;
			this.backgroundColor = backgroundColor;
			this.cornerRadius = cornerRadius;
			this.isAllCaps = isAllCaps;
		}
	}
}
