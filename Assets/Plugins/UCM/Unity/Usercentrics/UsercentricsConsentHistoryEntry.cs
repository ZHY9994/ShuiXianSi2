using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class UsercentricsConsentHistoryEntry
	{
		public bool status;

		public UsercentricsConsentType type;

		public long timestampInMillis;
	}
}
