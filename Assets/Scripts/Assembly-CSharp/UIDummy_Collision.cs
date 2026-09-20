using RootMotion.Dynamics;

public class UIDummy_Collision : BehaviourBase
{
	public override void OnReactivate()
	{
	}

	protected override void OnMuscleCollisionBehaviour(MuscleCollision collision)
	{
		base.OnMuscleCollisionBehaviour(collision);
		print("Thing");
	}
}
