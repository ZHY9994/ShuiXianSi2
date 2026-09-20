using System.Collections.Generic;
using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class UnitTrajectory : MonoBehaviour
	{
		[SerializeField]
		private Transform m_Root;

		[SerializeField]
        private List<TrajectoryDot> m_DotsList = new List<TrajectoryDot>();

		protected TrajectoryDataEditor.TrajectoryParameter m_Parameters => GameConfig.Instance.GamePlay.Trajectory.General;

		protected virtual void OnEnable()
		{
			initializeList(m_DotsList, m_Root, m_Parameters.DotColor, 2);
			HideTrajectory();
		}

		protected void initializeList(List<TrajectoryDot> i_DotList, Transform i_Root, Gradient i_ColorGradient, int i_Layer = 1)
		{
			foreach (TrajectoryDot dot in i_DotList)
			{
				if (dot != null)
				{
					PoolManager.Instance.Queue(ePoolType.TrajectoryDots, dot.gameObject);
				}
			}
			i_DotList.Clear();
			float scale = m_Parameters.ScaleMinMax.y;
			int dotCount = m_Parameters.DotCount;
			float step = scale / dotCount;
			for (int i = 0; i < dotCount; i++)
			{
				TrajectoryDot dot = PoolManager.Instance.Dequeue(ePoolType.TrajectoryDots, null, null, null, i_Root, true).GetComponent<TrajectoryDot>();
				if (i_ColorGradient != null)
				{
					dot.Initialize(scale, i_ColorGradient.Evaluate((float)i / dotCount));
					dot.SetOrder(i_Layer);
					i_DotList.Add(dot);
					scale -= scale <= m_Parameters.ScaleMinMax.x ? 0f : step;
				}
			}
			i_DotList[0].gameObject.SetActive(false);
			i_DotList[1].gameObject.SetActive(false);
			i_DotList[2].gameObject.SetActive(false);
		}

		public void ShowTrajectory(Vector3 i_Center, Vector3 i_PushDirection)
		{
			if (m_DotsList.Count >= 1)
			{
				m_Root.gameObject.SetActive(true);
				updateDots(i_PushDirection, i_Center, m_DotsList);
			}
		}

		public virtual void HideTrajectory()
		{
			m_Root.gameObject.SetActive(false);
		}

		protected void updateDots(Vector3 i_ShotVelocity, Vector3 i_ProjectilePosition, List<TrajectoryDot> i_DotList)
		{
			for (int i = 2; i < m_Parameters.DotCount; i++)
			{
				Vector3 parabolaPosition = WayneStaticFunctions.GetParabolaPosition(i_ProjectilePosition, i_ShotVelocity, i, m_Parameters.DotSpacing);
				if (i + 1 >= m_Parameters.DotCount)
				{
					return;
				}
				i_DotList[i + 1].transform.position = parabolaPosition;
			}
		}
	}
}
