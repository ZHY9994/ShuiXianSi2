using System.Collections.Generic;

namespace KobGamesSDKSlim.Analytics
{
	public class PooledList<TListType> : TimedPoolObject<PooledList<TListType>>, IFactoryObject
	{
		private const int c_LIST_CAPACITY = 20;

		private List<TListType> m_List;

		protected PooledList()
		{
		}

		IFactoryObject IFactoryObject.Instantiate()
		{
			return new PooledList<TListType>();
		}

		protected static List<TListType> createList()
		{
			return new List<TListType>(c_LIST_CAPACITY);
		}

		public List<TListType> GetList()
		{
			return m_List ?? (m_List = createList());
		}
	}
}
