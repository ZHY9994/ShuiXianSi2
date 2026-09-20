using System.Security.Cryptography;
using System;
using System.IO;

public static class CryptoExtensions
{
	private static readonly RNGCryptoServiceProvider RandomGenerator;

	private static readonly byte[] DefaultPublicKey;

	private const string DefaultPublicKey2Str = "BKkAqIak8aDpBa932dVRT;tDea0o0rucMMQlUxvdl-aKs6jjIgliklawgodwEJuZeImYvvVYZKNheInnLhEenfsIgvdX2bcr2cjr";

	private static byte[] m_PublicKey;

	private static string m_PublicKey2Str;

	static CryptoExtensions()
	{
		RandomGenerator = new RNGCryptoServiceProvider();
		// [未翻译] IDA initializes the 1024-byte array through RuntimeHelpers.InitializeArray;
		// the referenced metadata blob is not exposed as readable data by the current MCP.
		DefaultPublicKey = new byte[1024];
		m_PublicKey2Str = DefaultPublicKey2Str;
	}

	public static byte[] PublicKey
	{
		get
		{
			return m_PublicKey ?? DefaultPublicKey;
		}
		set
		{
			if (value == null || value.Length == 0)
				m_PublicKey = DefaultPublicKey;
			else
			{
				if (value.Length != 1024)
					throw new ArgumentOutOfRangeException(nameof(value));
				m_PublicKey = value;
			}
		}
	}

	public static string PublicKey2Str
	{
		get
		{
			return string.IsNullOrEmpty(m_PublicKey2Str) ? DefaultPublicKey2Str : m_PublicKey2Str;
		}
		set
		{
			m_PublicKey2Str = string.IsNullOrEmpty(value) ? DefaultPublicKey2Str : value;
		}
	}

	public static int RandomRangeInclusive(this int minimumValue, int maximumValue)
	{
		byte[] value = new byte[1];
		RandomGenerator.GetBytes(value);
		return (int)Math.Floor(Math.Max(0d, value[0] / 255d - 1e-11d) * (maximumValue - minimumValue + 1)) + minimumValue;
	}

	private static void BuildAESKeys(AesManaged RCrypto, string keyStr, byte[] key)
	{
		RCrypto.BlockSize = 128;
		RCrypto.KeySize = 256;
		using (var derive = new Rfc2898DeriveBytes(keyStr, key))
		{
			RCrypto.Key = derive.GetBytes(32);
			derive.Reset();
			RCrypto.IV = derive.GetBytes(16);
		}
	}

	public static byte[] EncryptStringWithAES(this string source)
	{
		return source.EncryptStringWithAES(PublicKey2Str, PublicKey);
	}

	public static byte[] EncryptStringWithAES(this string source, string keyStr, byte[] key)
	{
		return string.IsNullOrEmpty(source) ? null : UnityExtensions.UnityStringToBytes(source).EncryptWithAES(keyStr, key);
	}

	public static byte[] EncryptWithAES(this byte[] source)
	{
		return source.EncryptWithAES(PublicKey2Str, PublicKey);
	}

	public static byte[] EncryptWithAES(this byte[] source, string keyStr, byte[] key)
	{
		if (source == null || source.Length == 0) return null;
		using (var aes = new AesManaged())
		{
			BuildAESKeys(aes, keyStr, key);
			using (var output = new MemoryStream())
			using (var crypto = new CryptoStream(output, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
			{
				crypto.Write(source, 0, source.Length);
				crypto.FlushFinalBlock();
				return output.ToArray();
			}
		}
	}

	public static byte[] DecryptWithAES(this byte[] source)
	{
		return source.DecryptWithAES(PublicKey2Str, PublicKey);
	}

	public static byte[] DecryptWithAES(this byte[] source, string keyStr, byte[] key)
	{
		if (source == null || source.Length == 0) return null;
		using (var aes = new AesManaged())
		{
			BuildAESKeys(aes, keyStr, key);
			using (var input = new MemoryStream(source))
			using (var crypto = new CryptoStream(input, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Read))
			using (var output = new MemoryStream())
			{
				byte[] buffer = new byte[512];
				int read;
				while ((read = crypto.Read(buffer, 0, buffer.Length)) > 0)
					output.Write(buffer, 0, read);
				return output.ToArray();
			}
		}
	}

	public static string DecryptWithAESToString(this byte[] source)
	{
		return source.DecryptWithAESToString(PublicKey2Str, PublicKey);
	}

	public static string DecryptWithAESToString(this byte[] source, string keyStr, byte[] key)
	{
		return UnityExtensions.UnityBytesToString(source.DecryptWithAES(keyStr, key));
	}
}
