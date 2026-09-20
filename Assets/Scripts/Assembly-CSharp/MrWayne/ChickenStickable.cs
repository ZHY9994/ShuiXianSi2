namespace MrWayne
{
	public class ChickenStickable : AnimalStickable
	{
		protected override eWeaponType Type => eWeaponType.Chicken;

		protected override ePoolType PoolType => ePoolType.ChickenStickable;
	}
}
