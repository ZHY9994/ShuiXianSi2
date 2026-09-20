using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KobGamesSDKSlim.Ads
{
	[RequireComponent(typeof(MAXBanner), typeof(MAXInterstitial), typeof(MAXRewardVideo))]
	public class MAXMediation : MonoBehaviour, IMediationNetwork
	{
		public MAXBanner MAXBanner;

		public MAXInterstitial MAXInterstitial;

		public MAXRewardVideo MAXRewardVideo;

		private Action m_OnInterstitialAdReadyEvent = delegate { };

		private Action m_OnInitializedCallback = delegate { };

		private int m_DelayInit = 5;

		public bool IsInit { get; set; }

		public IBanner Banner => MAXBanner;

		public IInterstitial Interstitial => MAXInterstitial;

		public IRewardVideo RewardVideo => MAXRewardVideo;

		public event Action OnInterstitialAdReadyEvent
		{
			[CompilerGenerated]
			add
			{
				Action current;
				Action updated;
				do { current = m_OnInterstitialAdReadyEvent; updated = (Action)Delegate.Combine(current, value); }
				while (System.Threading.Interlocked.CompareExchange(ref m_OnInterstitialAdReadyEvent, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				Action current;
				Action updated;
				do { current = m_OnInterstitialAdReadyEvent; updated = (Action)Delegate.Remove(current, value); }
				while (System.Threading.Interlocked.CompareExchange(ref m_OnInterstitialAdReadyEvent, updated, current) != current);
			}
		}

		public event Action OnInitializedCallback
		{
			[CompilerGenerated]
			add
			{
				Action current;
				Action updated;
				do { current = m_OnInitializedCallback; updated = (Action)Delegate.Combine(current, value); }
				while (System.Threading.Interlocked.CompareExchange(ref m_OnInitializedCallback, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				Action current;
				Action updated;
				do { current = m_OnInitializedCallback; updated = (Action)Delegate.Remove(current, value); }
				while (System.Threading.Interlocked.CompareExchange(ref m_OnInitializedCallback, updated, current) != current);
			}
		}

		public void OnValidate()
		{
			SetRefs();
		}

		public void SetRefs()
		{
			if (MAXBanner == null) MAXBanner = GetComponent<MAXBanner>();
			if (MAXInterstitial == null) MAXInterstitial = GetComponent<MAXInterstitial>();
			if (MAXRewardVideo == null) MAXRewardVideo = GetComponent<MAXRewardVideo>();
		}

		public void Init(Action i_InitializedCallback)
		{
			IsInit = true;
			i_InitializedCallback?.Invoke();
			m_OnInitializedCallback?.Invoke();
		}

		public void DelayedBannerInit()
		{
			MAXBanner?.Initialize();
		}

		public void DelayedInterstitialInit()
		{
			MAXInterstitial?.Initialize();
		}

		public void DelayedRewardVideoInit()
		{
			MAXRewardVideo?.Initialize();
		}

		public void ShowMediationDebugger()
		{
			return;
		}

		public bool IsInterstitialAvailable()
		{
			return MAXInterstitial != null && MAXInterstitial.IsInterstitialAvailable();
		}

		public bool IsRewardVideoAvailable()
		{
			return MAXRewardVideo != null && MAXRewardVideo.IsRewardVideoAvailable();
		}

		public void ShowBanner(BannerEvents i_BannerEvents)
		{
			MAXBanner?.ShowBanner(i_BannerEvents);
		}

		public void HideBanner()
		{
			MAXBanner?.HideBanner();
		}

		public void DestroyBanner()
		{
			MAXBanner?.DestroyBanner();
		}

		public void LoadInterstitial()
		{
			MAXInterstitial?.LoadInterstitial();
		}

		public void ShowInterstitial(InterstitialEvents i_InterstitialEvents, bool i_IsDummyAd = false)
		{
			MAXInterstitial?.ShowInterstitial(i_InterstitialEvents, i_IsDummyAd);
		}

		public void ShowRewardVideo(RewardVideoEvents i_RewardVideoEvents, bool i_IsDummyAd = false)
		{
			MAXRewardVideo?.ShowRewardVideo(i_RewardVideoEvents, i_IsDummyAd);
		}

		public void OnApplicationPause(bool i_Pause)
		{
			MAXBanner?.OnApplicationPause(i_Pause);
		}
	}
}
