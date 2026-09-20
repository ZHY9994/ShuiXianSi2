namespace KobGamesSDKSlim.Analytics
{
	public interface IGameLoop
	{
		void OnLevelLoaded(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData);

		void OnLevelStarted(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData);

		void OnLevelCompleted(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData);

		void OnLevelFailed(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData);

		void OnLevelContinued(GameLoopEventsData i_GameLoopEventsData, DynamicDataBase i_DynamicData);
	}
}
