using UnityEngine;

namespace Crosstales.OnlineCheck.Tool
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[HelpURL("https://crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_tool_1_1_proxy.html")]
	public class Proxy : MonoBehaviour
	{
		[Header("HTTP Proxy Settings")]
		[Tooltip("URL (without protocol) or IP of the proxy server.")]
		public string HTTPProxyURL;

		[Tooltip("Port of the proxy server.")]
		[Range(0f, 65535f)]
		public int HTTPProxyPort;

		[Tooltip("Username for the proxy server (optional).")]
		public string HTTPProxyUsername;

		[Tooltip("Password for the proxy server (optional).")]
		public string HTTPProxyPassword;

		[Tooltip("Protocol (e.g. 'http://') for the proxy server (optional).")]
		public string HTTPProxyURLProtocol;

		[Header("HTTPS Proxy Settings")]
		[Tooltip("URL (without protocol) or IP of the proxy server.")]
		public string HTTPSProxyURL;

		[Tooltip("Port of the proxy server.")]
		[Range(0f, 65535f)]
		public int HTTPSProxyPort;

		[Tooltip("Username for the proxy server (optional).")]
		public string HTTPSProxyUsername;

		[Tooltip("Password for the proxy server (optional).")]
		public string HTTPSProxyPassword;

		[Tooltip("Protocol (e.g. 'http://') for the proxy server (optional).")]
		public string HTTPSProxyURLProtocol;

		[Header("Startup Behaviour")]
		[Tooltip("Enable the proxy on awake (default: false).")]
		public bool EnableOnAwake;

		private const string HTTPProxyEnvVar = "HTTP_PROXY";

		private const string HTTPSProxyEnvVar = "HTTPS_PROXY";

		public static bool hasHTTPProxy { get; private set; }

		public static bool hasHTTPSProxy { get; private set; }

		static Proxy()
		{
		}

		private void Awake()
		{
		}

		private void Update()
		{
		}

		public void EnableHTTPProxy()
		{
		}

		public void EnableHTTPSProxy()
		{
		}

		public static void EnableHTTPProxy(string url, int port, string username = "", string password = "", string urlProtocol = "")
		{
		}

		public static void EnableHTTPSProxy(string url, int port, string username = "", string password = "", string urlProtocol = "")
		{
		}

		public static void DisableHTTPProxy()
		{
		}

		public static void DisableHTTPSProxy()
		{
		}

		private static bool validPort(int port)
		{
			return port >= 0 && port <= 65535;
		}
	}
}
