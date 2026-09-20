using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MrWayne
{
	public class Detector : MonoBehaviour
	{
		[Serializable]
		protected class DetectorDictionary : UnitySerializedDictionary<int, UnitPhysics>
		{
		}

		[SerializeField]
		private SphereCollider m_Collider;

		protected DetectorDictionary m_DetectorDic = new DetectorDictionary();

		public bool HasTargets => m_DetectorDic.Values.Any(target => target != null);

		public List<UnitPhysics> Targets => m_DetectorDic.Values.Where(target => target != null).ToList();

		public void Initialize(float i_Radius)
		{
			m_DetectorDic.Clear();
			m_Collider.radius = i_Radius;
		}

		protected virtual void OnTriggerEnter(Collider i_Other)
		{
			if (i_Other.gameObject.CompareTag("BodyPart"))
			{
				UnitBodyPart bodyPart = i_Other.GetComponent<UnitBodyPart>();
				if (bodyPart != null)
				{
					addPhysics(bodyPart.Physics);
				}
			}
		}

		protected virtual void OnTriggerStay(Collider i_Other)
		{
			if (i_Other.gameObject.CompareTag("BodyPart"))
			{
				UnitBodyPart bodyPart = i_Other.GetComponent<UnitBodyPart>();
				if (bodyPart != null)
				{
					addPhysics(bodyPart.Physics);
				}
			}
		}

		protected virtual void OnTriggerExit(Collider i_Other)
		{
			if (i_Other.gameObject.CompareTag("BodyPart"))
			{
				UnitBodyPart bodyPart = i_Other.GetComponent<UnitBodyPart>();
				if (bodyPart != null)
				{
					removePhysics(bodyPart.Physics);
				}
			}
		}

		private void addPhysics(UnitPhysics i_Physics)
		{
			if (i_Physics == null)
			{
				return;
			}
			int instanceId = i_Physics.GetInstanceID();
			if (m_DetectorDic.ContainsKey(instanceId))
			{
				return;
			}
			m_DetectorDic.Add(instanceId, i_Physics);
		}

		private void removePhysics(UnitPhysics i_Physics)
		{
			if (i_Physics == null)
			{
				return;
			}
			int instanceId = i_Physics.GetInstanceID();
			if (m_DetectorDic.ContainsKey(instanceId))
			{
				m_DetectorDic.Remove(instanceId);
			}
		}

		protected virtual void SetRefs()
		{
			m_Collider = GetComponent<SphereCollider>();
		}
	}
}
