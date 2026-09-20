using System;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	[Serializable]
	public class WeaponData
	{
		[Serializable]
		public class WeaponStatsBase
		{
			public string Name;

			public float BaseHitDamage;

			public float ThrowForce;

			public float TorqueForce;

			public float PushForce;

			public float Mass;

			protected float m_Multiplier => GameConfig.Instance.GamePlay.WeaponData.Upgrade.Multiplier(Level);

			public float Multiplier => m_Multiplier;

			public string Id => Name ?? string.Empty;

			public int Level
			{
				get
				{
					return PlayerPrefs.GetInt("Weapon_" + (Name ?? string.Empty) + "_Level", 1);
				}
				set
				{
					PlayerPrefs.SetInt("Weapon_" + (Name ?? string.Empty) + "_Level", value);
				}
			}

			public int CardAmount
			{
				get
				{
					return PlayerPrefs.GetInt("Weapon_" + (Name ?? string.Empty) + "_CardAmount", 0);
				}
				set
				{
					PlayerPrefs.SetInt("Weapon_" + (Name ?? string.Empty) + "_CardAmount", value);
				}
			}

			public float HitDamageIncrease => BaseHitDamage * m_Multiplier;

			public float PushForceIncrease => PushForce * m_Multiplier;

			public virtual float BaseRadius => 0f;

			public virtual float RadiusIncrease => 0f;
		}

		[TextArea]
		public string WeaponDesciption;

		public Sprite Icon;

		public Sprite[] PowerIcons;

		public TItemData WeaponItem => WeaponShopManager.Instance.GetWeapon(Type);

		public virtual eWeaponType Type { get; private set; }

		public virtual ePoolType PoolType { get; private set; }

		public virtual WeaponStatsBase StatsData => null;

		public int Level => StatsData.Level;

		public int CardAmount => StatsData.CardAmount;

		public virtual float TotalDamage => 0f;

		public virtual string FormattedDescription => string.Empty;

		public virtual void UpgradeLevel()
		{
			WeaponStatsBase stats = StatsData;
			stats.Level = stats.Level + 1;
		}

		public virtual void AddCard(int i_Amount)
		{
			WeaponStatsBase stats = StatsData;
			stats.CardAmount = stats.CardAmount + i_Amount;
		}
	}
}
