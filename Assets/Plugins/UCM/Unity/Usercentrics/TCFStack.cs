using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class TCFStack
	{
		public string description;

		public int id;

		public string name;

		public List<int> purposeIds;

		public List<int> specialFeatureIds;
	}
}
