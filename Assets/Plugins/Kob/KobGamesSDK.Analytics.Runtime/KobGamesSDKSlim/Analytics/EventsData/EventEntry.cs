namespace KobGamesSDKSlim.Analytics.EventsData
{
	public struct EventEntry
	{
		public readonly string Name;

		private string m_Value;

		public EventEntry(string i_Name, string i_Value)
		{
			Name = i_Name;
			m_Value = i_Value;
		}
	}
}
