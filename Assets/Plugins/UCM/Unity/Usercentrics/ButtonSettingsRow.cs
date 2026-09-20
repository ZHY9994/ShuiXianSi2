using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class ButtonSettingsRow
	{
		public List<ButtonSettings> buttons;

		public ButtonSettingsRow(List<ButtonSettings> buttons)
		{
			this.buttons = buttons;
		}
	}
}
