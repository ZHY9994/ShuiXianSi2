using System.Collections.Generic;
using KobGamesSDKSlim.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace Wayne.Game.Utils
{
	public static class ProjectUtils
	{
		private static readonly Dictionary<eItemUnlockMethod, Color> m_UnlockColors = new Dictionary<eItemUnlockMethod, Color>
		{
			{ eItemUnlockMethod.Unlocked, Color.white },
			{ eItemUnlockMethod.AD, new Color(1f, 0.76f, 0.74f, 1f) },
			{ eItemUnlockMethod.IAP, new Color(0.52f, 0.7f, 1f, 1f) },
			{ eItemUnlockMethod.Cash, new Color(1f, 0.95f, 0.71f, 1f) }
		};
		private static readonly Dictionary<eItemRarity, Color> m_RarityColors = new Dictionary<eItemRarity, Color>
		{
			{ eItemRarity.Good, new Color(0.89f, 1f, 0.1f, 1f) },
			{ eItemRarity.Rare, new Color(0.22f, 0.75f, 1f, 1f) },
			{ eItemRarity.Epic, new Color(0.76f, 0.36f, 0.98f, 1f) },
			{ eItemRarity.Legendary, new Color(1f, 0.75f, 0.23f, 1f) }
		};

		public static int GetRandomValue(this Vector2Int i_Self) => Random.Range(i_Self.x, i_Self.y);

		public static Vector3 GetFillImageWorldPosition(this Image i_Self)
		{
			if (i_Self != null && i_Self.type == Image.Type.Filled && i_Self.fillMethod == Image.FillMethod.Vertical)
			{
				RectTransform rectTransform = i_Self.rectTransform;
				if (rectTransform != null)
				{
					Rect rect = rectTransform.rect;
					return rectTransform.TransformPoint(new Vector3(0f, rect.yMin + i_Self.fillAmount * rect.height, 0f));
				}
			}
			return i_Self.rectTransform.position;
		}

		public static float GetImageFillOnWorldPosition(Vector3 i_WorldPosition, Image i_Target)
		{
			RectTransform rectTransform = i_Target.rectTransform;
			float y = rectTransform.InverseTransformPoint(i_WorldPosition).y;
			Rect rect = rectTransform.rect;
			if (rect.yMin == rect.yMax) return 0f;
			return Mathf.Clamp01((y - rect.yMin) / (rect.yMax - rect.yMin));
		}

		public static Color GetUnlockMethodColor(eItemUnlockMethod i_Method)
		{
			return m_UnlockColors.TryGetValue(i_Method, out Color color) ? color : Color.white;
		}

		public static Color GetRarityColor(eItemRarity i_Rarity)
		{
			return m_RarityColors.TryGetValue(i_Rarity, out Color color) ? color : Color.white;
		}

	}
}
