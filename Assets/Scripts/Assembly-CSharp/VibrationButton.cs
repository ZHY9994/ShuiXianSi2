using KobGamesSDKSlim;
using UnityEngine;
using UnityEngine.Events;

public class VibrationButton : MonoBehaviour
{
	[SerializeField]
	private ExtendedButton m_VibrationButton;

	[SerializeField]
	private ImageToggle m_VibrationImageToggle;

	private void SetRefs()
	{
		m_VibrationButton = GetComponentInChildren<ExtendedButton>();
		m_VibrationImageToggle = GetComponentInChildren<ImageToggle>();
	}

	private void OnEnable()
	{
		m_VibrationButton.Setup(new UnityAction(OnVibrationButtonClick));
		UpdateVibrationButtonImage();
	}

	private void OnVibrationButtonClick()
	{
		StorageManager storage = StorageManager.Instance;
		bool state = storage.IsVibrationOn;
		storage.IsVibrationOn = !state;
		UpdateVibrationButtonImage();
	}

	private void UpdateVibrationButtonImage()
	{
		m_VibrationImageToggle.Set(StorageManager.Instance.IsVibrationOn);
	}
}
