using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class UsercentricsOptions
	{
		public string DefaultLanguage;

		public string Version;

		public bool DebugMode;

		public long TimeoutMillis;

		public UsercentricsNetworkMode NetworkMode;

		public bool ConsentMediation;

		public long InitTimeoutMillis;

		public AndroidOptions Android;
	}
}
