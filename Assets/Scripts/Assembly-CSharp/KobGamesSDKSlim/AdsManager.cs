namespace KobGamesSDKSlim
{
	[ExecutionOrder(-80)]
	public class AdsManager : AdsManagerBase
	{
		public AdsManager()
			: base()
		{
		}

		protected override void LevelCompleted()
		{
			m_IsRewardVideoShownOnThisLevel = false;
			base.LevelCompleted();
		}
	}
}
