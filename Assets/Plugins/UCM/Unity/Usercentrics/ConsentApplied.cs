using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class ConsentApplied
	{
		public string name;

		public string templateId;

		public bool consent;

		public bool mediated;
	}
}
