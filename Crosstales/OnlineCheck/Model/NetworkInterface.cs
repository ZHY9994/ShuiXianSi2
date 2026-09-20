using System;
using System.Net;
using System.Net.NetworkInformation;

namespace Crosstales.OnlineCheck.Model
{
	[Serializable]
	public class NetworkInterface
	{
		public readonly string Id;

		public readonly string Name;

		public readonly NetworkInterfaceType Type;

		public readonly IPAddress Address;

		public readonly IPAddress Mask;

		public readonly string MacAddress;

		public readonly IPAddress Gateway;

		public readonly long Speed;

		public readonly OperationalStatus Status;

		public NetworkInterface(string id, string name, NetworkInterfaceType type, IPAddress address, IPAddress mask, string macAddress, IPAddress gateway, long speed, OperationalStatus status)
		{
		}

		public override string ToString()
		{
			return string.Concat(Name, " (", Address, ")");
		}
	}
}
