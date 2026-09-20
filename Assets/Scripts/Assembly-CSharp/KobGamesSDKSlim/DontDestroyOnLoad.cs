using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		private static HashSet<string> PersistentGameObjects = new HashSet<string>();

		private string m_Key;

		private void Awake()
		{
			m_Key = string.Format("{0}_{1}", gameObject.name, transform.GetSiblingIndex());
			if (PersistentGameObjects.Contains(m_Key))
			{
				Destroy(gameObject);
				return;
			}
			PersistentGameObjects.Add(m_Key);
			DontDestroyOnLoad(gameObject);
		}

		private void OnApplicationQuit()
		{
			PersistentGameObjects.Clear();
		}

		public DontDestroyOnLoad()
		{
		}

	}
}
