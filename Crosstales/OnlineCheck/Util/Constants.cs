using System;
using Crosstales.Common.Util;

namespace Crosstales.OnlineCheck.Util
{
	public abstract class Constants : BaseConstants
	{
		public const string ASSET_NAME = "Online Check PRO";

		public const string ASSET_NAME_SHORT = "OC PRO";

		public const string ASSET_VERSION = "2021.2.7";

		public const int ASSET_BUILD = 20211230;

		public static readonly DateTime ASSET_CREATED;

		public static readonly DateTime ASSET_CHANGED;

		public const string ASSET_PRO_URL = "https://assetstore.unity.com/packages/slug/74688?aid=1011lNGT";

		public const string ASSET_UPDATE_CHECK_URL = "https://www.crosstales.com/media/assets/onlinecheck_versions.txt";

		public const string ASSET_CONTACT = "onlinecheck@crosstales.com";

		public const string ASSET_MANUAL_URL = "https://www.crosstales.com/media/data/assets/OnlineCheck/OnlineCheck-doc.pdf";

		public const string ASSET_API_URL = "https://crosstales.com/media/data/assets/OnlineCheck/api";

		public const string ASSET_FORUM_URL = "https://forum.unity.com/threads/online-check-pro-verify-internet-reachability.472558/";

		public const string ASSET_WEB_URL = "https://www.crosstales.com/en/portfolio/OnlineCheck/";

		public const string ASSET_VIDEO_PROMO = "https://youtu.be/pPvKE-eyxV4?list=PLgtonIOr6Tb41XTMeeZ836tjHlKgOO84S";

		public const string ASSET_VIDEO_TUTORIAL = "https://youtu.be/bNdafUNcs68?list=PLgtonIOr6Tb41XTMeeZ836tjHlKgOO84S";

		public const string KEY_PREFIX = "ONLINECHECK_CFG_";

		public const string KEY_DEBUG = "ONLINECHECK_CFG_DEBUG";

		public const string ONLINECHECK_SCENE_OBJECT_NAME = "OnlineCheck";

		public const string PROXY_SCENE_OBJECT_NAME = "Proxy";

		public const string PINGCHECK_SCENE_OBJECT_NAME = "PingCheck";

		public const string SPEEDTEST_SCENE_OBJECT_NAME = "SpeedTest";

		public const string SPEEDTESTNET_SCENE_OBJECT_NAME = "SpeedTestNET";

		public const string TAB = "\t\t";
	}
}
