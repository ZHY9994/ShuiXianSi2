using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class SecondLayerStyleSettings
	{
		public bool showCloseButton;

		public SecondLayerStyleSettings(bool showCloseButton)
		{
			this.showCloseButton = showCloseButton;
		}
	}
}
