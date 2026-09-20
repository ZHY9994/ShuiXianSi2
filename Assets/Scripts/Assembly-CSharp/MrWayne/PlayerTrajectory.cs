using System.Collections.Generic;
using UnityEngine;

namespace MrWayne
{
	public class PlayerTrajectory : UnitTrajectory
	{
		[SerializeField]
		private Transform m_PrevRoot;

		[SerializeField]
        private List<TrajectoryDot> m_PreDotList = new List<TrajectoryDot>();

		protected override void OnEnable()
		{
			base.OnEnable();
			initializeList(m_PreDotList, m_PrevRoot, m_Parameters.PrevDotColor, 1);
		}

		public void ShowPrevTrajectory(Vector3 i_Center, Vector3 i_PushDirection)
		{
			if (m_PreDotList.Count >= 1)
			{
				m_PrevRoot.gameObject.SetActive(true);
				updateDots(i_PushDirection, i_Center, m_PreDotList);
			}
		}

		public override void HideTrajectory()
		{
			base.HideTrajectory();
			m_PrevRoot.gameObject.SetActive(false);
		}
	}
}
