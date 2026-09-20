using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
internal sealed class ExecutionOrderAttribute : Attribute
{
	public readonly int ExecutionOrder;

	public ExecutionOrderAttribute(int executionOrder)
	{
		ExecutionOrder = executionOrder;
	}
}
