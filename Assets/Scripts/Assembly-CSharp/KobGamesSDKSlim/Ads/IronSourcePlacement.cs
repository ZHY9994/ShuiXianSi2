namespace KobGamesSDKSlim.Ads
{
	public class IronSourcePlacement
	{
		private string v1;

		private string v2;

		private int v3;

		public IronSourcePlacement(string v1, string v2, int v3)
		{
			this.v1 = v1;
			this.v2 = v2;
			this.v3 = v3;
		}

		public string getPlacementName()
		{
			return "Dummy Placement";
		}

		public string getRewardName()
		{
			return "Dummy Reward Name";
		}
	}
}
