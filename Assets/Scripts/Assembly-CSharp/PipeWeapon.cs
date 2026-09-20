using MrWayne;

public class PipeWeapon : BeatingWeapon
{
	public override eWeaponType Type => eWeaponType.Pipe;

	protected override void onHitBody(UnityEngine.Collision i_Other)
	{
		if (m_HitBodyPart != null && m_HitBodyPart.Physics != null && m_Unit != null && m_HitBodyPart.Physics.UnitType == m_Unit.UnitType)
		{
			return;
		}
		base.onHitBody(i_Other);
	}
}
