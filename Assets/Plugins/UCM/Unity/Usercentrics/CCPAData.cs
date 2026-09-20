using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class CCPAData
	{
		public int version;

		public bool noticeGiven;

		public bool optedOut;

		public bool lspact;

		public string uspString;
	}
}
