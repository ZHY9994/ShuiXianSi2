using System;

namespace KobGamesSDKSlim.Analytics
{
	[Obsolete]
	public struct kv
	{
		public string key;

		public bool IsString;

		public string valueString;

		public bool IsLong;

		public long valueLong;

		public bool IsDouble;

		public double valueDouble;

		public void SetValueString(string i_String)
		{
			ClearValues(); IsString = true; valueString = i_String;
		}

		public void SetValueLong(long i_Long)
		{
			ClearValues(); IsLong = true; valueLong = i_Long;
		}

		public void SetValueDouble(double i_Double)
		{
			ClearValues(); IsDouble = true; valueDouble = i_Double;
		}

		public void ClearValues()
		{
			IsString = IsLong = IsDouble = false; valueString = null; valueLong = 0L; valueDouble = 0.0;
		}

		public Tuple<string, object> ToTuple()
		{
			if (IsString) return Tuple.Create(key, (object)valueString); if (IsLong) return Tuple.Create(key, (object)valueLong); if (IsDouble) return Tuple.Create(key, (object)valueDouble); return Tuple.Create(key, (object)null);
		}

		public EventNameObjectPair ToKeyValuePair()
		{
			return EventNameObjectPair.CreatePooled(key, ToTuple().Item2);
		}
	}
}
