using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class TCFSpecialFeature
	{
		public string purposeDescription;

		public string descriptionLegal;

		public int id;

		public string name;

		public bool isPartOfASelectedStack;

		public bool showConsentToggle;

		public string _consent;

		public string _stackId;

		public bool? consent => StringParser.ToBool(_consent);

		public int? stackId => StringParser.ToInt(_stackId);
	}
}
