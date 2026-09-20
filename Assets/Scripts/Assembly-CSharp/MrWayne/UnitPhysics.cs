using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim;
using RootMotion.Dynamics;
using UnityEngine;

namespace MrWayne
{
	public class UnitPhysics : UnitComponent
	{
		[Serializable]
		public class BodyData
		{
			public UnitBodyPart BodyPart;

			public GameObject Bone;
		}

		[Serializable]
		private class BodyDataDictionary : UnitySerializedDictionary<eBodyPart, BodyData>
		{
		}

		[SerializeField]
		private BodyDataDictionary m_BodyData = new BodyDataDictionary();

		private bool m_WasPreviouslyUnpinned;

		private BehaviourPuppet m_Puppet => m_UnitBase.Puppet;

		public Transform Center => m_UnitBase.Center;

		public static event Action<float, eBodyPart, UnitBase, eInteractableType> OnUnitDamaged;

		public Transform GetClosestBody(Vector3 i_Position)
		{
			Transform closest = null;
			float closestDistance = float.MaxValue;
			foreach (BodyData bodyData in m_BodyData.Values)
			{
				Transform bodyTransform = bodyData.Bone.transform;
				float distance = Vector3.Distance(i_Position, bodyTransform.position);
				if (distance < closestDistance)
				{
					closestDistance = distance;
					closest = bodyTransform;
				}
			}
			return closest;
		}

		protected override void onUnitDeath(UnitBase i_Unit)
		{
			if (m_UnitBase.Health.Health <= 0f)
			{
				m_UnitBase.Puppet.puppetMaster.Kill();
				disableBodyTags();
			}
		}

		public void Initialize()
		{
			m_WasPreviouslyUnpinned = false;
		}

		public void PushAllBody(Vector3 i_Velocity, float i_Damage, eInteractableType i_Interactable)
		{
			if (m_UnitBase != null && m_UnitBase.Health != null && m_UnitBase.Health.Health > 0f && m_Puppet != null)
			{
				m_WasPreviouslyUnpinned = m_Puppet.state == BehaviourPuppet.State.Puppet ||
					m_Puppet.state == BehaviourPuppet.State.Unpinned ||
					m_Puppet.state == BehaviourPuppet.State.GetUp;
				m_Puppet.SetState(BehaviourPuppet.State.Unpinned);
			}
			foreach (BodyData bodyData in m_BodyData.Values)
			{
				if (bodyData != null && bodyData.BodyPart != null)
					bodyData.BodyPart.PushBodyPart(i_Velocity);
			}
			takeDamage(i_Damage, eBodyPart.Head, i_Interactable);
			m_WasPreviouslyUnpinned = false;
		}

		public void AnimaBitePuppetFix(BehaviourBase.AnimatorEvent[] i_HiEvent, bool i_CanGetUp = false)
		{
			m_UnitBase.Puppet.canGetUp = i_CanGetUp;
			m_UnitBase.Puppet.onLoseBalance.animations = i_HiEvent;
		}

		public GameObject GetBone(eBodyPart i_Part)
		{
			return m_BodyData[i_Part].Bone;
		}

		public void enableBodyTags()
		{
			foreach (BodyData bodyData in m_BodyData.Values)
			{
				bodyData.Bone.gameObject.tag = m_GeneralParameter.EnabledTag.ToString();
			}
		}

		private void disableBodyTags()
		{
			foreach (BodyData bodyData in m_BodyData.Values)
			{
				bodyData.Bone.gameObject.tag = m_GeneralParameter.DisabledTag.ToString();
			}
		}

		public void TakeDamage(float i_Damage, eBodyPart i_Part, eInteractableType i_Interactable)
		{
			takeDamage(i_Damage, i_Part, i_Interactable);
		}

		private void takeDamage(float i_Damage, eBodyPart i_Part, eInteractableType i_Interactable)
		{
			if (i_Damage == 0f)
			{
				return;
			}
			OnUnitDamaged?.Invoke(i_Damage, i_Part, m_UnitBase, i_Interactable);
		}

		protected override void SetRefs()
		{
			base.SetRefs();
			foreach (eBodyPart part in m_BodyData.Keys)
			{
				m_BodyData[part].BodyPart.SetRefs();
			}
		}

		private void setupBodyData()
		{
			return;
		}

	}
}
