using DG.Tweening;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Wayne.Game.Shops
{
	public class TTabButton : MonoBehaviour
	{
		protected enum eState
		{
			Off = 0,
			On = 1
		}

		[SerializeField]
		private eItemRarity m_Rarity;

		[SerializeField]
		private RectTransform m_Self;

		[SerializeField]
		private GameObject[] m_StatesGOs;

		[SerializeField]
		private ExtendedButton m_Button;

		[SerializeField]
		private GameObject m_Notification;

		protected eState m_State;

		private Tween m_Tween;

		public TTabButton()
		{
		}

		public eItemRarity Rarity => m_Rarity;

		protected MenuVariablesEditor.ShopTabParameter m_Parameter => GameConfig.Instance.Menus.ShopTabParameters;

		protected virtual void OnEnable()
		{
			m_Button.Setup(new UnityAction(onClick));
		}

		protected virtual void OnDisable()
		{
			if (m_Tween != null) m_Tween.Kill();
		}

		protected virtual void onClick()
		{
		}

		protected virtual void onTabUpdate(eItemRarity i_Rarity)
		{
			setState(m_Rarity == i_Rarity ? eState.On : eState.Off);
		}

		public virtual void Initialize(eItemRarity i_ActiveRarity)
		{
			if (m_Tween != null) m_Tween.Kill();
			m_Self.sizeDelta = m_Parameter.Size;
			updateNotification();
			setState(m_Rarity == i_ActiveRarity ? eState.On : eState.Off);
		}

		private void setState(eState i_State)
		{
			m_State = i_State;
			hideState();
			if (m_State == eState.On)
			{
				m_StatesGOs[0].SetActive(true);
				onAnim();
			}
			else
			{
				m_StatesGOs[1].SetActive(true);
				offAnim();
			}
		}

		private void hideState()
		{
			for (int i = 0; i < m_StatesGOs.Length; i++)
				m_StatesGOs[i].SetActive(false);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		private void onAnim()
		{
			if (m_Tween != null) m_Tween.Kill();
			Vector2 target = m_Parameter.Size * m_Parameter.ScaleFactor;
			if ((m_Self.sizeDelta - target).sqrMagnitude < 1e-10f) return;
			m_Tween = m_Self.DOSizeDelta(target, m_Parameter.ShowDuration)
				.SetEase(m_Parameter.ShowEase)
				.From(m_Parameter.Size, true)
				.OnComplete(() => m_Self.sizeDelta = target);
		}

		private void offAnim()
		{
			if (m_Tween != null) m_Tween.Kill();
			if ((m_Self.sizeDelta - m_Parameter.Size).sqrMagnitude < 1e-10f) return;
			m_Tween = m_Self.DOSizeDelta(m_Parameter.Size, m_Parameter.HideDuration)
				.SetEase(m_Parameter.HideEase)
				.OnComplete(() => m_Self.sizeDelta = m_Parameter.Size);
		}

		protected virtual void updateNotification()
		{
			m_Notification.SetActive(false);
		}

		protected void notificationVisibility(bool i_Value)
		{
			m_Notification.SetActive(i_Value);
		}

		private void SetRefs()
		{
			Transform state = transform.FindDeepChild<Transform>("State");
			Image[] images = state.GetComponentsInChildren<Image>(true);
			m_StatesGOs = new GameObject[images.Length];
			for (int i = 0; i < images.Length; i++)
				m_StatesGOs[i] = images[i].gameObject;
			m_Self = GetComponent<RectTransform>();
			m_Button = GetComponentInChildren<ExtendedButton>(true);
			m_Notification = transform.FindDeepChild<GameObject>("Notification");
		}
	}
}
