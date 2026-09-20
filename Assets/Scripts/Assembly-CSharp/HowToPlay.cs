using UnityEngine;
using KobGamesSDKSlim;
using Wayne.Game.Shops;

public class HowToPlay : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Root;

	private int m_ThrowCount;

	private void OnEnable()
	{
		m_ThrowCount = 0;
		m_Root.SetActive(false);
		if (StorageManager.Instance.WeaponSelectionTutorial && StorageManager.Instance.IsFirstLaunch && StorageManager.Instance.CurrentLevel == 1)
		{
			EventManager.OnPlayerStartAimEvent.Register(onStartedAiming);
			EventManager.OnPlayerEndAimEvent.Register(onPlayerThrow);
			WeaponSelectionManager.OnSetWeapon += onWeaponSelectedCallback;
		}
	}

	private void OnDisable()
	{
		EventManager.OnPlayerStartAimEvent.UnRegister(onStartedAiming);
		EventManager.OnPlayerEndAimEvent.UnRegister(onPlayerThrow);
		WeaponSelectionManager.OnSetWeapon -= onWeaponSelectedCallback;
	}

	private void onPlayerThrow()
	{
		m_ThrowCount++;
		m_Root.SetActive(false);
	}

	private void onWeaponSelected()
	{
		if (StorageManager.Instance.WeaponSelectionTutorial && StorageManager.Instance.IsFirstLaunch && StorageManager.Instance.CurrentLevel == 1)
			m_Root.SetActive(m_ThrowCount < 1);
		else
			m_Root.SetActive(false);
	}

	private void onWeaponSelectedCallback(WeaponItemData i_Item)
	{
		onWeaponSelected();
	}

	private void onStartedAiming()
	{
		m_Root.SetActive(false);
	}

	private void visibility(bool i_Visible)
	{
		m_Root.SetActive(i_Visible);
	}
}
