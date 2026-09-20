using KobGamesSDKSlim.Analytics.EventsData;

namespace KobGamesSDKSlim.Analytics.Examples
{
	public class EventsNamesPerProjectExample : EventsNamesDefault
	{
		public static readonly EventEntry k_Name_Deck;

		public static readonly EventEntry k_Type_Deck_Weapons;

		static EventsNamesPerProjectExample()
		{
			k_Name_Deck = new EventEntry("k_Name_Deck", "level_deck");
			k_Type_Deck_Weapons = new EventEntry("k_Type_Deck_Weapons", "deck_weapons");
		}

		public EventsNamesPerProjectExample()
			: base()
		{
		}
	}
}
