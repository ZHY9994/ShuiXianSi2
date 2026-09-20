using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KobGamesSDKSlim.Ads
{
	[RequireComponent(typeof(IronSourceBanner), typeof(IronSourceInterstitial), typeof(IronSourceRewardVideo))]
	public class IronSourceMediation : MonoBehaviour, IMediationNetwork
	{
		public IronSourceBanner IronSourceBanner;

		public IronSourceInterstitial IronSourceInterstitial;

		public IronSourceRewardVideo IronSourceRewardVideo;

		private Action m_OnInterstitialAdReadyEvent = delegate { };

		private Action m_OnInitializedCallback = delegate { };

		public bool IsInit { get; set; }

		public IBanner Banner => IronSourceBanner;

		public IInterstitial Interstitial => IronSourceInterstitial;

		public IRewardVideo RewardVideo => IronSourceRewardVideo;

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
			if (IronSourceBanner == null) IronSourceBanner = GetComponent<IronSourceBanner>();
			if (IronSourceInterstitial == null) IronSourceInterstitial = GetComponent<IronSourceInterstitial>();
			if (IronSourceRewardVideo == null) IronSourceRewardVideo = GetComponent<IronSourceRewardVideo>();
		}

		public void Init(Action i_InitializedCallback)
		{
			return;
		}

		public void ShowMediationDebugger()
		{
			return;
		}

		public bool IsInterstitialAvailable()
		{
			return IronSourceInterstitial != null && IronSourceInterstitial.IsInterstitialAvailable();
		}

		public bool IsRewardVideoAvailable()
		{
			return IronSourceRewardVideo != null && IronSourceRewardVideo.IsRewardVideoAvailable();
		}

		public void ShowBanner(BannerEvents i_BannerEvents)
		{
			IronSourceBanner?.ShowBanner(i_BannerEvents);
		}

		public void HideBanner()
		{
			IronSourceBanner?.HideBanner();
		}

		public void DestroyBanner()
		{
			IronSourceBanner?.DestroyBanner();
		}

		public void LoadInterstitial()
		{
			IronSourceInterstitial?.LoadInterstitial();
		}

		public void ShowInterstitial(InterstitialEvents i_InterstitialEvents, bool i_IsDummyAd = false)
		{
			IronSourceInterstitial?.ShowInterstitial(i_InterstitialEvents, i_IsDummyAd);
		}

		public void ShowRewardVideo(RewardVideoEvents i_RewardVideoEvents, bool i_IsDummyAd = false)
		{
			IronSourceRewardVideo?.ShowRewardVideo(i_RewardVideoEvents, i_IsDummyAd);
		}

		public void OnApplicationPause(bool i_Pause)
		{
			IronSourceBanner?.OnApplicationPause(i_Pause);
		}
	}
}
