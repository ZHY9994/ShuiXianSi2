using UnityEngine;
using Wayne.Game.Shops;

namespace MrWayne
{
	public class PlayerVisuals : UnitVisuals
	{
		[SerializeField]
		private Material m_FaceRenderer;

		protected override string m_SkinName => SkinShopManager.Instance == null || SkinShopManager.Instance.SelectedItem == null ? string.Empty : SkinShopManager.Instance.SelectedItem.Name;

		public override void Initialize(UnitSpawn i_SpawnData)
		{
			setBloodColor(AuditBloodColor);
		}

		protected override void changeFace(eFaceType i_FaceType)
		{
			if (m_FaceRenderer == null || m_GeneralParameter == null || m_GeneralParameter.Face == null) return;
			Texture face = m_GeneralParameter.Face.GetFace(m_SkinName, i_FaceType);
			m_FaceRenderer.SetTexture(StaticString.SP_BaseTexture, face);
		}
	}
}
