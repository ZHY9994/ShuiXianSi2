using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class UsercentricsServiceConsent
	{
		public string templateId;

		public bool status;

		public List<UsercentricsConsentHistoryEntry> history;

		public string dataProcessor;

		public string version;

		public bool isEssential;

		public string _type;

		public UsercentricsConsentType? type
		{
			get
			{
				int? value = StringParser.ToInt(_type);
				return value.HasValue && Enum.IsDefined(typeof(UsercentricsConsentType), value.Value) ? (UsercentricsConsentType?)value.Value : null;
			}
		}
	}
}
