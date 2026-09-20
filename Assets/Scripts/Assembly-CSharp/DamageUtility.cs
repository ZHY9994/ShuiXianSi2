using MrWayne;
using KobGamesSDKSlim;
using UnityEngine;

public static class DamageUtility
{
	public static float GetDamage(float i_BaseDamage, float i_Velocity, eDamageablePart i_Part, UnitBase i_Unit)
	{
		float multiplier = BodyPartDamage(i_Part, i_Unit.UnitType);
		AnimationCurve curve = GameConfig.Instance.GamePlay.WeaponData.General.VelocityCurve;
		return Mathf.Round(multiplier * curve.Evaluate(i_Velocity) * i_BaseDamage);
	}

	public static float GetDamageWithStick(float i_BaseDamage, float i_Velocity, eDamageablePart i_Part, float i_StickMultiplier, UnitBase i_Unit)
	{
		float multiplier = BodyPartDamage(i_Part, i_Unit.UnitType);
		return Mathf.Round(multiplier * i_StickMultiplier * i_BaseDamage);
	}

	public static float GetExplosiveDamageWithCurve(float i_Distance, float i_Radius, int i_ExplosiveDamage)
	{
		float normalizedDistance = i_Radius == 0f ? 0f : Mathf.Clamp01(i_Distance / i_Radius);
		AnimationCurve curve = GameConfig.Instance.GamePlay.WeaponData.General.ExplosiveCurve;
		return Mathf.Round(curve.Evaluate(normalizedDistance) * i_ExplosiveDamage);
	}

	public static float GetMaxCurveValue(this AnimationCurve i_Curve)
	{
		return i_Curve.keys[i_Curve.length - 1].value;
	}

	private static float BodyPartDamage(eDamageablePart i_Part, eUnityType i_UnitType)
	{
		WeaponDataEditor.GeneralWeaponDataEditor general = GameConfig.Instance.GamePlay.WeaponData.General;
		switch (i_Part)
		{
			case eDamageablePart.Head:
				return i_UnitType == eUnityType.Player ? general.HeadShotMultiplierPlayer : general.HeadShotMultiplierAI;
			case eDamageablePart.Body:
				return general.BodyShotMultiplier;
			case eDamageablePart.Limb:
				return general.LimbShotMultiplier;
			default:
				return 0f;
		}
	}
}
