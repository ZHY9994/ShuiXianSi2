using System;
using System.Net;
using Crosstales.Common.Util;

namespace Crosstales.OnlineCheck.Util
{
	public class CTWebClientNotCached : CTWebClient
	{
		public CTWebClientNotCached()
		{
		}

		public CTWebClientNotCached(int timeout, int connectionLimit = 20)
		{
		}

		protected override WebRequest GetWebRequest(Uri uri)
		{
			return base.GetWebRequest(uri);
		}
	}
}
