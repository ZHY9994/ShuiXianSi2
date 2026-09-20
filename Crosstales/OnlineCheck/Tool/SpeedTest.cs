using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Crosstales.Common.Util;
using Crosstales.OnlineCheck.Model.Enum;
using UnityEngine;
using UnityEngine.Serialization;

namespace Crosstales.OnlineCheck.Tool
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[HelpURL("https://www.crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_tool_1_1_speed_test.html")]
	public class SpeedTest : Singleton<SpeedTest>
	{
		public delegate void TestCompleted(string url, long dataSize, double duration, double speed);

		[CompilerGenerated]
		private sealed class _003Ctest_003Ed__57 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public SpeedTest _003C_003E4__this;

			public TestSize size;

			public string url;

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
			public _003Ctest_003Ed__57(int _003C_003E1__state)
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

		[FormerlySerializedAs("DataSize")]
		[Header("General Settings")]
		[Tooltip("Data size for the speed test (default: SMALL).")]
		[SerializeField]
		private TestSize dataSize;

		[FormerlySerializedAs("SmallURL")]
		[Tooltip("URL for the small data speed test (default: https://dl.google.com/googletalk/googletalk-setup.exe (1.5MB)).")]
		[SerializeField]
		private string smallURL;

		[FormerlySerializedAs("MediumURL")]
		[Tooltip("URL for the medium data speed test (default: https://dl.google.com/dl/earth/client/advanced/current/googleearthprowin-7.3.2-x64.exe (62MB)).")]
		[SerializeField]
		private string mediumURL;

		[FormerlySerializedAs("LargeURL")]
		[Tooltip("URL for the large data speed test (default: https://dl.google.com/drive-file-stream/GoogleDriveFSSetup.exe (195MB)).")]
		[SerializeField]
		private string largeURL;

		[FormerlySerializedAs("RunOnStart")]
		[Header("Behaviour Settings")]
		[Tooltip("Start at runtime (default: false).")]
		[SerializeField]
		private bool runOnStart;

		private Thread worker;

		[Header("Events")]
		public SpeedTestCompleteEvent OnSpeedTestComplete;

		public TestSize DataSize
		{
			get
			{
				return dataSize;
			}
			set
			{
			}
		}

		public string SmallUrl
		{
			get
			{
				return smallURL;
			}
			set
			{
			}
		}

		public string MediumUrl
		{
			get
			{
				return mediumURL;
			}
			set
			{
			}
		}

		public string LargeUrl
		{
			get
		{
				return largeURL;
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

		public string LastURL { get; private set; }

		public long LastDataSize { get; private set; }

		public double LastDataSizeMB => 0.0;

		public double LastDuration { get; private set; }

		public double LastSpeed { get; private set; }

		public double LastSpeedMBps => 0.0;

		public bool isBusy { get; private set; }

		public bool isPlatformSupported => false;

		public event TestCompleted OnTestCompleted
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

		protected override void OnApplicationQuit()
		{
		}

		private void Start()
		{
		}

		public void Test()
		{
		}

		public void Test(TestSize size)
		{
		}

		public void Test(string url)
		{
		}

		[IteratorStateMachine(typeof(_003Ctest_003Ed__57))]
		private IEnumerator test(TestSize size, string url)
		{
			return null;
		}

		private void speedTest(TestSize size, string url)
		{
		}

		private void onTestCompleted(string url, long filesize, double duration, double speed)
		{
		}
	}
}
