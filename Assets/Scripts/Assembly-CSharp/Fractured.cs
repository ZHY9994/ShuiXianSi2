using UnityEngine;
using KobGamesSDKSlim;
using MrWayne;

public class Fractured : MonoBehaviour
{
	[SerializeField]
	private FracturedObject[] m_Objects;

	private void OnEnable()
	{
		ResetFracture();
	}

	public void ResetFracture()
	{
		foreach (FracturedObject fracturedObject in m_Objects)
			fracturedObject.ResetObject();
	}

	public void ExplodeWithDirection(Vector3 i_Direction, Vector2 i_Force, ForceMode i_Mode = ForceMode.Impulse)
	{
		foreach (FracturedObject fracturedObject in m_Objects)
		{
			fracturedObject.ActivateObject();
			Vector3 force = i_Direction + randomVector() * Random.Range(i_Force.x, i_Force.y);
			fracturedObject.AddForce(force, i_Mode);
		}
		Invoke(nameof(HideObjects), 5f);
	}

	public void EnableShardDamage(UnitBase i_Owner)
	{
		foreach (FracturedObject fracturedObject in m_Objects)
			fracturedObject.EnableShardDamage(i_Owner);
	}

	private Vector3 randomVector()
	{
		return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
	}

	public void HideObjects()
	{
		foreach (FracturedObject fracturedObject in m_Objects)
			fracturedObject.Hide(GameConfig.Instance.Menus.PopUpTextParameters.HideDuration, GameConfig.Instance.Menus.PopUpTextParameters.HideEase);
	}

	private void SetRefs()
	{
		m_Objects = GetComponentsInChildren<FracturedObject>(true);
		foreach (FracturedObject fracturedObject in m_Objects)
			fracturedObject.SetRefs();
	}
}
