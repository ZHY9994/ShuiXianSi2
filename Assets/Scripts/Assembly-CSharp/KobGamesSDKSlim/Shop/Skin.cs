using UnityEngine;

namespace KobGamesSDKSlim.Shop
{
	public class Skin : SkinSelectorItem
	{
		[SerializeField]
		private SkinnedMeshRenderer[] m_SkinMesh;

		public void AssignSkin(SkinnedMeshRenderer i_TargetSkin, Material i_Mat, int i_Layer)
		{
			if (i_TargetSkin == null)
				return;
			for (int i = 0; i < m_SkinMesh.Length; i++)
			{
				SkinnedMeshRenderer skinMesh = m_SkinMesh[i];
				skinMesh.bones = i_TargetSkin.bones;
				skinMesh.sharedMaterial = i_Mat;
				skinMesh.gameObject.layer = i_Layer;
			}
		}

		private void SetRefs()
		{
			m_SkinMesh = GetComponentsInChildren<SkinnedMeshRenderer>();
		}

		public Skin()
		{
		}
	}
}
