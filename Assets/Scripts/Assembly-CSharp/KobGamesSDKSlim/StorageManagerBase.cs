using System.Runtime.CompilerServices;
using KobGamesSDKSlim.Collectable;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-300)]
	public class StorageManagerBase : Singleton<StorageManager>
	{
		public delegate void CoinsAmountChangedEvent(int i_CoinsAmount);

		public delegate void CollectableAmountChangedEvent(eCollectableType i_CollectableType, int i_EarnAmount, int i_ResultAmount);

		public int DefaultStartlevel = 1;

		public int InterstitialCoolDownCounter;

		private int m_CurrentLevel;

		private int m_CurrentScore;

		private int m_GameOverSessionAttempts;

		private int m_GameWonSessionAttempts;

		private bool m_IsFirstAttempt = true;

		private float m_Timer;

		public int GameLaunchCount
		{
			get { return PlayerPrefs.GetInt("GameLaunchCount", 0); }
			set
			{
				PlayerPrefs.SetInt("GameLaunchCount", value);
			}
		}

		public bool IsGDPRConsentGiven
		{
			get
			{
				return PlayerPrefs.GetInt("IsGDPRConsentGiven", 0) == 1;
			}
			set
			{
				PlayerPrefs.SetInt("IsGDPRConsentGiven", value ? 1 : 0);
			}
		}

		public string InstallationID
		{
			get
			{
				return PlayerPrefs.GetString("InstallationID", "");
			}
			set
			{
				PlayerPrefs.SetString("InstallationID", value);
			}
		}

		public int CurrentLevelAttempts
		{
			get
			{
				return PlayerPrefs.GetInt("CurrentLevelAttempts", 0);
			}
			protected set
			{
				PlayerPrefs.SetInt("CurrentLevelAttempts", value);
			}
		}

		public int TotalAttempts
		{
			get
			{
				return PlayerPrefs.GetInt("TotalAttempts", 0);
			}
			set
			{
				PlayerPrefs.SetInt("TotalAttempts", value);
			}
		}

		public int TotalGameWins
		{
			get
			{
				return PlayerPrefs.GetInt("TotalGameWins", 0);
			}
			private set
			{
				PlayerPrefs.SetInt("TotalGameWins", value);
			}
		}

		public int OverallAttempts => TotalAttempts + TotalGameWins;

		public int HighScore
		{
			get
			{
				return PlayerPrefs.GetInt("HighScore", 0);
			}
			private set
			{
				PlayerPrefs.SetInt("HighScore", value);
			}
		}

		public int HighScoreLevel
		{
			get
			{
				return PlayerPrefs.GetInt("HighScoreLevel", DefaultStartlevel);
			}
			protected set
			{
				PlayerPrefs.SetInt("HighScoreLevel", value);
			}
		}

		public LevelData OnGameClosedLevelData
		{
			get
			{
				string json = PlayerPrefs.GetString("OnGameClosedLevelData", "");
				return string.IsNullOrEmpty(json) ? null : JsonUtility.FromJson<LevelData>(json);
			}
			set
			{
				PlayerPrefs.SetString("OnGameClosedLevelData", value == null ? "" : JsonUtility.ToJson(value));
			}
		}

		public int LevelsCount => TotalAttempts + TotalGameWins + 1;

		public int CoinsAmount
		{
			get
			{
				// The live economy uses the collectable key "Coin". Keep the
				// legacy "CoinsAmount" key as a fallback for older saves.
				if (PlayerPrefs.HasKey(eCollectableType.Coin.ToString()))
					return PlayerPrefs.GetInt(eCollectableType.Coin.ToString(), 0);

				return PlayerPrefs.GetInt("CoinsAmount", 0);
			}
			set
			{
				PlayerPrefs.SetInt("CoinsAmount", value);
				SetCollectable(eCollectableType.Coin, value);
				OnCoinsAmountChanged?.Invoke(value);
			}
		}

		public bool IsSFXMuted
		{
			get
			{
				return PlayerPrefs.GetInt("IsSFXMuted", 0) == 1;
			}
			set
			{
				PlayerPrefs.SetInt("IsSFXMuted", value ? 1 : 0);
			}
		}

		public bool IsMusicMuted
		{
			get
			{
				return PlayerPrefs.GetInt("IsMusicMuted", 0) == 1;
			}
			set
			{
				PlayerPrefs.SetInt("IsMusicMuted", value ? 1 : 0);
			}
		}

		public bool IsVibrationOn
		{
			get
			{
				return PlayerPrefs.GetInt("IsVibrationOn", 1) == 1;
			}
			set
			{
				PlayerPrefs.SetInt("IsVibrationOn", value ? 1 : 0);
			}
		}

		public bool RemovedAds
		{
			get
			{
				return PlayerPrefsBool.GetBool("RemovedAds", false);
			}
			set
			{
				PlayerPrefsBool.SetBool("RemovedAds", value);
			}
		}

		public int TotalVideosWatched
		{
			get
			{
				return PlayerPrefs.GetInt("TotalVideosWatched", 0);
			}
			set
			{
				PlayerPrefs.SetInt("TotalVideosWatched", value);
			}
		}

		public int TotalInterstitialsWatched
		{
			get
			{
				return PlayerPrefs.GetInt("TotalInterstitialsWatched", 0);
			}
			set
			{
				PlayerPrefs.SetInt("TotalInterstitialsWatched", value);
			}
		}

		public virtual int CurrentLevel
		{
			get
			{
				return m_CurrentLevel;
			}
			set
			{
				if (m_CurrentLevel != value) CurrentLevelAttempts = 0;
				m_CurrentLevel = value;
				if (HighScoreLevel < value) HighScoreLevel = value;
			}
		}

		public int CurrentScore
		{
			get
			{
				return m_CurrentScore;
			}
			set
			{
				m_CurrentScore = value;
				if (HighScore < value) HighScore = value;
			}
		}

		public int GameOverSessionAttempts
		{
			get
			{
				return m_GameOverSessionAttempts;
			}
			set
			{
				if (m_GameOverSessionAttempts < value)
				{
					CurrentLevelAttempts++;
					TotalAttempts++;
					m_IsFirstAttempt = false;
				}
				m_GameOverSessionAttempts = value;
			}
		}

		public int GameWonSessionAttempts
		{
			get
			{
				return m_GameWonSessionAttempts;
			}
			set
			{
				if (m_GameWonSessionAttempts < value)
				{
					m_IsFirstAttempt = false;
					TotalGameWins++;
				}
				m_GameWonSessionAttempts = value;
			}
		}

		public bool IsFirstAttempt => m_IsFirstAttempt;

		public bool IsFirstLaunch => GameLaunchCount < 2;

		public string DeviceID => SystemInfo.deviceUniqueIdentifier;

		public static event CoinsAmountChangedEvent OnCoinsAmountChanged;

		public static event CollectableAmountChangedEvent OnCollectableAmountChanged;

		public virtual void SetCollectable(eCollectableType i_CollectableType, int i_Amount)
		{
			int oldAmount = GetCollectable(i_CollectableType);
			PlayerPrefs.SetInt(i_CollectableType.ToString(), i_Amount);
			OnCollectableAmountChanged?.Invoke(i_CollectableType, i_Amount - oldAmount, i_Amount);
		}

		public void AddCollectable(eCollectableType i_CollectableType, int i_AddAmount)
		{
			SetCollectable(i_CollectableType, GetCollectable(i_CollectableType) + i_AddAmount);
		}

		public void SubtractCollectable(eCollectableType i_CollectableType, int i_SubtractAmount)
		{
			SetCollectable(i_CollectableType, GetCollectable(i_CollectableType) - i_SubtractAmount);
		}

		public int GetCollectable(eCollectableType i_CollectableType)
		{
			string key = i_CollectableType.ToString();
			if (i_CollectableType == eCollectableType.Coin && !PlayerPrefs.HasKey(key) && PlayerPrefs.HasKey("CoinsAmount"))
			{
				int legacyAmount = PlayerPrefs.GetInt("CoinsAmount", 0);
				PlayerPrefs.SetInt(key, legacyAmount);
				return legacyAmount;
			}

			return PlayerPrefs.GetInt(key, -1);
		}

		public override void OnAwakeEvent()
		{
			base.OnAwakeEvent();
			GameLaunchCount++;
			if (string.IsNullOrEmpty(InstallationID)) InstallationID = System.Guid.NewGuid().ToString();
		}

		protected override void OnApplicationPause(bool i_IsPaused)
		{
			base.OnApplicationPause(i_IsPaused);
			if (i_IsPaused)
				PlayerPrefs.Save();
		}

		public override void OnApplicationQuit()
		{
			PlayerPrefs.Save();
			base.OnApplicationQuit();
		}

		private void Update()
		{
			if (InterstitialCoolDownCounter < 1) return;
			m_Timer += Time.unscaledDeltaTime;
			if (m_Timer > 1f) { m_Timer = 0f; InterstitialCoolDownCounter--; }
		}

		public void ClearPlayerPrefs()
		{
			PlayerPrefs.DeleteAll();
		}

		public void OpenPersistentPath()
		{
			Application.OpenURL("file://" + Application.persistentDataPath);
		}
	}
}
