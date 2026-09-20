using System.IO;
using System.Xml.Serialization;

public static class UnityXMLSerializationExtensions
{
	public static byte[] XMLSerialize_ToArray<T>(this T objToSerialize) where T : class
	{
		// [未翻译] IDA decompile/disasm both failed: no native symbol was present.
		return null;
	}

	public static string XMLSerialize_ToString<T>(this T objToSerialize) where T : class
	{
		// [未翻译] IDA decompile/disasm both failed: no native symbol was present.
		return null;
	}

	public static T XMLDeserialize_ToObject<T>(this string strSerial) where T : class
	{
		// [未翻译] IDA decompile/disasm both failed: no native symbol was present.
		return null;
	}

	public static T XMLDeserialize_ToObject<T>(byte[] objSerial) where T : class
	{
		if (objSerial.IsNullOrEmpty()) return null;
		using (MemoryStream stream = new MemoryStream(objSerial))
		{
			return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
		}
	}

	public static void XMLSerialize_AndSaveTo<T>(this T objToSerialize, string path) where T : class
	{
		// [未翻译] IDA decompile/disasm both failed: no native symbol was present.
	}

	public static void XMLSerialize_AndSaveToPersistentDataPath<T>(this T objToSerialize, string folderName, string filename) where T : class
	{
		// [未翻译] IDA decompile/disasm both failed: no native symbol was present.
	}

	public static void XMLSerialize_AndSaveToDataPath<T>(this T objToSerialize, string folderName, string filename) where T : class
	{
		// [未翻译] IDA decompile/disasm both failed: no native symbol was present.
	}

	public static T XMLDeserialize_AndLoadFrom<T>(this string path) where T : class
	{
		if (StringExtensions.IsNullOrEmpty(path) || !File.Exists(path)) return null;
		using (FileStream stream = new FileStream(path, FileMode.Open))
		{
			return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
		}
	}

	public static T XMLDeserialize_AndLoadFromPersistentDataPath<T>(this string filename, string folderName) where T : class
	{
		if (StringExtensions.IsNullOrEmpty(filename)) return null;
		string basePath = StringExtensions.IsNullOrEmpty(folderName) ? UnityEngine.Application.persistentDataPath : Path.Combine(UnityEngine.Application.persistentDataPath, folderName);
		return Path.Combine(basePath, filename).XMLDeserialize_AndLoadFrom<T>();
	}

	public static T XMLDeserialize_AndLoadFromDataPath<T>(this string filename, string folderName) where T : class
	{
		// [未翻译] IDA decompile/disasm both failed: no native symbol was present.
		return null;
	}
}
