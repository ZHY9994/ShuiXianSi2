namespace KobGamesSDKSlim.Analytics
{
	public class EventNameObjectPair : TimedPoolObject<EventNameObjectPair>, IFactoryObject
	{
		public string OriginalName { get; private set; }

		public string ConvertedName { get; private set; }

		public object OriginalValue { get; private set; }

		public object ConvertedValue { get; private set; }

		private EventNameObjectPair()
		{
		}

		static EventNameObjectPair()
		{
		}

		IFactoryObject IFactoryObject.Instantiate()
		{
			return new EventNameObjectPair();
		}

		private void setOriginalGeneric(string i_OriginalName, object i_Value)
		{
			OriginalName = i_OriginalName; OriginalValue = i_Value;
		}

		private void setConvertedGeneric(string i_ConvertedName, object i_Value = null)
		{
			ConvertedName = i_ConvertedName; ConvertedValue = i_Value;
		}

		public void SetConverted(string i_ConvertedName)
		{
			setConvertedGeneric(i_ConvertedName);
		}

		public void SetConverted(string i_ConvertedName, string i_Value)
		{
			setConvertedGeneric(i_ConvertedName, i_Value);
		}

		public static EventNameObjectPair CreatePooled(string i_OriginalName, object i_Value)
		{
			return createPooledGeneric(i_OriginalName, i_Value);
		}

		private static EventNameObjectPair createPooledGeneric(string i_OriginalName, object i_Value)
		{
			EventNameObjectPair result = new EventNameObjectPair(); result.setOriginalGeneric(i_OriginalName, i_Value); return result;
		}
	}
}
