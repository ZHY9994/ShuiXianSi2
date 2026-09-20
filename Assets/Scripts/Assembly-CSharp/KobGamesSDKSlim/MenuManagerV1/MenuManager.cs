namespace KobGamesSDKSlim.MenuManagerV1
{
	[ExecutionOrder(-60)]
	public class MenuManager : MenuManagerBase
	{
		public MenuManager()
		{
		}

		public override void Start()
		{
			base.Start();
		}

		public void OpenWelcomeScreen()
		{
			const string screenName = "Screen_Welcome";
			if (!IsScreenOpened(screenName))
				OpenMenuScreen(screenName);
		}
	}
}
