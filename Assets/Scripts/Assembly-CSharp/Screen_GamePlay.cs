using KobGamesSDKSlim;
using KobGamesSDKSlim.MenuManagerV1;
using TMPro;
using UnityEngine;

public class Screen_GamePlay : MenuScreenBase
{
	[SerializeField]
	private TextMeshProUGUI m_LevelText;

	protected override void OnEnable()
	{
		base.OnEnable();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelFailedNoContinueEvent += onLevelFinished;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompletedEvent += onLevelFinished;
		updateLevelText();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelFailedNoContinueEvent -= onLevelFinished;
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelCompletedEvent -= onLevelFinished;
	}

	private void onLevelFinished(LevelData i_LevelData)
	{
		Close();
	}

	private void updateLevelText()
	{
		string level = LocalizationUtility.GetWordLocalizedString("level");
		m_LevelText.text = string.Format("{0} {1}", level, StorageManager.Instance.CurrentLevel);
	}

	protected override void SetRefs()
	{
		base.SetRefs();
	}
}
