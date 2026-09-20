using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim.Debugging
{
	public class GameLoopDebugButtonManager : MonoBehaviour
	{
		[SerializeField]
		private Button SwitchButtonsState;

		[SerializeField]
		private Button HideButton;

		[SerializeField]
		private Text SwitchButtonsStateText;

		[SerializeField]
		private GameObject ButtonsParent;

		private bool m_ShowButtons;

		private void OnEnable()
		{
			SwitchButtonsState.onClick.AddListener(() => m_ShowButtons = !m_ShowButtons);
			HideButton.onClick.AddListener(() => GameConfig.Instance.Debug.ShowGameLoopButtons = false);
		}

		private void OnDisable()
		{
			SwitchButtonsState.onClick.RemoveAllListeners();
			HideButton.onClick.RemoveAllListeners();
		}

		private void SetButtonsState(bool i_State)
		{
			m_ShowButtons = i_State;
		}

		private void Update()
		{
			bool show = GameConfig.Instance.Debug.ShowGameLoopButtons;
			SwitchButtonsState.gameObject.SetActive(show);
			HideButton.gameObject.SetActive(show);
			ButtonsParent.SetActive(show && m_ShowButtons);
		}
	}
}
