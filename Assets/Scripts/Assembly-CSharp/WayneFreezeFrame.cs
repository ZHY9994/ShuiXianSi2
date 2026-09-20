using KobGamesSDKSlim;
using UnityEngine;

public class WayneFreezeFrame : Singleton<WayneFreezeFrame>
{
	[TextArea]
	public string TIP = "You need 'Wayne Time Manager' For this to work";

	public float FreezeFrameDuration = 0.02f;

	public void FreezeFrame()
	{
		FreezeFrameEvent.Trigger(FreezeFrameDuration);
	}
}
