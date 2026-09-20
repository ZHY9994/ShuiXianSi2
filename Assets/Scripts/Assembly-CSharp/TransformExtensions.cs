using UnityEngine;

public static class TransformExtensions
{
	public static Transform FindDeepChild(this Transform root, string childName)
	{
		if (root == null) return null;
		if (root.name == childName) return root;
		foreach (Transform child in root)
		{
			Transform result = child.FindDeepChild(childName);
			if (result != null) return result;
		}
		return null;
	}

	public static T FindDeepChild<T>(this Transform root, string childName) where T : Component
	{
		Transform result = root.FindDeepChild(childName);
		return result == null ? null : result.GetComponent<T>();
	}
}
