using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UnityGoExtensions
{
	public static T GetComponentOnObject<T>(this GameObject go, bool showErrorInConsole) where T : Component
	{
		return go.GetComponent<T>();
	}

	public static T GetComponentOnObject<T>(this Transform trans, bool showErrorInConsole) where T : Component
	{
		return trans.GetComponent<T>();
	}

	public static T GetComponentOnObjectOrParent<T>(this GameObject go, bool showErrorInConsole) where T : Component
	{
		return go.GetComponentInParent<T>();
	}

	public static bool IsNullOrInactive(this GameObject go)
	{
		return go == null || !go.activeSelf;
	}

	public static bool IsActive(this GameObject go)
	{
		return go != null && go.activeSelf;
	}

	public static void ActivateAndParent(this GameObject go)
	{
		if (go == null) return;
		if (go.transform.parent != null)
		{
			go.transform.parent.gameObject.ActivateAndParent();
		}
		go.SetActive(true);
	}

	public static bool HasRigidbody(this GameObject go)
	{
		return go.GetComponent<Rigidbody>() != null;
	}

	public static bool HasCharacterController(this GameObject go)
	{
		return go.GetComponent<CharacterController>() != null;
	}

	public static bool HasAnimation(this GameObject go)
	{
		return go.GetComponent<Animation>() != null;
	}

	public static bool HasComponent<T>(this GameObject go) where T : Component
	{
		return go.GetComponent<T>() != null;
	}

	public static void SetLayerRecursively(this GameObject go, int layer)
	{
		if (go == null) return;
		go.layer = layer;
		foreach (Transform child in go.transform)
		{
			child.gameObject.SetLayerRecursively(layer);
		}
	}

	public static void SetCollisionRecursively(this GameObject go, bool enabled)
	{
		if (go == null)
			return;
		Collider collider = go.GetComponent<Collider>();
		if (collider != null)
			collider.enabled = enabled;
		foreach (Transform child in go.transform)
			child.gameObject.SetCollisionRecursively(enabled);
	}

	public static List<T> GetComponentsInChildrenWithTag<T>(this GameObject go, string tag) where T : Component
	{
		List<T> result = new List<T>();
		if (go == null) return result;
		if (go.CompareTag(tag))
		{
			result.Add(go.GetComponent<T>());
		}
		foreach (Transform child in go.transform)
		{
			result.AddRange(child.gameObject.GetComponentsInChildrenWithTag<T>(tag));
		}
		return result;
	}

	public static int GetCollisionMask(this GameObject go, int layer = -1)
	{
		int layer1 = layer == -1 ? go.layer : layer;
		int mask = 0;
		for (int layer2 = 0; layer2 < 32; layer2++)
		{
			if (!Physics.GetIgnoreLayerCollision(layer1, layer2))
				mask |= 1 << layer2;
		}
		return mask;
	}

	public static Transform[] GetChildrenWithName(this GameObject go, string name)
	{
		return go.transform.Cast<Transform>().Where(x => x.name.Equals(name, System.StringComparison.CurrentCultureIgnoreCase)).ToArray();
	}

	public static T[] GetChildrenComponent<T>(this GameObject go) where T : Component
	{
		return go.transform.Cast<Transform>().Where(x => x.gameObject.HasComponent<T>()).Select(x => x.gameObject.GetComponent<T>()).ToArray();
	}

	public static T GetInterface<T>(this GameObject go) where T : class
	{
		// [未翻译] IDA decompile/disasm both failed: no native symbol was present.
		return null;
	}

	public static T GetInterfaceInChildren<T>(this GameObject go) where T : class
	{
		if (!typeof(T).IsInterface) return null;
		return go.GetComponentsInChildren<Component>().OfType<T>().FirstOrDefault();
	}

	public static IEnumerable<T> GetInterfaces<T>(this GameObject go) where T : class
	{
		if (!typeof(T).IsInterface) return Enumerable.Empty<T>();
		return go.GetComponents<Component>().OfType<T>();
	}

	public static IEnumerable<T> GetInterfacesInChildren<T>(this GameObject go) where T : class
	{
		if (!typeof(T).IsInterface) return Enumerable.Empty<T>();
		return go.GetComponentsInChildren<Component>(true).OfType<T>();
	}
}
