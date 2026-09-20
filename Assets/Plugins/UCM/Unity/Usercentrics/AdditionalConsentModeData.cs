using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class AdditionalConsentModeData
	{
		public string acString;

		public List<AdTechProvider> adTechProviders;
	}
}
