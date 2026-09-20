using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class SDKVersionInfo
	{
		public string Name;

		public string Version;

		public string URL;

		public SDKVersionInfo()
		{
		}

		public void Open()
		{
			Application.OpenURL(URL);
		}
	}
}
