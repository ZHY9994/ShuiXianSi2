using UnityEngine;
using KobGamesSDKSlim.GameManagerV1;

namespace KobGamesSDKSlim.SoundManagerV1
{
	[ExecutionOrder(-50)]
	public class SoundManagerBase : Singleton<SoundManager>
	{
		[Header("Audio Sources")]
		public AudioSource MusicSource;

		public AudioSource SFXGameAudioSource;

		public AudioSource SFXUIAudioSource;

		[Header("Clips")]
		public AudioClip UIClickSFX;

		public bool IsMusicMuted
		{
			get
			{
				return StorageManager.Instance.IsMusicMuted;
			}
			set
			{
				StorageManager.Instance.IsMusicMuted = value;
			}
		}

		public bool IsSFXMuted
		{
			get
			{
				return StorageManager.Instance.IsSFXMuted;
			}
			set
			{
				StorageManager.Instance.IsSFXMuted = value;
			}
		}

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			UpdateAudioSourcesMuteState();
		}

		public override void Start()
		{
			base.Start();
		}

		protected new virtual void OnEnable()
		{
			GameManagerBase.OnGameReset += OnGameReset;
		}

		public override void OnDisable()
		{
			base.OnDisable();
			GameManagerBase.OnGameReset -= OnGameReset;
		}

		protected virtual void OnGameReset()
		{
			return;
		}

		public virtual void PlayUIClickSound()
		{
			if (SFXUIAudioSource != null) SFXUIAudioSource.PlayOneShot(UIClickSFX);
		}

		public virtual void PlaySFX(AudioClip i_AudioClip, float i_VolumeScale = 1f)
		{
			if (i_VolumeScale > 0f) SFXGameAudioSource.PlayOneShot(i_AudioClip, i_VolumeScale);
		}

		public virtual void PlayMusic(AudioClip i_AudioClip)
		{
			MusicSource.clip = i_AudioClip;
			PlayMusic();
		}

		public virtual void PlayMusic()
		{
			MusicSource.Play();
		}

		public virtual void ToggleMusicMute()
		{
			SetMusicMute(!IsMusicMuted);
		}

		public virtual void SetMusicMute(bool i_IsMute)
		{
			IsMusicMuted = i_IsMute;
			UpdateAudioSourcesMuteState();
		}

		public virtual void ToggleSFXMute()
		{
			SetSFXMute(!IsSFXMuted);
		}

		public virtual void SetSFXMute(bool i_IsMute)
		{
			IsSFXMuted = i_IsMute;
			UpdateAudioSourcesMuteState();
		}

		protected void UpdateAudioSourcesMuteState()
		{
			if (MusicSource != null) MusicSource.mute = IsMusicMuted;
			if (SFXUIAudioSource != null)
			{
				bool muted = IsSFXMuted;
				if (SFXGameAudioSource != null) SFXGameAudioSource.mute = muted;
				SFXUIAudioSource.mute = muted;
			}
		}

		public SoundManagerBase() : base()
		{
		}
	}
}
