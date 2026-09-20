using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using KobGamesSDKSlim;

namespace Wayne.Game.Shops
{
	public abstract class TRarityItem : MonoBehaviour
	{
		protected TRarityItem()
		{
		}

		protected enum eState
		{
			Locked = 0,
			Unlocked = 1
		}

		[SerializeField]
		private GameObject m_UnlockedState;

		[SerializeField]
		private GameObject m_LockedState;

		[SerializeField]
		protected Image[] m_Icons;

		protected eState m_State;

		protected virtual void updateVisual()
		{
			if (m_UnlockedState != null) m_UnlockedState.SetActive(m_State == eState.Unlocked);
			if (m_LockedState != null) m_LockedState.SetActive(m_State == eState.Locked);
		}

		public virtual void UpdateItem()
		{
			updateState();
		}

		protected virtual void updateState()
		{
			updateVisual();
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}

		protected virtual void SetRefs()
		{
			m_UnlockedState = transform.FindDeepChild<GameObject>("Unlocked");
			m_LockedState = transform.FindDeepChild<GameObject>("Locked");

			List<Image> icons = new List<Image>();
			Image[] images = GetComponentsInChildren<Image>(true);
			if (images != null)
			{
				foreach (Image image in images)
				{
					if (image != null && image.name == "Icon")
						icons.Add(image);
				}
			}
			m_Icons = icons.ToArray();
		}
	}
}
