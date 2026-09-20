using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class ShortcutsEditor
	{
		[SerializeField]
		private bool EditMode;

		public ShortcutsGroup[] ShortcutsGroup;

		public ShortcutsEditor()
		{
			ShortcutsGroup = Array.Empty<ShortcutsGroup>();
		}

		private bool Enabled
		{
			get
			{
				return ShortcutsManagerBase.IsShortcutsEnable;
			}
			set
			{
				ShortcutsManagerBase.IsShortcutsEnable = value;
			}
		}
	}
}
