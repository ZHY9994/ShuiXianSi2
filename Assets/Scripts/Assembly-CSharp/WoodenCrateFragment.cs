using DG.Tweening;
using KobGamesSDKSlim;
using MrWayne;
using UnityEngine;

public class WoodenCrateFragment : WeaponFragment
{
	private Tween m_HideTween;

	private new GamePlayVariablesEditor.ObstacleParameter.WoodenCrateParameters m_Parameters => GameConfig.Instance.GamePlay.ObstacleParameters.WoodenCrate;

	private void OnCollisionEnter(Collision i_Other)
	{
		if (m_State == eState.Finished)
		{
			return;
		}
		if (i_Other.gameObject.CompareTag("BodyPart") || i_Other.gameObject.CompareTag("EnemyBodyPart"))
		{
			m_State = eState.Finished;
			executeAction(i_Other);
		}
	}

	private void hide()
	{
		if (m_HideTween != null)
		{
			m_HideTween.Kill();
		}
		transform.DOScale(0.05f, 0.3f).OnComplete(() =>
		{
			RB.isKinematic = true;
			gameObject.SetActive(false);
		});
	}

	protected override void executeAction(Collision i_Other)
	{
		UnitBodyPart bodyPart = WayneStaticFunctions.GetComponentNoAlloc<UnitBodyPart>(i_Other.gameObject);
		if (bodyPart == null)
		{
			return;
		}
		ContactPoint contact = i_Other.GetContact(0);
		GameObject bloodSplat = PoolManager.Instance.Dequeue(ePoolType.BloodSplat, contact.point);
		if (bloodSplat == null)
		{
			return;
		}
		bloodSplat.transform.rotation = Quaternion.LookRotation(contact.normal);
		Vector3 normal = contact.normal;
		float magnitude = normal.magnitude;
		Vector3 direction = magnitude <= 0.00001f ? Vector3.zero : -normal / magnitude * m_Parameters.ShankPushForce;
		bodyPart.UnpinBodyPart(5f, direction, contact.point, 10f, eInteractableType.CrateShank);
	}

	public override void LaunchFragment(Vector3 i_Velocity, Vector3 i_Torque, WeaponBase.DamageProfile i_Profile)
	{
		m_WeaponDamageProfile = i_Profile;
		m_State = eState.Launched;
		addForce(i_Velocity);
		addTorque(i_Torque);
		Invoke(nameof(hide), m_Parameters.MinMaxShankHideDuration.RandomValue());
	}
}
