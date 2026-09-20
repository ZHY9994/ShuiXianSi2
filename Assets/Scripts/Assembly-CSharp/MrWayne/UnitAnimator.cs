using UnityEngine;

namespace MrWayne
{
	public class UnitAnimator : UnitComponent
	{
		[SerializeField]
		internal Animator m_Animator;

		internal Vector3 m_LookPosition;

		private Vector3 m_CameraPosition => CameraManager.Instance.MainCamera.transform.position;

		protected override void onLevelCompleted()
		{
			if (m_UnitBase.Health.Health > 0f)
			{
				m_Animator.SetLayerWeight(1, 0f);
			}
		}

		private void OnAnimatorIK(int i_LayerIndex)
		{
			if (!m_GeneralParameter.UseIK)
			{
				return;
			}
			if (i_LayerIndex == 2)
			{
				setIKLookPosition(m_GeneralParameter.IKWeight, 0.5f, m_GeneralParameter.HeadWeight, m_CameraPosition);
			}
			else if (i_LayerIndex == 1 && Vector3.Distance(m_LookPosition, Vector3.zero) >= 1.0E-10f)
			{
				setIKLookPosition(m_GeneralParameter.IKWeight, m_GeneralParameter.BodyWeight, m_GeneralParameter.HeadWeight, m_LookPosition);
			}
		}

		public void CrossFade(int i_State, float i_Duration)
		{
			m_Animator.CrossFadeInFixedTime(i_State, i_Duration);
		}

		public void SetLayerWeight(int i_LayerIndex, int i_Weight)
		{
			m_Animator.SetLayerWeight(i_LayerIndex, i_Weight);
		}

		public void UpdateUnitLookPosition(Vector3 i_Position)
		{
			m_LookPosition = i_Position;
		}

		private void setIKLookPosition(float i_Weight, float i_BodyWeight, float i_HeadWeight, Vector3 i_Target)
		{
			m_Animator.SetLookAtWeight(i_Weight, i_BodyWeight, i_HeadWeight);
			m_Animator.SetLookAtPosition(i_Target);
		}

		public void PlayUnitThrow()
		{
			m_Animator.SetTrigger("Throw");
		}

		public void SetIntegerAnimParameter(int i_Value)
		{
			m_Animator.SetInteger("State", i_Value);
		}

		protected override void SetRefs()
		{
			m_UnitBase = GetComponentInParent<UnitBase>();
			m_Animator = GetComponent<Animator>();
		}
	}
}
