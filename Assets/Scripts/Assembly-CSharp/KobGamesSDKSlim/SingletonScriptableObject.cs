using UnityEngine;

namespace KobGamesSDKSlim
{
	public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
	{
		private static T s_Instance;

		public static T Instance
		{
			get
			{
				if (s_Instance == null)
				{
					T[] objects = Resources.FindObjectsOfTypeAll<T>();
					if (objects != null && objects.Length > 0 && objects[0] != null)
						s_Instance = objects[0];
					else
					{
						T[] loaded = Resources.LoadAll<T>(string.Empty);
						if (loaded == null || loaded.Length == 0)
							throw new System.InvalidOperationException();
						s_Instance = loaded[0];
					}
				}
				return s_Instance;
			}
		}
	}
}
