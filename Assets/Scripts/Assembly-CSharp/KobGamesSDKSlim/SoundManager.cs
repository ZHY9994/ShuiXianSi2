using System;
using System.Collections;
using KobGamesSDKSlim.Shop;
using KobGamesSDKSlim.SoundManagerV1;
using Wayne.Game.Shops;
using Wayne.Game.Shops.Skins;
using Wayne.Game.Shops.Weapons;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-50)]
	public class SoundManager : SoundManagerBase
	{
		[SerializeField]
		private AudioSource m_WeaponAudioSource;

		[SerializeField]
		private AudioSource m_EnvironmentSource;

		[SerializeField]
		private AudioSource m_ExplosionSource;

		[SerializeField]
		private AudioSource m_Pendulum;

		[SerializeField]
		private AudioSource[] m_BGSources;

		private int m_ActiveBG;

		private Coroutine[] m_Fader = new Coroutine[2];

		private AudioDataEditor m_AudioDataEditor => GameConfig.Instance.GamePlay.AudioDataEditor;

		protected override void OnEnable()
		{
			base.OnEnable();
			initializeBGs();
			GameManagerV1.GameManagerBase.OnGameReset += onGameReset;
			GameManagerV1.GameManagerBase.OnLevelStartedEvent += onLevelStarted;
			GameManagerV1.GameManagerBase.OnLevelCompletedEvent += onLevelComplete;
			GameManagerV1.GameManagerBase.OnLevelFailedEvent += onLevelFailed;
			Wayne.Game.Shops.SkinShopManager.OnSelectItem += onItemSelect;
			Wayne.Game.Shops.WeaponShopManager.OnSelectItem += onItemSelect;
		}

		public override void OnDisable()
		{
			base.OnDisable();
			GameManagerV1.GameManagerBase.OnGameReset -= onGameReset;
			GameManagerV1.GameManagerBase.OnLevelStartedEvent -= onLevelStarted;
			GameManagerV1.GameManagerBase.OnLevelCompletedEvent -= onLevelComplete;
			GameManagerV1.GameManagerBase.OnLevelFailedEvent -= onLevelFailed;
			Wayne.Game.Shops.SkinShopManager.OnSelectItem -= onItemSelect;
			Wayne.Game.Shops.WeaponShopManager.OnSelectItem -= onItemSelect;
		}

		private void onGameReset()
		{
			playBG(m_AudioDataEditor.Menu.Clip, m_AudioDataEditor.Menu.Volume);
			stopPendulumSFX();
		}

		private void onLevelStarted(LevelData i_Data)
		{
			playBG(m_AudioDataEditor.Combat.Clip, m_AudioDataEditor.Combat.Volume);
		}

		private void onLevelComplete(LevelData i_Data)
		{
			stopBG();
			PlayGeneralSFXByType(eSFXType.LevelWin);
			stopPendulumSFX();
		}

		private void onLevelFailed(LevelData i_Data)
		{
			stopPendulumSFX();
		}

		private void onItemSelect(TItemData i_Data)
		{
			PlayGeneralSFXByType(eSFXType.ItemSelect);
		}

		private void toggleMusic()
		{
			if (IsMusicMuted)
			{
				stopBG();
				return;
			}
			if (m_BGSources == null) return;
			for (int i = 0; i < m_BGSources.Length; i++)
				if (m_BGSources[i] != null && m_BGSources[i].isPlaying) return;
			if (GameManager.Instance == null) return;
			AudioClipData data = m_AudioDataEditor.Menu;
			if (GameManager.Instance.GameState == eGameState.Playing) data = m_AudioDataEditor.Combat;
			AudioSource source = m_BGSources[m_ActiveBG];
			if (source == null) return;
			source.clip = data.Clip;
			source.volume = data.Volume;
			source.Play();
		}

		public void PlayGeneralSFXByType(eSFXType i_Type)
		{
			if (IsSFXMuted) return;
			AudioClipData data = m_AudioDataEditor.GetGeneralAudio(i_Type);
			if (data == null) return;
			if (i_Type == eSFXType.BloodSpill || i_Type == eSFXType.BrokenGlass || i_Type == eSFXType.WoodenBoxBreaking)
				playEnvironSFX(data.Clip, data.Volume);
			else if (i_Type == eSFXType.BigExplosion || i_Type == eSFXType.SmallExplosion)
				playExplosionSFX(data.Clip, data.Volume);
			else if (i_Type == eSFXType.Pendulum)
				playPendulumSFX(data.Clip, data.Volume);
			else
				PlaySFX(data.Clip, data.Volume);
		}

		private void playEnvironSFX(AudioClip i_Clip, float i_Volume)
		{
			m_EnvironmentSource.PlayOneShot(i_Clip, i_Volume);
		}

		private void playExplosionSFX(AudioClip i_Clip, float i_Volume)
		{
			m_ExplosionSource.PlayOneShot(i_Clip, i_Volume);
		}

		public void PlayExplosionSFX(AudioClip i_Clip, float i_Volume)
		{
			m_ExplosionSource.PlayOneShot(i_Clip, i_Volume);
		}

		public void playPendulumSFX(AudioClip i_Clip, float i_Volume)
		{
			if (IsSFXMuted) return;
			m_Pendulum.clip = i_Clip;
			m_Pendulum.volume = i_Volume;
			m_Pendulum.loop = true;
			m_Pendulum.Play();
		}

		private void stopPendulumSFX()
		{
			m_Pendulum.Stop();
		}

		private bool isType(eSFXType i_Type, params eSFXType[] i_AllTypes)
		{
			if (i_AllTypes == null) throw new ArgumentNullException(nameof(i_AllTypes));
			for (int i = 0; i < i_AllTypes.Length; i++)
				if (i_AllTypes[i] == i_Type) return true;
			return false;
		}

		public void PlayWeaponSFX(AudioClip i_Clip, float i_Volume)
		{
			if (!IsSFXMuted && i_Volume > 0f && i_Clip != null)
				m_WeaponAudioSource.PlayOneShot(i_Clip, i_Volume);
		}

		public override void SetMusicMute(bool i_IsMute)
		{
			IsMusicMuted = i_IsMute;
			UpdateAudioSourcesMuteState();
			toggleMusic();
		}

		public override void SetSFXMute(bool i_IsMute)
		{
			IsSFXMuted = i_IsMute;
			UpdateAudioSourcesMuteState();
			toggleMusic();
		}

		private void initializeBGs()
		{
			m_Fader = new Coroutine[2];
			for (int i = 0; i < m_BGSources.Length; i++)
			{
				m_BGSources[i].loop = true;
				m_BGSources[i].playOnAwake = false;
				m_BGSources[i].volume = 0f;
			}
			playBG(m_AudioDataEditor.Menu.Clip, m_AudioDataEditor.Menu.Volume);
		}

		private void playBG(AudioClip i_Clip, float i_Volume)
		{
			if (IsMusicMuted || m_BGSources == null || m_BGSources.Length == 0) return;
			AudioSource current = m_BGSources[m_ActiveBG];
			if (current != null && current.clip == i_Clip) return;
			for (int i = 0; i < m_Fader.Length; i++)
				if (m_Fader[i] != null) StopCoroutine(m_Fader[i]);
			if (current != null && current.volume > 0f)
			{
				m_Fader[0] = StartCoroutine(CrossFadeCoroutine(current, m_AudioDataEditor.FadeDuration, 0f, () => current.Stop()));
			}
			m_ActiveBG = (m_ActiveBG + 1) % m_BGSources.Length;
			AudioSource next = m_BGSources[m_ActiveBG];
			next.clip = i_Clip;
			next.Play();
			m_Fader[1] = StartCoroutine(CrossFadeCoroutine(next, m_AudioDataEditor.FadeDuration, i_Volume));
		}

		private void stopBG()
		{
			if (m_BGSources == null) return;
			for (int i = 0; i < m_BGSources.Length; i++)
				if (m_BGSources[i] != null) m_BGSources[i].Stop();
		}

		private IEnumerator CrossFadeCoroutine(AudioSource i_Player, float i_Duration, float i_TargetVolume, Action i_FinishedCallback = null)
		{
			int steps = (int)(m_AudioDataEditor.VolumeChangesPerSecond * i_Duration);
			float stepTime = i_Duration / steps;
			float stepSize = (i_TargetVolume - i_Player.volume) / steps;
			for (int i = 1; i < steps; i++)
			{
				i_Player.volume += stepSize;
				yield return new WaitForSeconds(stepTime);
			}
			i_Player.volume = i_TargetVolume;
			i_FinishedCallback?.Invoke();
		}
	}
}
