using UnityEngine;
using UnityEngine.UI;
using KobGamesSDKSlim;

public class FPSCounterVisualizer : MonoBehaviour
{
	public Button ToggleButton;

	public GameObject FPSObject;

	public void Awake()
	{
		ToggleButton.onClick.AddListener(() => FPSObject.SetActive(!FPSObject.activeSelf));
	}

	public void Update()
	{
		FPSObject.SetActive(GameConfig.Instance.Debug.ShowFPSCounter);
		if (!ToggleButton.gameObject.activeSelf)
			FPSObject.SetActive(false);
	}
}
