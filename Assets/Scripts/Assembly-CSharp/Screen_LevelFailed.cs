using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using TMPro;
using UnityEngine;

public class Screen_LevelFailed : MenuScreenBase
{
	[SerializeField]
	private TextMeshProUGUI m_LevelNumber;

	[SerializeField]
	private ExtendedButton m_TapToRestart;

	protected override void SetRefs()
	{
		base.SetRefs();
		m_LevelNumber = transform.FindDeepChild<TextMeshProUGUI>("Level ID");
		m_TapToRestart = transform.FindDeepChild<ExtendedButton>("Tap To Restart");
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		m_TapToRestart.Setup(OnTapToRestart);
		m_LevelNumber.text = "LEVEL " + StorageManager.Instance.CurrentLevel;
	}

	private void OnTapToRestart()
	{
		GameManager.Instance.ResetGame();
		Close();
	}
}
