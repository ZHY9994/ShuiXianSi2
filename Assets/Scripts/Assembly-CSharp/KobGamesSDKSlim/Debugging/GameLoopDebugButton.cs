using UnityEngine;

namespace KobGamesSDKSlim.Debugging
{
	public class GameLoopDebugButton : MonoBehaviour
	{
		[SerializeField]
		private eGameLoopDebugType m_Type;

		[SerializeField]
		private ExtendedButton m_Button;

		private void SetRefs()
		{
			m_Button = GetComponent<ExtendedButton>();
			m_Button.gameObject.name = m_Type.ToString();
		}

		private void OnEnable()
		{
			m_Button.Setup(Click);
		}

		private void OnDisable()
		{
			m_Button.Setup(null);
		}

		private void Click()
		{
			ShortcutsManager.Instance.TriggerShortcut(m_Type.ToString());
		}

		public GameLoopDebugButton()
		{
		}
	}
}
