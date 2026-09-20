using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class ButtonLayout
	{
		public ButtonLayoutType type;

		public List<ButtonSettings> buttons;

		public List<ButtonSettingsRow> gridButtons;

		public static ButtonLayout Grid(List<ButtonSettingsRow> buttons)
		{
			return new ButtonLayout(buttons);
		}

		public static ButtonLayout Column(List<ButtonSettings> buttons)
		{
			return new ButtonLayout(ButtonLayoutType.Column, buttons);
		}

		public static ButtonLayout Row(List<ButtonSettings> buttons)
		{
			return new ButtonLayout(ButtonLayoutType.Row, buttons);
		}

		private ButtonLayout(ButtonLayoutType type, List<ButtonSettings> buttons)
		{
			this.type = type;
			this.buttons = buttons;
		}

		private ButtonLayout(List<ButtonSettingsRow> gridButtons)
		{
			type = ButtonLayoutType.Grid;
			this.gridButtons = gridButtons;
		}
	}
}
