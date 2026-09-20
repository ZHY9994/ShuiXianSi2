using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class UnitVisuals : UnitComponent
	{
		protected static readonly Color AuditBloodColor = new Color(0.15f, 0.85f, 0.25f, 1f);

		private bool m_StopHighlight = true;

		private Color m_BloodColor = AuditBloodColor;

		public Color BloodColor => m_BloodColor;

		protected virtual string m_SkinName => null;

		protected override void OnEnable()
		{
			base.OnEnable();
			UnitHealth.OnUnitDamageTaken += onUnitDamageTaken;
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			UnitHealth.OnUnitDamageTaken -= onUnitDamageTaken;
		}

		protected override void onUnitDeath(UnitBase i_Unit)
		{
			if (m_UnitBase.Health.Health <= 0f)
			{
				changeFace(eFaceType.Death);
			}
		}

		private void onUnitDamageTaken(float i_Damage, UnitBase i_Unit)
		{
			if (i_Unit == m_UnitBase)
			{
				changeFace(eFaceType.Angry);
			}
		}

		public virtual void Initialize(UnitSpawn i_Spawn)
		{
			return;
		}

		protected virtual void changeFace(eFaceType i_FaceType)
		{
			return;
		}

		protected void setBloodColor(Color i_Color)
		{
			m_BloodColor = i_Color;
			m_BloodColor.a = 1f;
		}
	}
}
