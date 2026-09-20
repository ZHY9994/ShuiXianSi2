using System;
using DG.Tweening;
using KobGamesSDKSlim.Animation;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace KobGamesSDKSlim
{
	public class ExtendedButton_FadeAnim : ExtendedButton
	{
		private string FadeDelayID => gameObject.GetInstanceID().ToString();

		private string FadeAnimID => gameObject.GetInstanceID().ToString() + gameObject.GetInstanceID().ToString();

		public void Setup(TweenData i_TweenData, UnityAction i_OnClick = null, Action<PointerEventData> i_OnDownEvent = null, Action<PointerEventData> i_OnUpEvent = null, Action<PointerEventData> i_OnBeginDragEvent = null, Action<PointerEventData> i_OnDragEvent = null, Action<PointerEventData> i_OnEndDragEvent = null, InteractivityChangedCallback i_OnInteractivityChanged = null)
		{
			if (m_IsSetup) return;
			m_IsSetup = true;
			SetEvents(i_OnClick, i_OnDownEvent, i_OnUpEvent, i_OnBeginDragEvent, i_OnDragEvent, i_OnEndDragEvent, i_OnInteractivityChanged);
			interactable = false;
			SetGraphicsVisibility(false);
			DOTween.Kill(FadeDelayID, FadeAnimID);
			DOVirtual.DelayedCall(i_TweenData.Delay, OnFadeAnimDelayCallback).SetId(FadeDelayID);
			FadeButton(true, i_TweenData);
		}

		protected virtual void OnFadeAnimDelayCallback()
		{
			SetInteractable(true);
		}

		public virtual void FadeButton(bool i_Enable, TweenData i_TweenData, TweenCallback i_OnComplete = null)
		{
			DOTween.Kill(FadeAnimID);
			m_CanvasGroup.DOFade(i_Enable ? 1f : 0f, i_TweenData.Duration).SetEase(i_TweenData.Ease).SetDelay(i_TweenData.Delay).SetId(FadeAnimID).OnComplete(i_OnComplete);
		}

		public override void ResetState()
		{
			DOTween.Complete(FadeDelayID);
			DOTween.Complete(FadeAnimID);
			base.ResetState();
		}
	}
}
