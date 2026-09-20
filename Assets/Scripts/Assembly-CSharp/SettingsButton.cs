using KobGamesSDKSlim;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
	[SerializeField]
	private ExtendedButton m_SettingsButton;

	private void SetRefs()
	{
		m_SettingsButton = GetComponentInChildren<ExtendedButton>();
	}

	private void OnEnable()
	{
		m_SettingsButton.Setup(OnSettingsButtonDown);
	}

	private void OnSettingsButtonDown()
	{
		ADManager.Instance.ShowWhiteAd();
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.CloseMenuScreen("Screen_Welcome");
		KobGamesSDKSlim.MenuManagerV1.MenuManager.Instance.OpenMenuScreen("Screen_Settings");
		KobGamesSDKSlim.HUDManager.Instance.Close(true);
	}
}
