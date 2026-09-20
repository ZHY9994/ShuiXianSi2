using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class TCFPurpose
	{
		public string purposeDescription;

		public string descriptionLegal;

		public int id;

		public string name;

		public bool isPartOfASelectedStack;

		public bool showConsentToggle;

		public bool showLegitimateInterestToggle;

		public string _consent;

		public string _legitimateInterestConsent;

		public string _stackId;

		public bool? consent => StringParser.ToBool(_consent);

		public bool? legitimateInterestConsent => StringParser.ToBool(_legitimateInterestConsent);

		public int? stackId => StringParser.ToInt(_stackId);
	}
}
