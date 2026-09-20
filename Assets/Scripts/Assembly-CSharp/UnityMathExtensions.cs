using UnityEngine;

public static class UnityMathExtensions
{
	public static float DistanceTo(this GameObject go, GameObject otherGO)
	{
		return Vector3.Distance(go.transform.position, otherGO.transform.position);
	}

	public static float DistanceTo(this GameObject go, Vector3 pos)
	{
		return Vector3.Distance(go.transform.position, pos);
	}

	public static float DistanceTo(this Vector3 start, Vector3 dest)
	{
		return Vector3.Distance(start, dest);
	}

	public static float DistanceTo(this Transform start, Transform dest)
	{
		return Vector3.Distance(start.position, dest.position);
	}

	public static Vector3 Add(this Vector3 v3, Vector3 value)
	{
		return new Vector3(v3.x + value.x, v3.y + value.y, v3.z + value.z);
	}

	public static Vector3 Add(this Vector3 v3, float x, float y, float z)
	{
		return new Vector3(v3.x + x, v3.y + y, v3.z + z);
	}

	public static Vector3 Subtract(this Vector3 v3, Vector3 value)
	{
		return new Vector3(v3.x - value.x, v3.y - value.y, v3.z - value.z);
	}

	public static Vector3 Subtract(this Vector3 v3, float x, float y, float z)
	{
		return new Vector3(v3.x - x, v3.y - y, v3.z - z);
	}
}
