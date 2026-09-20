using KobGamesSDKSlim;

public class LoadingManager : Singleton<LoadingManager>
{
	protected override void OnEnable()
	{
		base.OnEnable();
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_Loading");
	}

	public void CloseScreen()
	{
		CancelInvoke(nameof(CloseScreen));
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.CloseMenuScreen("Screen_Loading");
	}

	public void CloseScreenDelayed(float i_Delay)
	{
		CancelInvoke(nameof(CloseScreen));
		Invoke(nameof(CloseScreen), i_Delay);
	}
}
