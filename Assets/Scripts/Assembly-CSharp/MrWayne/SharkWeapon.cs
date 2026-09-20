namespace MrWayne
{
	public class SharkWeapon : AnimalWeapon
	{
		public override eWeaponType Type => eWeaponType.Shark;

		protected override ePoolType m_Stickable => ePoolType.SharkStickable;
	}
}
