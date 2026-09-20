using System.Linq;
using UnityEngine;

public static class TargetingExtensions
{
	private const float FLOAT_MaxSearchRange = 1E+12f;

	public static GameObject FindNearestUnit(this GameObject go, LayerMask maskToSearch)
	{
		Collider[] colliders = Physics.OverlapSphere(go.transform.position, FLOAT_MaxSearchRange, maskToSearch);
		return go.FindNearestUnit_List(colliders.Select(x => x.gameObject).ToArray());
	}

	public static GameObject FindNearestUnit_View(this GameObject go, float frustumAngle, LayerMask maskToSearch)
	{
		Collider[] colliders = Physics.OverlapSphere(go.transform.position, FLOAT_MaxSearchRange, maskToSearch);
		return go.FindNearestUnit_ListView(frustumAngle, colliders.Select(x => x.gameObject).ToArray());
	}

	public static GameObject FindNearestUnit_Range(this GameObject go, float targetRange, LayerMask maskToSearch)
	{
		Collider[] colliders = Physics.OverlapSphere(go.transform.position, targetRange, maskToSearch);
		return go.FindNearestUnit_ListRange(targetRange, colliders.Select(x => x.gameObject).ToArray());
	}

	public static GameObject FindNearestUnit_RangeView(this GameObject go, float frustumAngle, float targetRange, LayerMask maskToSearch)
	{
		Collider[] colliders = Physics.OverlapSphere(go.transform.position, targetRange, maskToSearch);
		return go.FindNearestUnit_ListRangeView(targetRange, frustumAngle, colliders.Select(x => x.gameObject).ToArray());
	}

	public static GameObject FindNearestUnit_ListRange(this GameObject go, float targetRange, GameObject[] goList)
	{
		Vector3 position = go.transform.position;
		float nearestDistance = FLOAT_MaxSearchRange;
		GameObject nearest = null;
		foreach (GameObject candidate in goList)
		{
			float distance = Vector3.Distance(position, candidate.transform.position);
			if (distance <= targetRange && distance < nearestDistance)
			{
				nearestDistance = distance;
				nearest = candidate;
			}
		}
		return nearest;
	}

	public static GameObject FindNearestUnit_List(this GameObject go, GameObject[] goList)
	{
		Vector3 position = go.transform.position;
		float nearestDistance = FLOAT_MaxSearchRange;
		GameObject nearest = null;
		foreach (GameObject candidate in goList)
		{
			float distance = Vector3.Distance(position, candidate.transform.position);
			if (distance < nearestDistance)
			{
				nearestDistance = distance;
				nearest = candidate;
			}
		}
		return nearest;
	}

	public static GameObject FindNearestUnit_ListView(this GameObject go, float frustumAngle, GameObject[] goList)
	{
		Vector3 position = go.transform.position;
		Vector3 forward = go.transform.forward;
		float nearestDistance = FLOAT_MaxSearchRange;
		GameObject nearest = null;
		foreach (GameObject candidate in goList)
		{
			Vector3 delta = candidate.transform.position - position;
			float distance = delta.magnitude;
			float angle = (distance == 0f || forward.magnitude == 0f)
				? 0f
				: Mathf.Acos(Mathf.Clamp(Vector3.Dot(forward, delta) / (forward.magnitude * distance), -1f, 1f)) * 57.296f;
			if (angle <= frustumAngle && distance < nearestDistance)
			{
				nearestDistance = distance;
				nearest = candidate;
			}
		}
		return nearest;
	}

	public static GameObject FindNearestUnit_ListRangeView(this GameObject go, float targetRange, float frustumAngle, GameObject[] goList)
	{
		Vector3 position = go.transform.position;
		Vector3 forward = go.transform.forward;
		float nearestDistance = FLOAT_MaxSearchRange;
		GameObject nearest = null;
		foreach (GameObject candidate in goList)
		{
			Vector3 delta = candidate.transform.position - position;
			float distance = delta.magnitude;
			if (distance > targetRange)
			{
				continue;
			}
			float angle = (distance == 0f || forward.magnitude == 0f)
				? 0f
				: Mathf.Acos(Mathf.Clamp(Vector3.Dot(forward, delta) / (forward.magnitude * distance), -1f, 1f)) * 57.296f;
			if (angle <= frustumAngle && distance < nearestDistance)
			{
				nearestDistance = distance;
				nearest = candidate;
			}
		}
		return nearest;
	}
}
