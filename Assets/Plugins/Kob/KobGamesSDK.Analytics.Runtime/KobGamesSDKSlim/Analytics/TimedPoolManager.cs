using System;
using System.Collections.Generic;
using System.Threading;

namespace KobGamesSDKSlim.Analytics
{
	internal static class TimedPoolManager
	{
		private const int c_POOLING_INITIAL_COUNT = 50;

		private const int c_MIN_TO_START_POOL = 25;

		private const int c_POOLING_DELAY = 15;

		private static bool m_CleanupStarted;

		private static Thread m_Thread;

		private static bool m_IsQuitting;

		private static readonly DateTime c_INIT_TIME;

		private static Dictionary<Type, Queue<object>> m_CachedEntries;

		private static Dictionary<Type, Queue<float>> m_CachedTime;

		private static Dictionary<Type, IFactoryObject> m_FactoryObjects;

		private static float m_CurrTime => (float)(DateTime.UtcNow - c_INIT_TIME).TotalSeconds;

		private static Dictionary<Type, Stack<object>> m_PooledEntries { get; }

		static TimedPoolManager()
		{
			c_INIT_TIME = DateTime.UtcNow;
			m_CachedEntries = new Dictionary<Type, Queue<object>>();
			m_CachedTime = new Dictionary<Type, Queue<float>>();
			m_FactoryObjects = new Dictionary<Type, IFactoryObject>();
			m_PooledEntries = new Dictionary<Type, Stack<object>>();
		}

		private static bool m_ShouldStartPooling => !m_CleanupStarted && m_PooledEntries.Count > c_MIN_TO_START_POOL;

		private static void startPool()
		{
			if (m_CleanupStarted) return;
			m_CleanupStarted = true;
			m_Thread = new Thread(cleanUpLoop) { IsBackground = true, Name = "KobTimedPoolCleanup" };
			m_Thread.Start();
		}

		private static void cleanUpLoop()
		{
			while (!m_IsQuitting)
			{
				Thread.Sleep(c_POOLING_DELAY * 1000);
				lock (m_PooledEntries)
				{
					foreach (Type type in new List<Type>(m_PooledEntries.Keys))
					{
						if (m_PooledEntries[type].Count == 0) m_PooledEntries.Remove(type);
					}
				}
			}
		}

		private static T getPooled<T>() where T : class
		{
			Type type = typeof(T);
			lock (m_PooledEntries)
			{
				if (m_PooledEntries.TryGetValue(type, out Stack<object> pool) && pool.Count > 0)
					return pool.Pop() as T;
			}
			return null;
		}

		private static void cache<T>(T i_Entry)
		{
			if (i_Entry == null) return;
			Type type = typeof(T);
			lock (m_PooledEntries)
			{
				if (!m_PooledEntries.TryGetValue(type, out Stack<object> pool))
					m_PooledEntries[type] = pool = new Stack<object>(c_POOLING_INITIAL_COUNT);
				pool.Push(i_Entry);
				if (m_ShouldStartPooling) startPool();
			}
		}

		public static void RegisterTypeForFactory(Type i_Type, IFactoryObject i_Object)
		{
			if (i_Type != null && i_Object != null) m_FactoryObjects[i_Type] = i_Object;
		}

		public static T CreateObject<T>() where T : class, IFactoryObject
		{
		return instantiateFromFactory<T>();
		}

		private static TObjectType instantiateFromFactory<TObjectType>() where TObjectType : IFactoryObject
		{
			Type type = typeof(TObjectType);
			if (m_FactoryObjects.TryGetValue(type, out IFactoryObject factory) && factory != null)
				return (TObjectType)factory.Instantiate();
			return (TObjectType)Activator.CreateInstance(type, true);
		}
	}
}
