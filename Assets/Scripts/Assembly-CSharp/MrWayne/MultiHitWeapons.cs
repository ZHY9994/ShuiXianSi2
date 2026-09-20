using UnityEngine;

namespace MrWayne
{
	public class MultiHitWeapons : ThrowableWeapon
	{
		private float m_HitTime = -1f;

		protected int m_BounceCount;

		private UnitBase m_LastHitUnit;

		protected sealed override void hitBodyPart(Collision i_Other)
		{
			if (m_Unit.UnitType == m_HitBodyPart.Physics.UnitType)
			{
				if (Time.time - m_WeaponThrowTime < m_GeneralParameter.DamageDelay)
				{
					return;
				}
			}
			if (m_LastHitUnit != null && m_LastHitUnit != m_HitBodyPart.Physics.UnitBase)
			{
				onHitBody(i_Other);
				return;
			}
			if (m_HitTime == -1f || Time.time - m_HitTime >= m_GeneralParameter.HitDelay)
			{
				onHitBody(i_Other);
			}
		}

		protected sealed override void hitOtherElements(Collision i_Other)
		{
			onHitElement(i_Other);
		}

		protected virtual void onHitBody(Collision i_Other)
		{
			return;
		}

		protected virtual void onHitElement(Collision i_Other)
		{
			return;
		}

		protected override void initializeValues()
		{
			base.initializeValues();
			m_LastHitUnit = null;
			m_HitTime = -1f;
			m_BounceCount = 0;
		}

		protected void updateHit()
		{
			if (m_HitBodyPart == null || m_HitBodyPart.Physics == null)
				return;
			m_LastHitUnit = m_HitBodyPart.Physics.UnitBase;
			m_HitTime = Time.time;
		}

		protected void updateBounceCount()
		{
			m_BounceCount++;
		}
	}
}
