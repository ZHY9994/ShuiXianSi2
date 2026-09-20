using System;

namespace KobGamesSDKSlim
{
	[Flags]
	public enum eCameraZoomType : byte
	{
		Dolly = 1,
		Ortho = 2,
		Perspective = 4
	}
}
