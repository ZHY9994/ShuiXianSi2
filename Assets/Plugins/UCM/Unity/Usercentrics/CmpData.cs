using System;
using System.Collections.Generic;

namespace Unity.Usercentrics
{
	[Serializable]
	public class CmpData
	{
		public UsercentricsVariant activeVariant;

		public List<PublishedApp> publishedApps;

		public UsercentricsLocation userLocation;
	}
}
