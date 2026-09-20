namespace MrWayne
{
	public class AIUnit : UnitBase
	{
		public override eUnityType UnitType => eUnityType.AI;

		public override void UnitVisibility(bool i_Value)
		{
			base.UnitVisibility(i_Value);
		}
	}
}
