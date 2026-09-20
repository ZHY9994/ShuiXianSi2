using DG.Tweening;
using UnityEngine;

public class PlayAudioOnAwake : MonoBehaviour
{
	[SerializeField]
	private eSFXType m_AudioType;

	[SerializeField]
	private float m_Delay;

	private void OnEnable()
	{
		if (KobGamesSDKSlim.GameManager.Instance.GameState != KobGamesSDKSlim.eGameState.Playing)
			return;
		if (m_Delay == 0f)
		{
			KobGamesSDKSlim.SoundManager.Instance.PlayGeneralSFXByType(m_AudioType);
			return;
		}
		DOVirtual.DelayedCall(m_Delay, () => KobGamesSDKSlim.SoundManager.Instance.PlayGeneralSFXByType(m_AudioType));
	}
}
