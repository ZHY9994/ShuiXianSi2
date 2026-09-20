using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class HolyGrenadeExplosiveDetector : ExplosiveDetector
	{
		public override void Explode(float i_Radius, float i_ImpactForce, int i_ExplosiveDamage)
		{
			if (m_DetectorDic == null || m_DetectorDic.Count < 1)
				return;
			foreach (UnitPhysics physics in m_DetectorDic.Values)
			{
				if (physics == null || physics.UnitBase == null)
					continue;
				Transform closestBody = physics.GetClosestBody(transform.position);
				if (closestBody == null)
					continue;
				float distance = Vector3.Distance(closestBody.position, transform.position);
				float damage = DamageUtility.GetExplosiveDamageWithCurve(distance, i_Radius, i_ExplosiveDamage);
				Vector3 direction = closestBody.position - transform.position;
				Vector3 normalized = direction.sqrMagnitude <= 0.0000001f ? Vector3.zero : direction.normalized;
				float distanceRatio = i_Radius == 0f ? 0f : Mathf.Clamp01(distance / i_Radius);
				normalized.y += GameConfig.Instance.GamePlay.WeaponData.General.PushBodyYOffset;
				if (damage <= 0f)
					continue;
				float actualDamage = physics.UnitBase.UnitType == eUnityType.AI ? damage : 0f;
				physics.PushAllBody(normalized * ((1f - distanceRatio) * i_ImpactForce), actualDamage, eInteractableType.Weapon);
			}
		}
	}
}
