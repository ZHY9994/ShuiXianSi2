using System;
using System.IO;
using UnityEngine;

public static class SerializationExtensions
{
	public static string ToStringSerial(this string str)
	{
		return str;
	}

	public static string ToStringSerial(this Guid? guid)
	{
		return guid.HasValue ? Convert.ToBase64String(guid.Value.ToByteArray()) : Convert.ToBase64String(Array.Empty<byte>());
	}

	public static string ToStringSerial(this Guid guid)
	{
		return Convert.ToBase64String(guid.ToByteArray());
	}

	public static Guid? GuidNullableFromSerialString(this string str)
	{
		if (string.IsNullOrEmpty(str))
			return null;
		return new Guid(Convert.FromBase64String(str));
	}

	public static Guid GuidFromSerialString(this string str)
	{
		return string.IsNullOrEmpty(str) ? default : new Guid(Convert.FromBase64String(str));
	}

	public static BinaryWriter WriteByteArray(this BinaryWriter bw, byte[] data)
	{
		if (data == null || data.Length == 0)
		{
			bw.Write(0);
			return bw;
		}
		bw.Write(data.Length);
		bw.Write(data);
		return bw;
	}

	public static byte[] ReadbByteArray(this BinaryReader bread)
	{
		int length = bread.ReadInt32();
		return length == 0 ? null : bread.ReadBytes(length);
	}

	public static BinaryWriter WriteByteArrayCompressed(this BinaryWriter bw, byte[] data)
	{
		return bw.WriteByteArray(CompressionExtensions.CompressBytes(data));
	}

	public static byte[] ReadbByteArrayCompressed(this BinaryReader bread)
	{
		return CompressionExtensions.DecompressBytes(bread.ReadbByteArray());
	}

	public static BinaryWriter WriteGuid(this BinaryWriter bw, Guid? guid)
	{
		bw.Write(guid.ToStringSerial());
		return bw;
	}

	public static BinaryWriter WriteGuid(this BinaryWriter bw, Guid guid)
	{
		bw.Write(guid.ToStringSerial());
		return bw;
	}

	public static Guid? ReadGuidNullable(this BinaryReader bread)
	{
		return bread.ReadString().GuidNullableFromSerialString();
	}

	public static Guid ReadGuid(this BinaryReader bread)
	{
		return bread.ReadString().GuidFromSerialString();
	}

	public static BinaryWriter WriteSerializedString(this BinaryWriter bw, string source)
	{
		bw.Write(source.ToStringSerial());
		return bw;
	}

	public static BinaryWriter WriteDateTime(this BinaryWriter bw, DateTime time)
	{
		bw.Write(time.ToUniversalTime().Ticks);
		return bw;
	}

	public static DateTime ReadDateTimeAsUTC(this BinaryReader bread)
	{
		return new DateTime(bread.ReadInt64());
	}

	public static DateTime ReadDateTimeAsLocalTime(this BinaryReader bread)
	{
		return new DateTime(bread.ReadInt64()).ToLocalTime();
	}

	public static BinaryWriter WriteInt(this BinaryWriter bw, int val)
	{
		bw.Write(val);
		return bw;
	}

	public static BinaryWriter WriteLong(this BinaryWriter bw, long val)
	{
		bw.Write(val);
		return bw;
	}

	public static BinaryWriter WriteFloat(this BinaryWriter bw, float val)
	{
		bw.Write(val);
		return bw;
	}

	public static BinaryWriter WriteVector3(this BinaryWriter bw, Vector3 v3)
	{
		bw.Write(v3.x);
		bw.Write(v3.y);
		bw.Write(v3.z);
		return bw;
	}

	public static BinaryWriter WriteVector2(this BinaryWriter bw, Vector2 v2)
	{
		bw.Write(v2.x);
		bw.Write(v2.y);
		return bw;
	}

	public static Vector3 ReadVector3(this BinaryReader bread)
	{
		return new Vector3(bread.ReadSingle(), bread.ReadSingle(), bread.ReadSingle());
	}

	public static Vector2 ReadVector2(this BinaryReader bread)
	{
		return new Vector2(bread.ReadSingle(), bread.ReadSingle());
	}

	public static BinaryWriter WriteBool(this BinaryWriter bw, bool val)
	{
		bw.Write(val);
		return bw;
	}
}
