using UnityEngine;

public class Reskinner : MonoBehaviour
{
	[SerializeField]
	private SkinnedMeshRenderer m_RefSkin;

	private void reassign()
	{
		SkinnedMeshRenderer skin = GetComponent<SkinnedMeshRenderer>();
		skin.rootBone = m_RefSkin.rootBone;
		skin.bones = m_RefSkin.bones;
	}

	public void Setup(SkinnedMeshRenderer i_Skin)
	{
		m_RefSkin = i_Skin;
		reassign();
	}
}
