using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Crosstales.Common.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Crosstales.OnlineCheck.Tool
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[HelpURL("https://www.crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_tool_1_1_ping_check.html")]
	public class PingCheck : Singleton<PingCheck>
	{
		public delegate void PingCompleted(string host, string ip, float time);

		[CompilerGenerated]
		private sealed class _003Cping_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public PingCheck _003C_003E4__this;

			public string hostname;

			private string _003Cip_003E5__2;

			private Ping _003Cping_003E5__3;

			private float _003Celapsed_003E5__4;

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
			public _003Cping_003Ed__41(int _003C_003E1__state)
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

		[FormerlySerializedAs("HostName")]
		[Header("General Settings")]
		[Tooltip("Hostname or IP for the Ping.")]
		[SerializeField]
		private string hostName;

		[FormerlySerializedAs("Timeout")]
		[Tooltip("Timeout for the Ping in seconds (default: 3).")]
		[Range(1f, 10f)]
		[SerializeField]
		private float timeout;

		[FormerlySerializedAs("RunOnStart")]
		[Header("Behaviour Settings")]
		[Tooltip("Start at runtime (default: false).")]
		[SerializeField]
		private bool runOnStart;

		private int lastPingTimeMs;

		[Header("Events")]
		public PingCompleteEvent OnPingComplete;

		public string HostName
		{
			get
			{
				return hostName;
			}
			set
			{
			}
		}

		public float Timeout
		{
			get
			{
				return timeout;
			}
			set
			{
			}
		}

		public bool RunOnStart
		{
			get
			{
				return runOnStart;
			}
			set
			{
			}
		}

		public string LastHost { get; private set; }

		public string LastIP { get; private set; }

		public float LastPingTime => 0f;

		public int LastPingTimeMilliseconds
		{
			get
			{
				return lastPingTimeMs;
			}
			private set
			{
			}
		}

		public bool isBusy { get; private set; }

		public bool isPlatformSupported => false;

		public event PingCompleted OnPingCompleted
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

		public void Ping()
		{
		}

		public void Ping(string hostname)
		{
		}

		[IteratorStateMachine(typeof(_003Cping_003Ed__41))]
		private IEnumerator ping(string hostname)
		{
			return null;
		}

		private void onPingCompleted(string host, string ip, float time)
		{
		}
	}
}
