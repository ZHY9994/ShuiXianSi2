using System;
using UnityEngine;

namespace UCM
{
	public class SettingsCMPMainThreadCallbackDispatcher : MonoBehaviour
	{
		private bool _readyToCallback;

		private bool _callbackResult;

		private Action<bool> _wasShownSuccessfullyCallback;

		private void LateUpdate()
		{
			if (!_readyToCallback) return;
			_wasShownSuccessfullyCallback?.Invoke(_callbackResult);
			Destroy(gameObject);
		}

		public void SetCallback(Action<bool> wasShownSuccessfullyCallback)
		{
			_wasShownSuccessfullyCallback = wasShownSuccessfullyCallback;
		}

		public void SetReadyToCallback(bool callbackResult)
		{
			_callbackResult = callbackResult;
			_readyToCallback = true;
		}
	}
}
