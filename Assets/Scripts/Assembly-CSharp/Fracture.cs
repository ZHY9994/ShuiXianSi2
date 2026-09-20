using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Fracture : MonoBehaviour
{
	[SerializeField]
	private GameObject m_Container;

	[SerializeField]
	private Rigidbody[] m_Bodies;

	[SerializeField]
	private List<Vector3> m_OriginalPositions = new List<Vector3>();

	[SerializeField]
	private List<Quaternion> m_OriginalRotations = new List<Quaternion>();

	public Transform Container => m_Container.transform;

	public void ResetBodies()
	{
		m_Container.SetActive(false);
		for (int i = 0; i < m_Bodies.Length; i++)
		{
			m_Bodies[i].velocity = Vector3.zero;
			m_Bodies[i].transform.localPosition = m_OriginalPositions[i];
			m_Bodies[i].transform.localRotation = m_OriginalRotations[i];
			m_Bodies[i].transform.localScale = Vector3.one;
		}
	}

	public void ScaleReductions()
	{
		foreach (Rigidbody body in m_Bodies)
			body.transform.DOScale(Vector3.zero, 2f);
	}

	public void SetContainerScale(Vector3 i_Scale)
	{
		m_Container.transform.localScale = i_Scale;
	}

	public void ObjectFracture(float i_Force, Vector3 i_HitPoint)
	{
		m_Container.SetActive(true);
		foreach (Rigidbody body in m_Bodies)
			body.AddExplosionForce(i_Force, i_HitPoint, 2f, 0f, ForceMode.Impulse);
		Invoke(nameof(ScaleReductions), 1f);
	}

	public void ObjectFractureRandom(float i_Force)
	{
		m_Container.SetActive(true);
		foreach (Rigidbody body in m_Bodies)
			body.AddForce(RandomVector() * i_Force, ForceMode.Impulse);
		Invoke(nameof(ScaleReductions), 1f);
	}

	private Vector3 RandomVector()
	{
		return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
	}

	private void SetRefs()
	{
		m_Container = transform.GetChild(0).gameObject;
		m_Bodies = GetComponentsInChildren<Rigidbody>();
		m_OriginalPositions.Clear();
		m_OriginalRotations.Clear();
		foreach (Rigidbody body in m_Bodies)
		{
			m_OriginalPositions.Add(body.transform.localPosition);
			m_OriginalRotations.Add(body.transform.localRotation);
		}
	}
}
