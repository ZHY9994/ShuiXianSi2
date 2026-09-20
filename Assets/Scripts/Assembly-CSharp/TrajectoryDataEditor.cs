using System;
using UnityEngine;

[Serializable]
public class TrajectoryDataEditor
{
	[Serializable]
	public class TrajectoryParameter
	{
		public int DotCount;

		public float DotSpacing;

		public Vector2 ScaleMinMax;

		public Gradient DotColor;

		public Gradient PrevDotColor;
	}

	[Serializable]
	public class TutorialData
	{
		[Serializable]
		public class Data
		{
			public string Name = "Data";

			public Vector3 PushDirection;

			public float Offset;

		}

		[Range(0f, 1f)]
		public float CorrectThreshold = 0.8f;

		public Vector2 ScaleMinMax;

		public Gradient CorrectGradient;

		[SerializeField]
		private Data[] m_TutorialData;

		public Data GetData(int i_Index)
		{
			if (m_TutorialData == null)
				throw new NullReferenceException();
			if (m_TutorialData.Length <= i_Index)
				return null;
		return m_TutorialData[i_Index];
		}
	}

	public TrajectoryParameter General;

	public TutorialData Tutorial;
}
