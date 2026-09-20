using System;

[Serializable]
public class BackGroundData
{
	public BGPartData[] PartDatas;

	public float GetPartSpeedCoefficient(Background.eBackgroundPart i_Part)
	{
		float result = 0f;
		for (int i = 0; i < PartDatas.Length; i++)
		{
			BGPartData partData = PartDatas[i];
			if (partData.Part == i_Part)
			{
				return partData.SpeedCoefficient;
			}
		}
		return result;
	}

	public BackGroundData()
	{
	}
}
