using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public class UsercentricsOptionsInternal
	{
		public string settingsId;

		public string ruleSetId;

		public string defaultLanguage;

		public string version;

		public long timeoutMillis;

		public UsercentricsLoggerLevel loggerLevel;

		public UsercentricsNetworkMode networkMode;

		public bool consentMediation;

		public long initTimeoutMillis;

		public static UsercentricsOptionsInternal CreateFrom(UsercentricsOptions unityOptions, string settingsId, string ruleSetId)
		{
			return new UsercentricsOptionsInternal
			{
				settingsId = settingsId,
				ruleSetId = ruleSetId,
				defaultLanguage = unityOptions != null ? unityOptions.DefaultLanguage : null,
				version = unityOptions != null ? unityOptions.Version : null,
				timeoutMillis = unityOptions != null ? unityOptions.TimeoutMillis : 0L,
				loggerLevel = unityOptions != null && unityOptions.DebugMode ? UsercentricsLoggerLevel.Debug : UsercentricsLoggerLevel.None,
				networkMode = unityOptions != null ? unityOptions.NetworkMode : UsercentricsNetworkMode.World,
				consentMediation = unityOptions != null && unityOptions.ConsentMediation,
				initTimeoutMillis = unityOptions != null ? unityOptions.InitTimeoutMillis : 0L
			};
		}
	}
}
