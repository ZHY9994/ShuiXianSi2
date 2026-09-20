using System;
using AOT;
using UnityEngine;

namespace UCM
{
	public class ATTDialog : MonoBehaviour
	{
		private delegate void ATTCompletition(int status);

		private static Action<ATTManagerAuthorizationStatus> _onATTComplete;

		public static void CallATTrackingDialog(Action<ATTManagerAuthorizationStatus> onComplete)
		{
			_onATTComplete = onComplete;
			#if !UNITY_IOS || UNITY_EDITOR
			ATTCompleteCallback(0);
			#endif
		}

		public static void GetAttStatus(Action<ATTManagerAuthorizationStatus> onComplete)
		{
			if (onComplete != null) onComplete(ATTManagerAuthorizationStatus.NotDetermined);
		}

		public static void OpenSettings()
		{
			Application.OpenURL("app-settings:");
		}

		[MonoPInvokeCallback(typeof(ATTCompletition))]
		private static void ATTCompleteCallback(int status)
		{
			Action<ATTManagerAuthorizationStatus> callback = _onATTComplete;
			_onATTComplete = null;
			if (callback != null) callback((ATTManagerAuthorizationStatus)Mathf.Clamp(status, 0, 3));
		}
	}
}
