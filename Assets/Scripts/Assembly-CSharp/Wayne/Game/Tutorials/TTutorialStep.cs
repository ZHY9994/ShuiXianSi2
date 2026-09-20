using UnityEngine;

namespace Wayne.Game.Tutorials
{
	public abstract class TTutorialStep : MonoBehaviour
	{
		[SerializeField]
		private eStep m_Step;

		public eStep Step => m_Step;

		public TTutorialStep()
		{
		}

		private void OnEnable()
		{
			onActivate();
		}

		protected virtual void onActivate()
		{
		}

		private void OnDisable()
		{
			onDeactivate();
		}

		protected virtual void onDeactivate()
		{
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Show()
		{
			gameObject.SetActive(true);
		}
	}
}
