using System;
using RootMotion.Dynamics;
using UnityEngine;

namespace MrWayne
{
	[Serializable]
	public class GeneralUnitParameters
	{
		public float HealthPerLevel = 30f;

		public bool UseIK = true;

		public float IKWeight = 0.45f;

		public float BodyWeight = 1f;

		public float HeadWeight = 1f;

		public BehaviourBase.AnimatorEvent[] DefaultHitEvent = new BehaviourBase.AnimatorEvent[]
		{
			new BehaviourBase.AnimatorEvent { animationState = "Fall", crossfadeTime = 0.7f }
		};

		public LayerMask GroundLayer;

		public float YOffset = 0.3f;

		public float RayMaxDistance = 0.35f;

		public eTags EnabledTag;

		public eTags DisabledTag;

		[SerializeField]
		private FaceDataEditor m_Face;

		public FaceDataEditor Face => m_Face;

		public SkinOutlineData Outlines;

		public bool UseOutline;

		public float OutlineWidth = 20f;

		public float DamageIncreaseLevelsPlayer;

		public float DamageIncreaseLevelsAI;

		public Texture GetRandomFace(string i_Name, eFaceType i_Type)
		{
			return m_Face.GetFace(i_Name, i_Type);
		}

	}
}
