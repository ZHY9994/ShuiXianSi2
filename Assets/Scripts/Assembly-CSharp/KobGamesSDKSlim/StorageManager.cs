using System.Runtime.CompilerServices;
using KobGamesSDKSlim.Collectable;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class StorageManager : StorageManagerBase
	{
		public new delegate void CollectableAmountChangedEvent(int i_CoinsAmount);

		public int ExtendedVariableNonPersistent;

		public int ExtendedVariablePersistent
		{
			get
			{
				return PlayerPrefs.GetInt("ExtendedVariablePersistent", 0);
			}
			set
			{
				PlayerPrefs.SetInt("ExtendedVariablePersistent", value);
			}
		}

		public int PlayerFlagIndex
		{
			get
			{
				return PlayerPrefs.GetInt("PlayerFlagIndex", 0);
			}
			set
			{
				PlayerPrefs.SetInt("PlayerFlagIndex", value);
			}
		}

		public int PlayerFlagSpiteIndex
		{
			get
			{
				return PlayerPrefs.GetInt("PlayerFlagSpiteIndex", 0);
			}
			set
			{
				PlayerPrefs.SetInt("PlayerFlagSpiteIndex", value);
			}
		}

		public string PlayerName
		{
			get
			{
				return PlayerPrefs.GetString("PlayerName", "");
			}
			set
			{
				PlayerPrefs.SetString("PlayerName", value);
			}
		}

		public bool IsSpecialOfferPurchased
		{
			get
			{
				return PlayerPrefsBool.GetBool("IsSpecialOfferPurchased", false);
			}
			set
			{
				PlayerPrefsBool.SetBool("IsSpecialOfferPurchased", value);
			}
		}

		public bool IsGoldenOfferPurchased
		{
			get
			{
				return PlayerPrefsBool.GetBool("IsGoldenOfferPurchased", false);
			}
			set
			{
				PlayerPrefsBool.SetBool("IsGoldenOfferPurchased", value);
			}
		}

		/// <summary>
		/// One-time rewarded-video claim state for the mythic skin offer in Main.
		/// </summary>
		public bool IsMythicSkinRewardClaimed
		{
			get => PlayerPrefsBool.GetBool("IsMythicSkinRewardClaimed", false);
			set => PlayerPrefsBool.SetBool("IsMythicSkinRewardClaimed", value);
		}

	/// <summary>
	/// One-time rewarded-video claim state for the epic skin offer in Main.
	/// </summary>
	public bool IsEpicSkinRewardClaimed
	{
		get => PlayerPrefsBool.GetBool("IsEpicSkinRewardClaimed", false);
		set => PlayerPrefsBool.SetBool("IsEpicSkinRewardClaimed", value);
	}

	/// <summary>
	/// Number of successful rewarded videos watched for the skin offer.
	/// </summary>
	public int SkinRewardVideoCount
	{
		get => Mathf.Clamp(PlayerPrefs.GetInt("SkinRewardVideoCount", 0), 0, 3);
		set => PlayerPrefs.SetInt("SkinRewardVideoCount", Mathf.Clamp(value, 0, 3));
	}

		/// <summary>
		/// One-time rewarded-video claim state for the mythic weapon offer in Main.
		/// </summary>
		public bool IsMythicWeaponRewardClaimed
		{
			get => PlayerPrefsBool.GetBool("IsMythicWeaponRewardClaimed", false);
			set => PlayerPrefsBool.SetBool("IsMythicWeaponRewardClaimed", value);
		}

		/// <summary>
		/// Number of successful rewarded videos watched for the mythic weapon offer.
		/// </summary>
		public int MythicWeaponRewardVideoCount
		{
			get => Mathf.Clamp(PlayerPrefs.GetInt("MythicWeaponRewardVideoCount", 0), 0, 3);
			set => PlayerPrefs.SetInt("MythicWeaponRewardVideoCount", Mathf.Clamp(value, 0, 3));
		}

		public bool WeaponSelectionTutorial
		{
			get
			{
				return PlayerPrefsBool.GetBool("WeaponSelectionTutorial", HighScoreLevel <= 1);
			}
			set
			{
				PlayerPrefsBool.SetBool("WeaponSelectionTutorial", value);
				global::Ade_Framework.AdeCloudPlayerPrefs.Save();
			}
		}

		public int LevelLoop
		{
			get
			{
				return PlayerPrefs.GetInt("LevelLoop", 1);
			}
			set
			{
				PlayerPrefs.SetInt("LevelLoop", value);
			}
		}

		public bool RewardWaiting
		{
			get
			{
				return PlayerPrefsBool.GetBool("RewardWaiting", false);
			}
			set
			{
				PlayerPrefsBool.SetBool("RewardWaiting", value);
			}
		}

		public int PlayerHealth
		{
			get
			{
				// IDA 0x0222A57C: the PlayerPrefs fallback is the configured
				// starting player health.  The exported Player config has lost
				// that serialized member, while GeneralUnitParameters.HealthPerLevel
				// remains the authored base-health value (30 in GameConfig.asset).
				int defaultHealth = Mathf.RoundToInt(GameConfig.Instance.GamePlay.UnitData.GeneralUnitParameters.HealthPerLevel);
				return PlayerPrefs.GetInt("PlayerHealth", defaultHealth);
			}
			set
			{
				PlayerPrefs.SetInt("PlayerHealth", value);
			}
		}

		public string AchievedLevelSave
		{
			get
			{
				return PlayerPrefs.GetString("AchievedLevelSave", "");
			}
			set
			{
				PlayerPrefs.SetString("AchievedLevelSave", value);
			}
		}

		public new static event CollectableAmountChangedEvent OnCoinsAmountChanged = delegate { };

		public override void SetCollectable(eCollectableType i_CollectableType, int i_Amount)
		{
			base.SetCollectable(i_CollectableType, i_Amount);
			if (i_CollectableType == eCollectableType.Coin)
				OnCoinsAmountChanged?.Invoke(i_Amount);
		}
	}
}
