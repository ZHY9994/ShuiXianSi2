using UnityEngine;

namespace KobGamesSDKSlim
{
	public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
	{
		protected SingletonBase()
		{
		}

		protected virtual void Awake()
		{
			return;
		}
	}
}
