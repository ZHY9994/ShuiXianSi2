using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class ToggleStyleSettings
	{
		public string activeBackgroundColor;

		public string inactiveBackgroundColor;

		public string disabledBackgroundColor;

		public string activeThumbColor;

		public string inactiveThumbColor;

		public string disabledThumbColor;

		public ToggleStyleSettings(string activeBackgroundColor = "", string inactiveBackgroundColor = "", string disabledBackgroundColor = "", string activeThumbColor = "", string inactiveThumbColor = "", string disabledThumbColor = "")
		{
			this.activeBackgroundColor = activeBackgroundColor;
			this.inactiveBackgroundColor = inactiveBackgroundColor;
			this.disabledBackgroundColor = disabledBackgroundColor;
			this.activeThumbColor = activeThumbColor;
			this.inactiveThumbColor = inactiveThumbColor;
			this.disabledThumbColor = disabledThumbColor;
		}
	}
}
