using KobGamesSDKSlim;

public class Test4 : Singleton<Test4>
{
	public string EventName = "level_1_achieve";

	public bool Result => EventName.Contains("_achieve");
}
