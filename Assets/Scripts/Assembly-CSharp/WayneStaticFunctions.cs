using System.Collections.Generic;
using KobGamesSDKSlim;
using KobGamesSDKSlim.Shop;
using UnityEngine;
using Wayne.Game.Shops;
using Wayne.Game.Shops.Weapons;

public static class WayneStaticFunctions
{
	private static List<Component> m_ComponentCache = new List<Component>();

	public static T GetComponentNoAlloc<T>(this GameObject @this) where T : Component
	{
		@this.GetComponents(typeof(T), m_ComponentCache);
		T result = (m_ComponentCache.Count > 0) ? (m_ComponentCache[0] as T) : null;
		m_ComponentCache.Clear();
		return result;
	}

	public static List<T> MergeLists<T>(List<T> i_ListA, List<T> i_ListB)
	{
		i_ListB.AddRange(i_ListA);
		return i_ListB;
	}

	public static Dictionary<T1, T2> Merge2Dictionaries<T1, T2>(Dictionary<T1, T2> dicA, Dictionary<T1, T2> dicb)
	{
		foreach (KeyValuePair<T1, T2> pair in dicA)
			dicb.Add(pair.Key, pair.Value);
		return dicA;
	}

	public static Vector3 Vector2To3(this Vector2 i_Self)
	{
		return new Vector3(i_Self.x, 0f, i_Self.y);
	}

	public static Vector3 AngleToVector3(float angle)
	{
		return new Vector3(0f, Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
	}

	public static float Vector3ToAngle(float x, float y)
	{
		return Mathf.Atan2(y, x);
	}

	public static void Shuffle2<T>(this IList<T> ts)
	{
		for (int i = 0; i < ts.Count - 1; i++)
		{
			int j = Random.Range(i, ts.Count);
			T value = ts[i];
			ts[i] = ts[j];
			ts[j] = value;
		}
	}

	public static float Clamp01(float value)
	{
		if (value < 0f)
			return 0f;
		if (value > 1f)
			return 1f;
		return value;
	}

	public static float InverseLerp(float a, float b, float value)
	{
		return (value - a) / (b - a);
	}

	public static float InverseLerpClamped(float a, float b, float value)
	{
		return Clamp01(InverseLerp(a, b, value));
	}

	public static float Lerp(float a, float b, float t)
	{
		return b * t + (1f - t) * a;
	}

	public static float Eerp(float a, float b, float t)
	{
		return Mathf.Pow(a, 1f - t) * Mathf.Pow(b, t);
	}

	public static float InverseEerp(float a, float b, float v)
	{
		return Mathf.Log(a / v) / Mathf.Log(a / b);
	}

	public static Vector2 Lerp(Vector2 a, Vector2 b, Vector2 t)
	{
		return new Vector2(Lerp(a.x, b.x, Clamp01(t.x)), Lerp(a.y, b.y, Clamp01(t.y)));
	}

	public static Vector2 InverseLerp(Vector2 a, Vector2 b, Vector2 v)
	{
		return new Vector2(InverseLerp(a.x, b.x, v.x), InverseLerp(a.y, b.y, v.y));
	}

	public static Vector2 Remap(Rect iRect, Rect oRect, Vector2 iPos)
	{
		return Remap(new Vector2(iRect.xMin, iRect.yMin), new Vector2(iRect.xMax, iRect.yMax),
			new Vector2(oRect.xMin, oRect.yMin), new Vector2(oRect.xMax, oRect.yMax), iPos);
	}

	public static Vector2 Remap(Vector2 iMin, Vector2 iMax, Vector2 oMin, Vector2 oMax, Vector2 value)
	{
		Vector2 t = InverseLerp(iMin, iMax, value);
		return new Vector2(Lerp(oMin.x, oMax.x, Clamp01(t.x)), Lerp(oMin.y, oMax.y, Clamp01(t.y)));
	}

	public static float Remap(float iMin, float iMax, float oMin, float oMax, float value)
	{
		return Lerp(oMin, oMax, InverseLerp(iMin, iMax, value));
	}

	public static float RemapClamped(float iMin, float iMax, float oMin, float oMax, float value)
	{
		return Lerp(oMin, oMax, InverseLerpClamped(iMin, iMax, value));
	}

	public static float DistanceSquare(Vector3 i_Pos01, Vector3 i_Pos02)
	{
		Vector3 delta = i_Pos01 - i_Pos02;
		return delta.x * delta.x + delta.y * delta.y + delta.z * delta.z;
	}

	public static WeaponShopItem.eUnlockState GetUnlockState(this WeaponItemData i_Item)
	{
		if (i_Item == null)
			throw new System.ArgumentNullException(nameof(i_Item));

		var upgradeData = i_Item.UpgradeData;
		if (i_Item.IsMaxed || upgradeData == null)
			return WeaponShopItem.eUnlockState.Maxed;

		if (i_Item.IsMaxed || i_Item.IsUpgradeable)
		{
			if (!i_Item.IsMaxed && i_Item.IsUpgradeable && StorageManager.Instance.CoinsAmount >= upgradeData.Price)
				return WeaponShopItem.eUnlockState.UpgradableByCash;

			if (i_Item.IsMaxed || !i_Item.IsUpgradeable)
				return i_Item.IsMaxed ? WeaponShopItem.eUnlockState.Maxed : WeaponShopItem.eUnlockState.None;

			return (StorageManager.Instance.CoinsAmount < upgradeData.Price)
				? WeaponShopItem.eUnlockState.UpgradableByRV
				: (i_Item.IsMaxed ? WeaponShopItem.eUnlockState.Maxed : WeaponShopItem.eUnlockState.None);
		}

		return WeaponShopItem.eUnlockState.NotUpgradeable;
	}

	public static string ColorToHex(this Color i_Color)
	{
		Color32 color = i_Color;
		return "#" + color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
	}

	public static string GetBodyPartName(this eBodyPart i_Part)
	{
		// [未翻译] IDA exposes an indexed native string table, but the referenced
		// string objects are not available as readable binary strings.
		return string.Empty;
	}

	public static Vector3 GetParabolaPosition(Vector3 i_InitialPosition, Vector3 i_Velocity, float i_Time, float i_Spacing)
	{
		float time = i_Time * i_Spacing;
		float fixedTime = Time.fixedDeltaTime;
		return new Vector3(0f,
			i_InitialPosition.y + time * (i_Velocity.y * fixedTime) + time * time * (Physics2D.gravity.y * 0.5f * fixedTime * fixedTime),
			i_InitialPosition.z + time * (i_Velocity.z * fixedTime));
	}
}
