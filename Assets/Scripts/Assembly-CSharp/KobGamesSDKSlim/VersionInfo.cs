using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class VersionInfo
	{
		[SerializeField]
		public string Name;

		[SerializeField]
		public string Version;

		public VersionInfo()
		{
		}
	}
}
