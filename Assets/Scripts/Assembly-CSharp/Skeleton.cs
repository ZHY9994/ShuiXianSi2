using RootMotion.Dynamics;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
	public Animator animator;

	public PuppetMaster puppetMaster;

	public ConfigurableJoint[] leftLeg;

	public ConfigurableJoint[] rightLeg;

	private bool leftLegRemoved;

	private bool rightLegRemoved;

	private void Start()
	{
		puppetMaster.OnMuscleRemoved += OnMuscleDisconnected;
		puppetMaster.OnMuscleDisconnected += OnMuscleDisconnected;
	}

	public void OnRebuild()
	{
		animator.SetFloat("Legs", 2f);
		animator.Play("Move", 0, 0f);
		leftLegRemoved = false;
		rightLegRemoved = false;
	}

	private void OnMuscleDisconnected(Muscle m)
	{
		bool isLeft;
		if (!IsLegMuscle(m, out isLeft))
		{
			return;
		}
		if (isLeft)
		{
			leftLegRemoved = true;
		}
		else
		{
			bool wasLeftRemoved = leftLegRemoved;
			rightLegRemoved = true;
			if (!wasLeftRemoved)
			{
				if (animator != null)
				{
					animator.SetFloat("Legs", 1f);
				}
				return;
			}
		}
		if (rightLegRemoved)
		{
			if (puppetMaster != null)
			{
				puppetMaster.state = PuppetMaster.State.Dead;
				return;
			}
		}
		if (animator != null)
		{
			animator.SetFloat("Legs", 1f);
		}
	}

	private bool IsLegMuscle(Muscle m, out bool isLeft)
	{
		isLeft = false;
		for (int i = 0; i < leftLeg.Length; i++)
		{
			if (leftLeg[i] == m.joint)
			{
				isLeft = true;
				return true;
			}
		}
		for (int i = 0; i < rightLeg.Length; i++)
		{
			if (rightLeg[i] == m.joint)
			{
				return true;
			}
		}
		return false;
	}
}
