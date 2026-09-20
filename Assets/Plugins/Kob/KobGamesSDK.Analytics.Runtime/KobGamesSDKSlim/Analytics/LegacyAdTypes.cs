namespace KobGamesSDKSlim
{
	public enum eRewardVideoCallResult
	{
		Success = 0,
		AdsMediationNotInitialized = 1,
		AdsMediationIsDisabledByRemoteSettings = 2,
		RewardVideoIsDisabledByRemoteSettings = 3,
		RewardVideoIsNotAvailable = 4,
		OpenedRVConfirmationScreen = 5
	}
}

namespace KobGamesSDKSlim.Ads
{
	public static class AdsConstants
	{
		public const string k_None = "None";
	}

	public static class eBannerPlacements
	{
		public const string k_Main = "main";
	}

	public enum eAdsAvailabilityResult
	{
		Success = 0,
		Not_Available = 1,
		Waited = 2
	}

	public enum eVideoAdsSuccessResult
	{
		Watched = 0,
		Clicked = 1
	}

	public class IAdNetworkError
	{
		private readonly string m_Description;
		private readonly int m_Code;

		public IAdNetworkError(string i_ErrorCode, string i_ErrorDescription)
		{
			int.TryParse(i_ErrorCode, out m_Code);
			m_Description = i_ErrorDescription;
		}

		public IAdNetworkError(int i_ErrorCode, string i_ErrorDescription)
		{
			m_Code = i_ErrorCode;
			m_Description = i_ErrorDescription;
		}

		public int getErrorCode()
		{
			return m_Code;
		}

		public string getDescription()
		{
			return m_Description ?? string.Empty;
		}

		public int getCode()
		{
			return m_Code;
		}

		public override string ToString()
		{
			return m_Code + " : " + m_Description;
		}
	}

	public class BannerEvents
	{
		public delegate void BannerDestroyed(string i_Placement);
		public delegate void BannerShown(string i_Placement);
		public delegate void BannerHidden(string i_Placement);
		public delegate void BannerFailed(string i_Placement, IAdNetworkError i_AdNetworkError);

		public string PlacementId;

		public event BannerDestroyed DestroyedEvent;
		public event BannerShown ShownEvent;
		public event BannerHidden HiddenEvent;
		public event BannerFailed FailedEvent;

		public void OnDestroyed()
		{
			DestroyedEvent?.Invoke(PlacementId);
		}

		public void OnShown()
		{
			ShownEvent?.Invoke(PlacementId);
		}

		public void OnHidden()
		{
			HiddenEvent?.Invoke(PlacementId);
		}

		public void OnFailed(IAdNetworkError i_AdNetworkError)
		{
			FailedEvent?.Invoke(PlacementId, i_AdNetworkError);
		}

		public void ResetCallbacks(BannerEvents i_GlobalEvents, BannerShown i_ShownEvent = null, BannerHidden i_HiddenEvent = null, BannerFailed i_FailedEvent = null, BannerDestroyed i_BannerDestroyed = null, string i_PlacementId = "None")
		{
			PlacementId = i_PlacementId;
			InjectCallBacks(i_GlobalEvents, false);
			ShownEvent += i_ShownEvent;
			HiddenEvent += i_HiddenEvent;
			FailedEvent += i_FailedEvent;
			DestroyedEvent += i_BannerDestroyed;
		}

		public void InjectCallBacks(BannerEvents i_BannerEvents, bool i_Append = true)
		{
			if (i_BannerEvents == null)
			{
				if (!i_Append)
				{
					DestroyedEvent = null;
					ShownEvent = null;
					HiddenEvent = null;
					FailedEvent = null;
				}
				return;
			}

			if (i_Append)
			{
				ShownEvent += i_BannerEvents.ShownEvent;
				HiddenEvent += i_BannerEvents.HiddenEvent;
				FailedEvent += i_BannerEvents.FailedEvent;
				DestroyedEvent += i_BannerEvents.DestroyedEvent;
			}
			else
			{
				ShownEvent = i_BannerEvents.ShownEvent;
				HiddenEvent = i_BannerEvents.HiddenEvent;
				FailedEvent = i_BannerEvents.FailedEvent;
				DestroyedEvent = i_BannerEvents.DestroyedEvent;
			}
		}
	}

	public class InterstitialEvents
	{
		public delegate void InterstitialAvailability(string i_Placement, eAdsAvailabilityResult i_Result);
		public delegate void InterstitialOpen(string i_Placement);
		public delegate void InterstitialClose(string i_Placement);
		public delegate void InterstitialShown(eVideoAdsSuccessResult i_Result, string i_Placement);
		public delegate void InterstitialShownFail(string i_Placement, IAdNetworkError i_AdNetworkError);

		public string PlacementId;

		public event InterstitialAvailability AvailabilityEvent;
		public event InterstitialOpen OpenEvent;
		public event InterstitialClose CloseEvent;
		public event InterstitialShown ShownEvent;
		public event InterstitialShownFail ShownFailEvent;

		public void OnAvailability(string i_PlacementID, eAdsAvailabilityResult i_AvailabilityResult)
		{
			PlacementId = i_PlacementID;
			AvailabilityEvent?.Invoke(i_PlacementID, i_AvailabilityResult);
		}

		public void OnOpened()
		{
			OpenEvent?.Invoke(PlacementId);
		}

		public void OnClosed()
		{
			CloseEvent?.Invoke(PlacementId);
		}

		public void OnShownSuccess(eVideoAdsSuccessResult i_Result)
		{
			ShownEvent?.Invoke(i_Result, PlacementId);
		}

		public void OnShownFailed(IAdNetworkError i_AdNetworkError)
		{
			ShownFailEvent?.Invoke(PlacementId, i_AdNetworkError);
		}

		public void ResetCallbacks(InterstitialEvents i_GlobalEvents, InterstitialShown i_InterstitialShown = null, InterstitialOpen i_InterstitialOpen = null, InterstitialClose i_InterstitialClose = null, InterstitialShownFail i_InterstitialShownFail = null, string i_PlacementId = "None")
		{
			PlacementId = i_PlacementId;
			InjectCallBacks(i_GlobalEvents, false);
			ShownEvent += i_InterstitialShown;
			OpenEvent += i_InterstitialOpen;
			CloseEvent += i_InterstitialClose;
			ShownFailEvent += i_InterstitialShownFail;
		}

		public void InjectCallBacks(InterstitialEvents i_InterstitialEventsCallbacks, bool i_Append = true)
		{
			if (i_InterstitialEventsCallbacks == null)
			{
				if (!i_Append)
				{
					AvailabilityEvent = null;
					OpenEvent = null;
					CloseEvent = null;
					ShownEvent = null;
					ShownFailEvent = null;
				}
				return;
			}

			if (i_Append)
			{
				AvailabilityEvent += i_InterstitialEventsCallbacks.AvailabilityEvent;
				OpenEvent += i_InterstitialEventsCallbacks.OpenEvent;
				CloseEvent += i_InterstitialEventsCallbacks.CloseEvent;
				ShownEvent += i_InterstitialEventsCallbacks.ShownEvent;
				ShownFailEvent += i_InterstitialEventsCallbacks.ShownFailEvent;
			}
			else
			{
				AvailabilityEvent = i_InterstitialEventsCallbacks.AvailabilityEvent;
				OpenEvent = i_InterstitialEventsCallbacks.OpenEvent;
				CloseEvent = i_InterstitialEventsCallbacks.CloseEvent;
				ShownEvent = i_InterstitialEventsCallbacks.ShownEvent;
				ShownFailEvent = i_InterstitialEventsCallbacks.ShownFailEvent;
			}
		}
	}

	public class RewardVideoEvents
	{
		public delegate void RewardVideoAvailability(string i_Placement, eAdsAvailabilityResult i_Result);
		public delegate void RewardVideoOpen(string i_Placement);
		public delegate void RewardVideoClose(string i_Placement, bool i_IsRVSuccess);
		public delegate void RewardVideoSuccess(eVideoAdsSuccessResult i_Result, string i_Placement);
		public delegate void RewardVideoFail(string i_Placement, IAdNetworkError i_AdNetworkError);

		public string PlacementId;

		public event RewardVideoAvailability AvailabilityEvent;
		public event RewardVideoOpen OpenEvent;
		public event RewardVideoOpen OpenedEvent;
		public event RewardVideoClose CloseEvent;
		public event RewardVideoSuccess SuccessEvent;
		public event RewardVideoFail FailEvent;

		public void OnAvailability(string i_PlacementID, eAdsAvailabilityResult i_AvailabilityResult)
		{
			PlacementId = i_PlacementID;
			AvailabilityEvent?.Invoke(i_PlacementID, i_AvailabilityResult);
		}

		public void OnOpen()
		{
			OpenEvent?.Invoke(PlacementId);
		}

		public void OnOpened()
		{
			OpenedEvent?.Invoke(PlacementId);
		}

		public void OnClosed(bool i_IsRVSuccess)
		{
			CloseEvent?.Invoke(PlacementId, i_IsRVSuccess);
		}

		public void OnSuccess(eVideoAdsSuccessResult i_Result)
		{
			SuccessEvent?.Invoke(i_Result, PlacementId);
		}

		public void OnFailed(IAdNetworkError i_AdNetworkError)
		{
			FailEvent?.Invoke(PlacementId, i_AdNetworkError);
		}

		public void ResetCallbacks(RewardVideoEvents i_GlobalEvents, RewardVideoSuccess i_RewardVideoSuccess = null, RewardVideoOpen i_RewardVideoOpened = null, RewardVideoClose i_RewardVideoClose = null, RewardVideoFail i_RewardVideoFail = null, string i_PlacementId = "None", RewardVideoOpen i_RewardVideoOpen = null)
		{
			PlacementId = i_PlacementId;
			InjectCallBacks(i_GlobalEvents, false);
			SuccessEvent += i_RewardVideoSuccess;
			OpenedEvent += i_RewardVideoOpened;
			CloseEvent += i_RewardVideoClose;
			FailEvent += i_RewardVideoFail;
			OpenEvent += i_RewardVideoOpen;
		}

		public void InjectCallBacks(RewardVideoEvents i_RewardVideoCallbacks, bool i_Append = true)
		{
			if (i_RewardVideoCallbacks == null)
			{
				if (!i_Append)
				{
					AvailabilityEvent = null;
					OpenEvent = null;
					OpenedEvent = null;
					CloseEvent = null;
					SuccessEvent = null;
					FailEvent = null;
				}
				return;
			}

			if (i_Append)
			{
				AvailabilityEvent += i_RewardVideoCallbacks.AvailabilityEvent;
				OpenEvent += i_RewardVideoCallbacks.OpenEvent;
				OpenedEvent += i_RewardVideoCallbacks.OpenedEvent;
				CloseEvent += i_RewardVideoCallbacks.CloseEvent;
				SuccessEvent += i_RewardVideoCallbacks.SuccessEvent;
				FailEvent += i_RewardVideoCallbacks.FailEvent;
			}
			else
			{
				AvailabilityEvent = i_RewardVideoCallbacks.AvailabilityEvent;
				OpenEvent = i_RewardVideoCallbacks.OpenEvent;
				OpenedEvent = i_RewardVideoCallbacks.OpenedEvent;
				CloseEvent = i_RewardVideoCallbacks.CloseEvent;
				SuccessEvent = i_RewardVideoCallbacks.SuccessEvent;
				FailEvent = i_RewardVideoCallbacks.FailEvent;
			}
		}
	}
}
