using MrWayne;
using UnityEngine;

public class BottleWeapon : BeatingWeapon
{
	public override eWeaponType Type => eWeaponType.Bottle;

	protected override void onHitBody(Collision i_Other)
	{
		base.onHitBody(i_Other);
		bottleHit(i_Other);
	}

	protected override void onHitElement(Collision i_Other)
	{
		sfx(WeaponEffectControllers.AudioController.eSFXTypeWeapon.Hit);
		haptic(WeaponEffectControllers.HapticController.eHapticWeapon.Hit);
		bottleHit(i_Other);
	}

	private void bottleHit(Collision i_Other)
	{
		if (i_Other != null)
		{
			ContactPoint contact = i_Other.GetContact(0);
			GameObject fractureObject = KobGamesSDKSlim.PoolManager.Instance.Dequeue(ePoolType.BottleFracture, contact.point);
			if (fractureObject == null)
			{
				actionfinished();
				returnToQueue();
				return;
			}
			Fractured fractured = fractureObject.GetComponent<Fractured>();
			if (fractured != null)
			{
				vFX(WeaponEffectControllers.VFXController.eVFXWeapon.WallHit, contact.point);
				fractured.ResetFracture();
				fractured.EnableShardDamage(m_Unit);
				fractured.ExplodeWithDirection(contact.normal.normalized, new Vector2(2f, 7f));
			}
		}
		actionfinished();
		returnToQueue();
	}
}
