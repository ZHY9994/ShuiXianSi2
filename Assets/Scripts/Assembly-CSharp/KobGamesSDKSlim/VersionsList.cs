using System;
using System.Collections.Generic;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class VersionsList
	{
		public VersionsList()
		{
			Versions = new List<VersionInfo>();
		}
		public List<VersionInfo> Versions;
	}
}
