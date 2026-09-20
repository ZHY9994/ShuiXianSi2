using System;

[Serializable]
public class RandomRangeValue
{
	public float Min;

	public float Max;

	public float RandomValue()
	{
		return UnityEngine.Random.Range(Min, Max);
	}
}
