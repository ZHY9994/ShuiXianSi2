namespace Wayne.Game.Tutorials
{
	public interface ITutorialModule
	{
		void OnStartUp();

		void OnActivate();

		void OnDeactivate();

		void OnGameReset();

		void OnLevelStart();

		void OnLevelComplete();
	}
}
