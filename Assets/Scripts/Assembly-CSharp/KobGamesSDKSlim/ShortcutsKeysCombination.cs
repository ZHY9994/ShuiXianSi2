using System;
using Sirenix.OdinInspector;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class ShortcutsKeysCombination
	{
		public ShortcutKey[] Keys;

		public string Action;

		private ValueDropdownList<string> ShortcutActions()
		{
			return ShortcutsActionsBase.NotUsedFields;
		}

		public ShortcutsKeysCombination()
		{
			Keys = Array.Empty<ShortcutKey>();
			Action = "Undefined";
		}
	}
}
