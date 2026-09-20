using System;
using System.Collections.Generic;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class RateUsEditor
	{
		public List<int> RateUsLevels;

		public ePopupMode RateUsPopupMode;

		public bool IsRateUsEnabled
		{
			get
			{
				return Managers.Instance.RemoteSettings.FirebaseRemoteConfig.IsRateUsEnabled.Value;
			}
			set
			{
				Managers.Instance.RemoteSettings.FirebaseRemoteConfig.IsRateUsEnabled.SetToDefaultValue(value);
			}
		}

		public RateUsEditor()
		{
			RateUsLevels = new List<int> { 6, 15, 25 };
			RateUsPopupMode = ePopupMode.LevelLoad;
		}
	}
}
