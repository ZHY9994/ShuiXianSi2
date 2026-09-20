using KobGamesSDKSlim.MenuManagerV1;
using KobGamesSDKSlim;
using UnityEngine;

public class Screen_MenuButtons : MenuScreenBase
{
	[SerializeField]
	private GameObject m_Root;

	protected override void OnEnable()
	{
		base.OnEnable();
		rootVisibility();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted += onLevelStarted;
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		KobGamesSDKSlim.GameManagerV1.GameManagerBase.OnLevelStarted -= onLevelStarted;
	}

	private void onLevelStarted()
	{
		Close();
	}

	private void rootVisibility()
	{
		m_Root.SetActive(StorageManager.Instance.CurrentLevel > 2);
	}
}
