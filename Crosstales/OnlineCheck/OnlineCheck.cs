using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Crosstales.Common.Util;
using Crosstales.OnlineCheck.Data;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace Crosstales.OnlineCheck
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[HelpURL("https://crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_online_check.html")]
	public class OnlineCheck : Singleton<OnlineCheck>
	{
		public delegate void OnlineStatusChange(bool isConnected);

		public delegate void NetworkReachabilityChange(NetworkReachability networkReachability);

		public delegate void OnlineCheckComplete(bool isConnected, NetworkReachability networkReachability);

		[CompilerGenerated]
		private sealed class _003CRefreshYield_003Ed__129 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public OnlineCheck _003C_003E4__this;

			public bool triggerCallbacks;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get { return _003C_003E2__current; }
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get { return _003C_003E2__current; }
			}

			[DebuggerHidden]
			public _003CRefreshYield_003Ed__129(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003Cgoogle204Check_003Ed__132 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public OnlineCheck _003C_003E4__this;

			public bool showError;

			private UnityWebRequest _003Cwww_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003Cgoogle204Check_003Ed__132(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CgoogleBlankCheck_003Ed__133 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public OnlineCheck _003C_003E4__this;

			public bool showError;

			private UnityWebRequest _003Cwww_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CgoogleBlankCheck_003Ed__133(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CinternetCheck_003Ed__136 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public OnlineCheck _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CinternetCheck_003Ed__136(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CstartWorker_003Ed__135 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public OnlineCheck _003C_003E4__this;

			public string url;

			public string data;

			public bool equals;

			public string type;

			public int header;

			public bool showError;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CstartWorker_003Ed__135(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CwwwCheck_003Ed__131 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public OnlineCheck _003C_003E4__this;

			public string url;

			public bool equals;

			public string data;

			public int header;

			public bool showError;

			private UnityWebRequest _003Cwww_003E5__2;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return _003C_003E2__current;
				}
			}

			[DebuggerHidden]
			public _003CwwwCheck_003Ed__131(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return MoveNext();
			}

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[FormerlySerializedAs("EndlessMode")]
		[Header("General Settings")]
		[Tooltip("Continuously check for Internet availability within given intervals (default: true).")]
		[SerializeField]
		private bool endlessMode;

		[FormerlySerializedAs("IntervalMin")]
		[Tooltip("Minimum delay between checks in seconds (default: 8, range: 3 - 59).")]
		[Range(3f, 59f)]
		[SerializeField]
		private int intervalMin;

		[FormerlySerializedAs("IntervalMax")]
		[Tooltip("Maximum delay between checks in seconds (default: 12, range: 4 - 60).")]
		[Range(4f, 60f)]
		[SerializeField]
		private int intervalMax;

		[FormerlySerializedAs("Timeout")]
		[Tooltip("Timeout for every check in seconds (default: 2, range: 1 - 10).")]
		[Range(1f, 10f)]
		[SerializeField]
		private int timeout;

		[FormerlySerializedAs("ForceWWW")]
		[Tooltip("Force UnityWebRequest instead of WebClient (default: false).")]
		[SerializeField]
		private bool forceWWW;

		[FormerlySerializedAs("CustomCheck")]
		[Tooltip("Use a custom configuration for the checks.")]
		[SerializeField]
		[ContextMenuItem("Create CustomCheck", "createCustomCheck")]
		private CustomCheck customCheck;

		[Header("Active Checks")]
		[Tooltip("Enable or disable the 'Microsoft NCSI' check (default: true, 184 Bytes).")]
		[SerializeField]
		private bool microsoft;

		[Tooltip("Enable or disable the 'Google 204' check (default: true, 279 Bytes).")]
		[SerializeField]
		private bool google204;

		[Tooltip("Enable or disable the 'Google Blank' check (default: true, 831 Bytes).")]
		[SerializeField]
		private bool googleBlank;

		[Tooltip("Enable or disable the 'Apple' check (default: true, 503 Bytes).")]
		[SerializeField]
		private bool apple;

		[Tooltip("Enable or disable the 'Ubuntu' check (default: true, 1001 Bytes).")]
		[SerializeField]
		private bool ubuntu;

		[FormerlySerializedAs("RunOnStart")]
		[Header("Behaviour Settings")]
		[Tooltip("Start at runtime (default: true).")]
		[SerializeField]
		private bool runOnStart;

		[FormerlySerializedAs("Delay")]
		[Tooltip("Delay in seconds until the OnlineCheck starts checking (default: 0, range: 0 - 120).")]
		[Range(0f, 120f)]
		[SerializeField]
		private float delay;

		private bool isFirsttime;

		private bool lastInternetAvailable;

		private NetworkReachability lastNetworkReachability;

		private bool isRunning;

		private float checkTime;

		private float checkTimeCounter;

		private float burstTime;

		private float burstTimeCounter;

		private bool available;

		private NetworkReachability networkReachability;

		private float lastCheckTime;

		private const float burstIntervalMin = 2f;

		private const float burstIntervalMax = 5f;

		private const string microsoftUrl = "http://www.msftncsi.com/ncsi.txt";

		private const string appleUrl = "https://www.apple.com/library/test/success.html";

		private const string ubuntuUrl = "https://start.ubuntu.com/connectivity-check";

		private const string fallbackUrl = "https://crosstales.com/media/downloads/up.txt";

		private const int google204HeaderSize = 279;

		private const int googleBlankHeaderSize = 831;

		private const int microsoftHeaderSize = 170;

		private const int appleHeaderSize = 376;

		private const int ubuntuHeaderSize = 425;

		private const int fallbackHeaderSize = 349;

		private const string microsoftText = "Microsoft NCSI";

		private const string appleText = "<TITLE>Success</TITLE>";

		private const string ubuntuText = "<TITLE>Lorem Ipsum</TITLE>";

		private const string fallbackText = "crosstales rulez!";

		private const string windowsDesc = "Microsoft";

		private const string appleDesc = "Apple";

		private const string ubuntuDesc = "Ubuntu";

		private const string fallbackDesc = "Fallback (crosstales)";

		private const string customDesc = "custom URL";

		private const string testingDesc = "Testing the Internet availability with:";

		private const bool microsoftEquals = true;

		private const bool appleEquals = false;

		private const bool ubuntuEquals = false;

		private const bool fallbackEquals = true;

		private Thread worker;

		private bool existsCustomCheck;

		[Header("Events")]
		public StatusChangeEvent OnStatusChange;

		public bool EndlessMode
		{
			get { return endlessMode; }
			set
			{
			}
		}

		public int IntervalMin
		{
			get { return intervalMin; }
			set
			{
			}
		}

		public int IntervalMax
		{
			get { return intervalMax; }
			set
			{
			}
		}

		public int Timeout
		{
			get { return timeout; }
			set
			{
			}
		}

		public bool ForceWWW
		{
			get { return forceWWW; }
			set
			{
			}
		}

		public CustomCheck CustomCheck
		{
			get { return customCheck; }
			set
			{
			}
		}

		public bool Google204
		{
			get { return google204; }
			set
			{
			}
		}

		public bool GoogleBlank
		{
			get { return googleBlank; }
			set
			{
			}
		}

		public bool Microsoft
		{
			get { return microsoft; }
			set
			{
			}
		}

		public bool Apple
		{
			get { return apple; }
			set
			{
			}
		}

		public bool Ubuntu
		{
			get { return ubuntu; }
			set
			{
			}
		}

		public bool RunOnStart
		{
			get { return runOnStart; }
			set
			{
			}
		}

		public float Delay
		{
			get { return delay; }
			set
			{
			}
		}

		public bool isInternetAvailable { get; private set; }

		public NetworkReachability NetworkReachability => NetworkReachability.NotReachable;

		public string NetworkReachabilityShort => null;

		public DateTime LastCheck { get; private set; }

		public long DataDownloaded { get; private set; }

		public bool isBusy => false;

		public event OnlineStatusChange OnOnlineStatusChange
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event NetworkReachabilityChange OnNetworkReachabilityChange
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		public event OnlineCheckComplete OnOnlineCheckComplete
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		protected override void Awake()
		{
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		protected override void OnApplicationQuit()
		{
		}

		private void OnValidate()
		{
		}

		public static void ResetObject()
		{
		}

		public void Refresh(bool triggerCallbacks = false)
		{
		}

		[IteratorStateMachine(typeof(_003CRefreshYield_003Ed__129))]
		public IEnumerator RefreshYield(bool triggerCallbacks = false)
		{
			return null;
		}

		private void run()
		{
		}

		[IteratorStateMachine(typeof(_003CwwwCheck_003Ed__131))]
		private IEnumerator wwwCheck(string url, string data, bool equals, string type, int header, bool showError = false)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003Cgoogle204Check_003Ed__132))]
		private IEnumerator google204Check(bool showError = false)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CgoogleBlankCheck_003Ed__133))]
		private IEnumerator googleBlankCheck(bool showError = false)
		{
			return null;
		}

		private void threadCheck(out bool _available, string url, string data, bool equals, string type, int header, bool showError = false)
		{
			_available = false;
		}

		[IteratorStateMachine(typeof(_003CstartWorker_003Ed__135))]
		private IEnumerator startWorker(string url, string data, bool equals, string type, int header, bool showError = false)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CinternetCheck_003Ed__136))]
		private IEnumerator internetCheck()
		{
			return null;
		}

		private static string URLAntiCacheRandomizer(string url)
		{
			return url;
		}

		private void onInternetStatusChange(bool isConnected)
		{
		}

		private void onNetworkReachabilityChange(NetworkReachability _networkReachability)
		{
		}

		private void onInternetCheckComplete(bool isConnected, NetworkReachability _networkReachability)
		{
		}
	}
}
