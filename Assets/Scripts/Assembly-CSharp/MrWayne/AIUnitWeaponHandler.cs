using System;
using System.Collections.Generic;
using UnityEngine;

namespace MrWayne
{
	public class AIUnitWeaponHandler : UnitWeaponHandler
	{
		private List<eWeaponType> m_Weapons = new List<eWeaponType>();

		private UnitHealth m_UnitHealth;

		private void Awake()
		{
			m_UnitHealth = GetComponent<UnitHealth>();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			setWeapons();
		}

		public override void InitializeWeapon(eWeaponType i_Type = eWeaponType.Grenade, Action<eWeaponType> i_SpawnCallback = null)
		{
			eWeaponType weapon = getWeapon();
			base.InitializeWeapon(weapon, i_SpawnCallback);
			if (m_UnitBase != null && m_UnitBase.Animator != null)
				m_UnitBase.Animator.SetIntegerAnimParameter(weapon == eWeaponType.RPG ? 1 : 0);
		}

		private void setWeapons()
		{
			if (m_Weapons == null) m_Weapons = new List<eWeaponType>();
			m_Weapons.Clear();
			DifficultyConfig config = DifficultyManager.Instance == null ? null : DifficultyManager.Instance.Config;
			if (config == null || config.EnemyWeapons == null || config.EnemyWeapons.Count == 0 || m_UnitHealth == null) return;
			int level = Mathf.Max(0, m_UnitHealth.Level - 1);
			DifficultyConfig.EnemyDeckData deck = config.EnemyWeapons[level % config.EnemyWeapons.Count];
			if (deck != null && deck.Weapons != null) m_Weapons.AddRange(deck.Weapons);
		}

		private eWeaponType getWeapon()
		{
			if (m_Weapons == null || m_Weapons.Count == 0) return default(eWeaponType);
			eWeaponType weapon = m_Weapons[0];
			m_Weapons.RemoveAt(0);
			m_Weapons.Add(weapon);
			return weapon;
		}
	}
}
