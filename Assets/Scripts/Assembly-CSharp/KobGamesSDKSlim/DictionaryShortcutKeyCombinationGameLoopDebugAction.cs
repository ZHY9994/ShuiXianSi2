using System;
using KobGamesSDKSlim.Debugging;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class DictionaryShortcutKeyCombinationGameLoopDebugAction : UnitySerializedDictionary<ShortcutsKeysCombination, eGameLoopDebugType>
	{
		public DictionaryShortcutKeyCombinationGameLoopDebugAction()
		{
		}
	}
}
