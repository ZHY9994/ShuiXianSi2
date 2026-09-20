using UnityEngine;

namespace KobGamesSDKSlim.Analytics.Modules
{
	public abstract class ModuleBase : MonoBehaviour
	{
		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}

		protected virtual void OnApplicationPause(bool pauseStatus)
		{
		}
	}
}
