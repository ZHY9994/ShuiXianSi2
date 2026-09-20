namespace KobGamesSDKSlim.Shop
{
	public static class eItemUnlockUtility
	{
		public static string ToValue(this eItemUnlockMethod i_UnlockMethod)
		{
			switch (i_UnlockMethod)
			{
				case eItemUnlockMethod.Unlocked:
					return "unlocked";
				case eItemUnlockMethod.AD:
					return "ad";
				case eItemUnlockMethod.Cash:
					return "cash";
				default:
					return "unlocked";
			}
		}
	}
}
