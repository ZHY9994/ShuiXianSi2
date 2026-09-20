using UnityEngine;

namespace MrWayne
{
	public class ChihuahuaWeapon : AnimalWeapon
	{
		[SerializeField]
		private Animator m_Animator;

		public override eWeaponType Type => eWeaponType.Chihuahua;

		protected override ePoolType m_Stickable => ePoolType.ChihuahuaStickable;

		protected override void Update()
		{
			base.Update();
			if (m_State == eState.Thrown && !m_HasHitElement && m_RB != null && m_RB.velocity.sqrMagnitude > 0.0000001f)
			{
				m_RB.rotation = Quaternion.LookRotation(m_RB.velocity);
			}
		}

		protected override void fire(Vector3 i_Velocity, Vector3 i_Torque)
		{
			base.fire(i_Velocity, Vector3.zero);
			if (m_Animator != null)
			{
				m_Animator.SetTrigger(StaticString.AP_Attack);
			}
		}
	}
}
