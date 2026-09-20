using KobGamesSDKSlim;
using UnityEngine;

namespace MrWayne
{
	public class AIUnitVisuals : UnitVisuals
	{
		[SerializeField]
		private SkinnedMeshRenderer m_BodyRenderer;

		[SerializeField]
		private MeshRenderer m_Face;

		private MaterialPropertyBlock m_FaceBlock;

		private MaterialPropertyBlock m_BodyBlock;

		protected override string m_SkinName => "Base";

		private void Awake()
		{
			m_FaceBlock = new MaterialPropertyBlock();
			m_BodyBlock = new MaterialPropertyBlock();
		}

		public override void Initialize(UnitSpawn i_UnitSpawn)
		{
			AISkinData skin = i_UnitSpawn == null ? null : i_UnitSpawn.AISkin;
			Color color = skin != null && skin.SkinType == eAISkinType.Default ? skin.Color : Color.white;
			setBloodColor(AuditBloodColor);
			updateBodyColor(color);
			changeFace(eFaceType.Happy);
		}

		protected override void onUnitDeath(UnitBase i_Unit)
		{
			base.onUnitDeath(i_Unit);
			if (m_UnitBase == null || m_UnitBase.Health == null || m_UnitBase.Health.Health > 0f)
				return;
			AIVisualData visuals = GameConfig.Instance == null ? null : GameConfig.Instance.GamePlay.UnitData.AI.Visuals;
			if (visuals != null)
				updateBodyColor(visuals.DeathColor);
		}

		private void updateBodyColor(Color i_Color)
		{
			if (m_BodyRenderer == null || m_BodyBlock == null) return;
			m_BodyBlock.SetColor(StaticString.SP_AlbedoColor, i_Color);
			m_BodyRenderer.SetPropertyBlock(m_BodyBlock);
		}

		protected override void changeFace(eFaceType i_FaceType)
		{
			if (m_Face == null || m_FaceBlock == null || m_GeneralParameter == null || m_GeneralParameter.Face == null) return;
			Texture face = m_GeneralParameter.Face.GetFace(m_SkinName, i_FaceType);
			m_FaceBlock.SetTexture(StaticString.SP_MainTex, face);
			m_Face.SetPropertyBlock(m_FaceBlock);
		}
	}
}
