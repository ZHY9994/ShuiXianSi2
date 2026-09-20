using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class UnityExtensions
{
	public static string ToV3String(this Vector3 v3)
	{
		return string.Format("{0}, {1}, {2}", v3.x, v3.y, v3.z);
	}

	public static Vector3 ZeroY(this Vector3 v3)
	{
		v3.y = 0f;
		return v3;
	}

	public static Vector3 RotateAroundY(this Vector3 v3, Vector3 goV3)
	{
		v3.y = goV3.y;
		return v3;
	}

	public static string ToStringParentHierarchy(this GameObject go)
	{
		if (go == null) return string.Empty;
		string parent = go.transform.parent == null ? string.Empty : go.transform.parent.gameObject.ToStringParentHierarchy();
		if (string.IsNullOrEmpty(parent)) return string.Format("{0}", go.name);
		return string.Format("{0}{1}", string.Format("{0} > ", parent), go.name);
	}

	public static T LoadFromResources<T>(this string resourcePath, bool errorOnNull) where T : UnityEngine.Object
	{
		return Resources.Load<T>(resourcePath);
	}

	public static byte[] UnityStringToBytes(this string source)
	{
		if (string.IsNullOrEmpty(source)) return null;
		MemoryStream stream = new MemoryStream();
		StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
		writer.Write(source);
		writer.Close();
		byte[] result = stream.ToArray();
		writer.Dispose();
		stream.Dispose();
		return result;
	}

	public static string UnityBytesToString(this byte[] source)
	{
		if (source.IsNullOrEmpty()) return string.Empty;
		using (MemoryStream stream = new MemoryStream(source))
		using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
		{
			return reader.ReadToEnd();
		}
	}

	public static Color WithAlpha(this Color color, float alpha)
	{
		color.a = alpha;
		return color;
	}

	public static List<OutputType> UnityConvertAll<InputType, OutputType>(this List<InputType> inputList, Func<InputType, OutputType> converter)
	{
		if (inputList == null)
			return null;
		List<OutputType> result = new List<OutputType>(inputList.Count);
		for (int i = 0; i < inputList.Count; i++)
			result.Add(converter(inputList[i]));
		return result;
	}
}
