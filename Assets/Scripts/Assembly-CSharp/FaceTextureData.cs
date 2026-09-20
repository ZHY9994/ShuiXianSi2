using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "MrWayne/FaceData", fileName = "FaceData")]
public class FaceTextureData : ScriptableObject
{
	[SerializeField]
	private List<Texture> m_Happy;

	[SerializeField]
	private List<Texture> m_Angry;

	[SerializeField]
	private List<Texture> m_Death;

	public Texture GetRandomFace(eFaceType i_Type)
	{
		List<Texture> list = i_Type switch
		{
			eFaceType.Death => m_Death,
			eFaceType.Angry => m_Angry,
			eFaceType.Happy => m_Happy,
			_ => throw new SwitchExpressionException(i_Type)
		};
		return list[Random.Range(0, list.Count)];
	}
}
