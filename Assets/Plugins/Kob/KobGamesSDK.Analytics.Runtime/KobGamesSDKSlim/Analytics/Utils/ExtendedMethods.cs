namespace KobGamesSDKSlim.Analytics.Utils
{
	public static class ExtendedMethods
	{
		public static string ToAllowedEventName(this string i_InputString)
		{
			if (string.IsNullOrEmpty(i_InputString)) return string.Empty;
			return i_InputString.Replace(" ", "_").Replace("/", "_").Replace("\\", "_");
		}
	}
}
