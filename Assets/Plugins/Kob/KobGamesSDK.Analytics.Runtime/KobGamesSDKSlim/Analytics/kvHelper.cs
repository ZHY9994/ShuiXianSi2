using System;

namespace KobGamesSDKSlim.Analytics
{
	public static class kvHelper
	{
		private static kv m_DummyKV;

		[Obsolete]
		public static kv kv(string i_Key, string i_Value)
		{
			m_DummyKV = new kv { key = i_Key }; m_DummyKV.SetValueString(i_Value); return m_DummyKV;
		}

		[Obsolete]
		public static kv kv(string i_Key, int i_Value)
		{
			m_DummyKV = new kv { key = i_Key }; m_DummyKV.SetValueLong(i_Value); return m_DummyKV;
		}

		[Obsolete]
		public static kv kv(string i_Key, float i_Value)
		{
			m_DummyKV = new kv { key = i_Key }; m_DummyKV.SetValueDouble(i_Value); return m_DummyKV;
		}

		[Obsolete]
		public static kv kv(string i_Key, bool i_Value)
		{
			m_DummyKV = new kv { key = i_Key }; m_DummyKV.SetValueString(i_Value.ToString()); return m_DummyKV;
		}
	}
}
