using System;

namespace KobGamesSDKSlim.Analytics
{
	public abstract class TimedPoolObject<T> where T : class, IFactoryObject
	{
		protected static void registerTypeForFactory(Type i_Type, IFactoryObject i_Object)
		{
			TimedPoolManager.RegisterTypeForFactory(i_Type, i_Object);
		}

		protected static T createObject()
		{
			return TimedPoolManager.CreateObject<T>();
		}
	}
}
