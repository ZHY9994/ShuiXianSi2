using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class UsercentricsReadyStatus
	{
		public bool shouldCollectConsent;

		public List<UsercentricsServiceConsent> consents;

		public GeolocationRuleset geolocationRuleset;

		public UsercentricsLocation location;
	}
}
