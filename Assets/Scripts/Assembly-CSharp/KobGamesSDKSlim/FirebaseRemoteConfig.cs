using System;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class FirebaseRemoteConfig : FirebaseRemoteConfigBase
	{
		public override void SetDefaults()
		{
			base.SetDefaults();
		}

		protected override void RefrectPropertiesSafe()
		{
			return;
		}

		public void UpdateRemoteConfigs(int i_InterstitalLevel, int i_Cooldown)
		{
			return;
		}

		public FirebaseRemoteConfig()
		{
		}
	}
}
