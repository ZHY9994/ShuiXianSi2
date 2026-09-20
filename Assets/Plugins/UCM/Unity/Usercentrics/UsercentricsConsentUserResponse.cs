using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class UsercentricsConsentUserResponse
	{
		public UsercentricsUserInteraction userInteraction;

		public List<UsercentricsServiceConsent> consents;

		public string controllerId;
	}
}
