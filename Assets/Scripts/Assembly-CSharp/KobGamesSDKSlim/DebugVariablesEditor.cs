using System;
using UnityEngine;

namespace KobGamesSDKSlim
{
	[Serializable]
	public class DebugVariablesEditor : DebugVariablesEditorBase
	{
		[SerializeField]
		public bool m_UnlimitedHealth;

		[SerializeField]
		public bool m_DebugWeapon;

		[SerializeField]
		public eWeaponType[] m_WeaponType;

		public DebugVariablesEditor()
			: base()
		{
		}

		public bool UnlimitedHealth => DebugMode && m_UnlimitedHealth;

		public bool DebugWeapon => DebugMode && m_DebugWeapon;

		public eWeaponType[] WeaponType => m_WeaponType;
	}
}
