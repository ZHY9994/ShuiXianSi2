using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class UsercentricsUpdatedConsentEvent
	{
		public List<UsercentricsServiceConsent> consents;

		public string controllerId;

		public string tcString;

		public string uspString;

		public string acString;
	}
}
