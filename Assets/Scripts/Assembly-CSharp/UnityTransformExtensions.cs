using UnityEngine;

public static class UnityTransformExtensions
{
	public static void SetPositionX(this Transform t, float newX)
	{
		Vector3 position = t.position;
		position.x = newX;
		t.position = position;
	}

	public static void SetPositionLocalX(this Transform t, float newX)
	{
		Vector3 position = t.localPosition;
		position.x = newX;
		t.localPosition = position;
	}

	public static Vector3 SetPositionX(this Vector3 v3, float newX)
	{
		v3.x = newX;
		return v3;
	}

	public static void SetPositionY(this Transform t, float newY)
	{
		Vector3 position = t.position;
		position.y = newY;
		t.position = position;
	}

	public static void SetPositionLocalY(this Transform t, float newY)
	{
		Vector3 position = t.localPosition;
		position.y = newY;
		t.localPosition = position;
	}

	public static Vector3 SetPositionY(this Vector3 v3, float newY)
	{
		v3.y = newY;
		return v3;
	}

	public static void SetPositionZ(this Transform t, float newZ)
	{
		Vector3 position = t.position;
		position.z = newZ;
		t.position = position;
	}

	public static void SetPositionLocalZ(this Transform t, float newZ)
	{
		Vector3 position = t.localPosition;
		position.z = newZ;
		t.localPosition = position;
	}

	public static Vector3 SetPositionZ(this Vector3 v3, float newZ)
	{
		v3.z = newZ;
		return v3;
	}

	public static float GetPositionX(this Transform t)
	{
		return t.position.x;
	}

	public static float GetPositionY(this Transform t)
	{
		return t.position.y;
	}

	public static float GetPositionZ(this Transform t)
	{
		return t.position.z;
	}
}
