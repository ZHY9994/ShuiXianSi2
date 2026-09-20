using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public struct ShortcutKey
	{
		public KeyCode Key;

		public eShortcutKeyState State;
	}
}
