using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskExtension
{
	public static LayerMask Create(params string[] layerNames)
	{
		return NamesToMask(layerNames);
	}

	public static LayerMask Create(params int[] layerNumbers)
	{
		return LayerNumbersToMask(layerNumbers);
	}

	public static LayerMask NamesToMask(params string[] layerNames)
	{
		LayerMask mask = 0;
		foreach (string name in layerNames)
			mask |= 1 << LayerMask.NameToLayer(name);
		return mask;
	}

	public static LayerMask LayerNumbersToMask(params int[] layerNumbers)
	{
		LayerMask mask = 0;
		foreach (int layer in layerNumbers)
			mask |= 1 << layer;
		return mask;
	}

	public static LayerMask Inverse(this LayerMask original)
	{
		return ~original;
	}

	public static LayerMask AddToMask(this LayerMask original, params string[] layerNames)
	{
		return original | NamesToMask(layerNames);
	}

	public static LayerMask RemoveFromMask(this LayerMask original, params string[] layerNames)
	{
		return ~(~original | NamesToMask(layerNames));
	}

	public static string[] MaskToNames(this LayerMask original)
	{
		List<string> names = new List<string>();
		for (int i = 0; i < 32; i++)
		{
			if ((original.value & (1 << i)) != 0)
			{
				string name = LayerMask.LayerToName(i);
				if (!string.IsNullOrEmpty(name)) names.Add(name);
			}
		}
		return names.ToArray();
	}

	public static string MaskToString(this LayerMask original)
	{
		return string.Join(", ", original.MaskToNames());
	}

	public static string MaskToString(this LayerMask original, string delimiter)
	{
		return string.Join(delimiter, original.MaskToNames());
	}

	public static bool IsInLayerMask(this GameObject obj, LayerMask mask)
	{
		return (mask.value & (1 << obj.layer)) != 0;
	}
}
