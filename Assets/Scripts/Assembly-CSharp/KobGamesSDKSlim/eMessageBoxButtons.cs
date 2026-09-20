using System;

namespace KobGamesSDKSlim
{
	[Flags]
	public enum eMessageBoxButtons
	{
		None = 1,
		All = 2,
		Ok = 4,
		Yes = 8,
		No = 0x10,
		Next = 0x20
	}
}
