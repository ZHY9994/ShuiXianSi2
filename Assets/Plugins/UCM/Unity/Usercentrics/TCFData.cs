using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class TCFData
	{
		public List<TCFFeature> features;

		public List<TCFPurpose> purposes;

		public List<TCFSpecialFeature> specialFeatures;

		public List<TCFSpecialPurpose> specialPurposes;

		public List<TCFStack> stacks;

		public List<TCFVendor> vendors;

		public string tcString;
	}
}
