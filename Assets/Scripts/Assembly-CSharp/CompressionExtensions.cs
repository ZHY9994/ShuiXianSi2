using System.IO;
using ICSharpCode.SharpZipLib.BZip2;

public static class CompressionExtensions
{
	public static byte[] CompressBytes(this byte[] data)
	{
		if (data == null || data.Length == 0)
			return null;
		using (MemoryStream stream = new MemoryStream())
		{
			using (BinaryWriter writer = new BinaryWriter(stream))
			{
				writer.Write(data.Length);
				using (BZip2OutputStream compressor = new BZip2OutputStream(stream))
					compressor.Write(data, 0, data.Length);
			}
			return stream.ToArray();
		}
	}

	public static string CompressBytesToString(this byte[] data)
	{
		// [未翻译] IDA 中未生成该泛型/包装函数实体，无法按唯一真相源核实调用链。
		return null;
	}

	public static string CompressString(this string sBuffer)
	{
		// [未翻译] IDA 中未生成该泛型/包装函数实体，无法按唯一真相源核实调用链。
		return null;
	}

	public static byte[] CompressStringToBytes(this string sBuffer)
	{
		// [未翻译] IDA 中未生成该泛型/包装函数实体，无法按唯一真相源核实调用链。
		return null;
	}

	public static byte[] DecompressBytes(this byte[] data)
	{
		if (data == null || data.Length == 0)
			return null;
		using (MemoryStream stream = new MemoryStream(data))
		using (BinaryReader reader = new BinaryReader(stream))
		{
			int length = reader.ReadInt32();
			byte[] result = new byte[length];
			using (BZip2InputStream decompressor = new BZip2InputStream(stream))
				decompressor.Read(result, 0, length);
			return result;
		}
	}

	public static byte[] DecompressBytesFromString(this string data)
	{
		// [未翻译] IDA 中未生成该泛型/包装函数实体，无法按唯一真相源核实调用链。
		return null;
	}

	public static string DecompressStringFromBytes(this byte[] sBuffer)
	{
		return UnityExtensions.UnityBytesToString(DecompressBytes(sBuffer));
	}

	public static string DecompressString(this string sBuffer)
	{
		return UnityExtensions.UnityBytesToString(DecompressBytes(UnityExtensions.UnityStringToBytes(sBuffer)));
	}
}
