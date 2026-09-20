using System;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class RemoteSettingsEditor
	{
		public RemoteSettingsEditor()
		{
		}

		public RemoteSettingsManager RemoteSettingsManager
		{
			get
			{
				return Managers.Instance.RemoteSettings;
			}
			set
			{
				Managers.Instance.RemoteSettings = value;
			}
		}
	}
}
