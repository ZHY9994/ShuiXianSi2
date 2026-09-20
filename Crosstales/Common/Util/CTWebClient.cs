using System;
using System.Net;

namespace Crosstales.Common.Util
{
	public class CTWebClient : WebClient
	{
		public int Timeout { get; set; }

		public int ConnectionLimit { get; set; }

		public CTWebClient()
		{
		}

		public CTWebClient(int timeout, int connectionLimit = 20)
		{
		}

		public WebRequest CTGetWebRequest(string uri)
		{
			return GetWebRequest(new Uri(uri));
		}

		protected override WebRequest GetWebRequest(Uri uri)
		{
			return base.GetWebRequest(uri);
		}
	}
}
