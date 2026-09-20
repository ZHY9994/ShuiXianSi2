using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using UnityEngine;

namespace Wayne.Game.UIs
{
	public abstract class TMenuButtons : MonoBehaviour
	{
		public enum eButtonType
		{
			Home = 0,
			Skin = 1,
			Weapon = 2,
			Locked = 3
		}

		[SerializeField]
		private ExtendedButton m_Button;

		[SerializeField]
		private GameObject m_InActive;

		[SerializeField]
		private GameObject m_Active;

		public TMenuButtons()
		{
		}

		public bool IsActive => m_Active.activeInHierarchy;

		protected abstract eButtonType m_Type { get; }

		public static event Action<eButtonType> OnClicked
			= delegate { };

		public static event Action<eButtonType> OnActivate
			= delegate { };

		private void Awake()
		{
			if (m_Type == eButtonType.Home)
				activate();
			else
				deactivate();
		}

		protected virtual void OnEnable()
		{
			m_Button.Setup(new UnityEngine.Events.UnityAction(onClicked));
			OnClicked += onButtonClicked;
		}

		protected virtual void OnDisable()
		{
			OnClicked -= onButtonClicked;
		}

		protected void onClicked()
		{
			if (m_Type == eButtonType.Locked)
				return;

			if (ExperienceManager.Instance.IsBlock)
				return;

			if (!IsActive)
				ADManager.Instance.ShowBlackAd();

			OnClicked?.Invoke(m_Type);
		}

		private void onButtonClicked(eButtonType i_Type)
		{
			if (m_Type == i_Type)
				activate();
			else
				deactivate();
		}

		private void activate()
		{
			if (m_Active.activeInHierarchy)
				return;

			m_Active.SetActive(true);
			m_InActive.SetActive(false);
			onActivated();
			OnActivate?.Invoke(m_Type);
		}

		protected virtual void onActivated()
		{
		}

		private void deactivate()
		{
			if (m_Active != null && m_Active.activeInHierarchy)
			{
				m_Active.SetActive(false);
				m_InActive.SetActive(true);
			}
		}

		public static void OpenShop()
		{
			OnClicked?.Invoke(eButtonType.Weapon);
		}

		private void SetRefs()
		{
			m_Button = GetComponentInChildren<ExtendedButton>();
			m_InActive = transform.FindDeepChild<GameObject>("InActive");
			m_Active = transform.FindDeepChild<GameObject>("Active");
		}
	}
}
