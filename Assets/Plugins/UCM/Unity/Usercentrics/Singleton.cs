using UnityEngine;

namespace Unity.Usercentrics
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static bool m_ShuttingDown;

		private static object m_Lock;

		private static T m_Instance;

		public static T Instance
		{
			get
			{
				if (m_ShuttingDown) return null;
				lock (m_Lock ?? (m_Lock = new object()))
				{
					if (m_Instance == null)
					{
						m_Instance = Object.FindObjectOfType<T>();
						if (m_Instance == null)
						{
							GameObject singleton = new GameObject(typeof(T).Name);
							m_Instance = singleton.AddComponent<T>();
							Object.DontDestroyOnLoad(singleton);
						}
					}
					return m_Instance;
				}
			}
		}

		private void OnApplicationQuit()
		{
			m_ShuttingDown = true;
		}

		private void OnDestroy()
		{
			if (m_Instance == this) m_Instance = null;
		}
	}
}
