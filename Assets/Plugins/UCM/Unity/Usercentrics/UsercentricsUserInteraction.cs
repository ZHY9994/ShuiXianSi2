using System;

namespace Unity.Usercentrics
{
	[Serializable]
	public enum UsercentricsUserInteraction
	{
		AcceptAll = 0,
		DenyAll = 1,
		Granular = 2,
		NoInteraction = 3
	}
}
