using System.Collections.Generic;
using Crosstales.OnlineCheck.Model;

namespace Crosstales.OnlineCheck.Util
{
	public static class NetworkInfo
	{
		private static List<NetworkInterface> interfaceCache;

		private static string publicIpCache;

		public static string PublicIP => null;

		public static List<NetworkInterface> LastNetworkInterfaces => null;

		public static string LastPublicIP => null;

		public static bool isPlatformSupported => false;

		public static void Refresh()
		{
		}

		public static List<NetworkInterface> getNetworkInterfaces(bool activeOnly = true)
		{
			return new List<NetworkInterface>();
		}
	}
}
