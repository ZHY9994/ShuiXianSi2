using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KobGamesSDKSlim
{
	public class UnityMainThreadDispatcher : MonoBehaviour
	{
		private static readonly Queue<Action> _executionQueue;

		private static UnityMainThreadDispatcher _instance;

		static UnityMainThreadDispatcher()
		{
			_executionQueue = new Queue<Action>();
		}

		public void Update()
		{
			lock (_executionQueue)
			{
				while (_executionQueue.Count > 0)
					_executionQueue.Dequeue()?.Invoke();
			}
		}

		public void Enqueue(IEnumerator action)
		{
			if (action == null) return;
			lock (_executionQueue) _executionQueue.Enqueue(() => StartCoroutine(action));
		}

		public void Enqueue(Action action)
		{
			if (action == null) return;
			lock (_executionQueue) _executionQueue.Enqueue(action);
		}

		public void Enqueue(Action action, float delay)
		{
			if (action == null) return;
			lock (_executionQueue) _executionQueue.Enqueue(() => StartCoroutine(ActionWrapper(action, delay)));
		}

		private IEnumerator ActionWrapper(Action a)
		{
			a();
			yield return null;
		}

		private IEnumerator ActionWrapper(Action a, float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			a();
			yield return null;
		}

		public static bool Exists()
		{
			return _instance != null;
		}

		public static UnityMainThreadDispatcher Instance()
		{
			if (_instance == null)
			{
				var go = new GameObject("UnityMainThreadDispatcher");
				_instance = go.AddComponent<UnityMainThreadDispatcher>();
				DontDestroyOnLoad(go);
			}
			return _instance;
		}

		private void Awake()
		{
			if (_instance != null && _instance != this)
			{
				Destroy(gameObject);
				return;
			}
			_instance = this;
			SceneManager.sceneLoaded += onSceneLoaded;
		}

		private void onSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			if (_instance == null) _instance = this;
		}
	}
}
