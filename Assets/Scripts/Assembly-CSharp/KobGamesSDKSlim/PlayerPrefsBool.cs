namespace KobGamesSDKSlim
{
	public static class PlayerPrefsBool
	{
		public static bool GetBool(string key, bool i_Default)
		{
			return global::Ade_Framework.AdeCloudPlayerPrefs.HasKey(key)
				? global::Ade_Framework.AdeCloudPlayerPrefs.GetInt(key) == 1
				: i_Default;
		}

		public static void SetBool(string key, bool state)
		{
			global::Ade_Framework.AdeCloudPlayerPrefs.SetInt(key, state ? 1 : 0);
		}
	}
}
