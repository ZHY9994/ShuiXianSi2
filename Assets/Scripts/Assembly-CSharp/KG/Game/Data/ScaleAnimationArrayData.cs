using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace KG.Game.Data
{
	[Serializable]
	public class ScaleAnimationArrayData
	{
		[Serializable]
		private class StepData
		{
			public Vector3 Scale;

			public float Time;

			public Ease Ease;

			public float Delay;
		}

		[SerializeField]
		private List<StepData> m_Steps;

		public float FullTime
		{
			get
			{
				if (m_Steps == null) return 0f;
				float result = 0f;
				foreach (StepData step in m_Steps)
				{
					if (step == null) continue;
					result += step.Time + Mathf.Max(0f, step.Delay);
				}
				return result;
			}
		}

		public int StepCount => m_Steps == null ? 0 : m_Steps.Count;

		public Sequence Start(Transform i_Transform, float i_ScaleMult = 1f, string i_Debug = "debugScale")
		{
			Sequence sequence = DOTween.Sequence();
			if (m_Steps == null || i_Transform == null) return sequence;
			foreach (StepData step in m_Steps)
				sequence.Append(i_Transform.DOScale(step.Scale * i_ScaleMult, step.Time).SetEase(step.Ease).SetDelay(step.Delay));
			return sequence;
		}

		public void SetInitialStepScale(float i_Value)
		{
			if (m_Steps != null && m_Steps.Count > 0 && m_Steps[0] != null)
				m_Steps[0].Scale = Vector3.one * i_Value;
		}
	}
}
