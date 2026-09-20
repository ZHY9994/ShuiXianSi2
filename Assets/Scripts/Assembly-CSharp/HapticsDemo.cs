using System;
using MoreMountains.FeedbacksForThirdParty;
using UnityEngine;
using UnityEngine.UI;

public class HapticsDemo : MonoBehaviour
{
	private Button OriginalButton;

	private void Start()
	{
		OriginalButton = GetComponentInChildren<Button>();
		string[] names = Enum.GetNames(typeof(MMFeedbackHaptics.HapticTypes));
		for (int i = 0; i < names.Length; i++)
		{
			Button button = i == 0 ? OriginalButton : Instantiate(OriginalButton, OriginalButton.transform.parent);
			button.onClick.RemoveAllListeners();
			int type = i;
			button.onClick.AddListener(() => KobGamesSDKSlim.Managers.Instance.HapticManager.Haptic((MMFeedbackHaptics.HapticTypes)type));
			button.GetComponentInChildren<Text>().text = ((MMFeedbackHaptics.HapticTypes)i).ToString();
		}
	}
}
