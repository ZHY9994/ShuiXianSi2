using System.Collections.Generic;

namespace KobGamesSDKSlim.Analytics
{
	public class PooledDictionary<TKey, TValue> : TimedPoolObject<PooledDictionary<TKey, TValue>>, IFactoryObject
	{
		private const int c_DICTIONARY_CAPACITY = 20;

		private Dictionary<TKey, TValue> m_Dictionary;

		protected PooledDictionary()
		{
		}

		IFactoryObject IFactoryObject.Instantiate()
		{
			return new PooledDictionary<TKey, TValue>();
		}

		protected static Dictionary<TKey, TValue> createDictionary()
		{
			return new Dictionary<TKey, TValue>(c_DICTIONARY_CAPACITY);
		}

		public Dictionary<TKey, TValue> GetDictionary()
		{
			return m_Dictionary ?? (m_Dictionary = createDictionary());
		}
	}
}
