using System.Collections.Generic;
using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class ExplosiveDetector : Detector
	{
		[SerializeField]
		private LayerMask m_WallLayer;

		[SerializeField]
		private eTags[] m_Walls;

		[SerializeField]
		private Dictionary<int, Collider> m_GeneralPurpose = new Dictionary<int, Collider>();

		protected override void OnTriggerEnter(Collider i_Other)
		{
			base.OnTriggerEnter(i_Other);
			if (i_Other.gameObject.CompareTag("Obstacle"))
			{
				addObstacle(i_Other);
			}
		}

		protected override void OnTriggerStay(Collider i_Other)
		{
			base.OnTriggerStay(i_Other);
			if (i_Other.gameObject.CompareTag("Obstacle"))
			{
				addObstacle(i_Other);
			}
		}

		protected override void OnTriggerExit(Collider i_Other)
		{
			base.OnTriggerExit(i_Other);
			if (i_Other.gameObject.CompareTag("Obstacle"))
			{
				removeObstacle(i_Other);
			}
		}

		private void addObstacle(Collider i_Obstacle)
		{
			int instanceId = i_Obstacle.GetInstanceID();
			if (m_GeneralPurpose.ContainsKey(instanceId))
			{
				return;
			}
			m_GeneralPurpose.Add(instanceId, i_Obstacle);
		}

		private void removeObstacle(Collider i_Obstacle)
		{
			int instanceId = i_Obstacle.GetInstanceID();
			if (m_GeneralPurpose.ContainsKey(instanceId))
			{
				m_GeneralPurpose.Remove(instanceId);
			}
		}

		public virtual void Explode(float i_Radius, float i_ImpactForce, int i_ExplosiveDamage)
		{
			pushObstacles(i_ImpactForce, i_Radius);
			if (m_DetectorDic.Count < 1)
			{
				return;
			}
			foreach (UnitPhysics physics in m_DetectorDic.Values)
			{
				if (physics == null)
				{
					continue;
				}
				Transform closestBody = physics.GetClosestBody(transform.position);
				if (closestBody == null)
				{
					continue;
				}
				float distance = Vector3.Distance(closestBody.position, transform.position);
				float damage = DamageUtility.GetExplosiveDamageWithCurve(distance, i_Radius, i_ExplosiveDamage);
				Vector3 direction = closestBody.position - transform.position;
				Vector3 normalized = direction.magnitude <= 0.00001f ? Vector3.zero : direction.normalized;
				float distanceRatio = i_Radius == 0f ? 0f : Mathf.Clamp01(distance / i_Radius);
				normalized = UnityTransformExtensions.SetPositionY(normalized, normalized.y + GameConfig.Instance.GamePlay.WeaponData.General.PushBodyYOffset);
				if (damage > 0f)
				{
					Vector3 velocity = normalized * ((1f - distanceRatio) * i_ImpactForce);
					physics.PushAllBody(velocity, damage, eInteractableType.Weapon);
				}
			}
		}

		protected bool isHittingWall(Vector3 i_ObjectPosition, float i_Distance)
		{
			Vector3 direction = i_ObjectPosition - transform.position;
			float distance = direction.magnitude;
			if (distance <= 0.00001f) return false;
			return Physics.Raycast(transform.position, direction / distance, i_Distance, m_WallLayer);
		}

		protected void pushObstacles(float i_ImpactForce, float i_Radius)
		{
			if (m_GeneralPurpose == null) return;
			foreach (Collider collider in m_GeneralPurpose.Values)
			{
				if (collider == null) continue;
				Vector3 direction = collider.transform.position - transform.position;
				float distance = direction.magnitude;
				if (distance > i_Radius || isHittingWall(collider.transform.position, distance)) continue;
				WoodenBox box = collider.GetComponent<WoodenBox>();
				if (box == null) continue;
				Vector3 normal = distance <= 0.00001f ? Vector3.zero : direction / distance;
				float strength = i_Radius <= 0f ? 0f : (1f - Mathf.Clamp01(distance / i_Radius)) * i_ImpactForce;
				box.Fragment(normal * strength);
			}
		}
	}
}
