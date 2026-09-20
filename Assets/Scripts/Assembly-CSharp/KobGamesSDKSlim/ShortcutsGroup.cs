using UnityEngine;

namespace KobGamesSDKSlim
{
	[CreateAssetMenu(fileName = "Shortcuts Group - ", menuName = "KobGames/Shortcuts Group")]
	public class ShortcutsGroup : ScriptableObject
	{
		public string GroupName;

		public ShortcutsKeysCombination[] Shortcuts;

		public ShortcutsGroup()
		{
		}
	}
}
