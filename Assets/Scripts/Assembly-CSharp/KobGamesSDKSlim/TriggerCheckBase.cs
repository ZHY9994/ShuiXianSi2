using System;
using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class TriggerCheckBase : MonoBehaviour
	{
		public eTags[] TagsToCheck;

		public bool IsActive;

		public Action OnIsActive;

		public Action OnIsNotActive;

		public List<GameObject> ObjectsInsideTrigger;

		public void Reset()
		{
			IsActive = false;
			ObjectsInsideTrigger.Clear();
		}

		public void OnTriggerEnter(Collider i_Collider)
		{
			if (i_Collider == null || !CheckTag(i_Collider.gameObject.tag) || IsActive) return;
			IsActive = true;
			if (!ObjectsInsideTrigger.Contains(i_Collider.gameObject)) ObjectsInsideTrigger.Add(i_Collider.gameObject);
			OnIsActive?.Invoke();
		}

		public void OnTriggerStay(Collider i_Collider)
		{
			if (i_Collider == null || !CheckTag(i_Collider.gameObject.tag) || IsActive) return;
			IsActive = true;
			if (!ObjectsInsideTrigger.Contains(i_Collider.gameObject)) ObjectsInsideTrigger.Add(i_Collider.gameObject);
			OnIsActive?.Invoke();
		}

		public void OnTriggerExit(Collider i_Collider)
		{
			if (i_Collider == null || !CheckTag(i_Collider.gameObject.tag) || !IsActive) return;
			IsActive = false;
			if (ObjectsInsideTrigger.Contains(i_Collider.gameObject)) ObjectsInsideTrigger.Remove(i_Collider.gameObject);
			OnIsNotActive?.Invoke();
		}

		private bool CheckTag(string i_Tag)
		{
			for (int i = 0; i < TagsToCheck.Length; i++)
				if (TagsToCheck[i].ToString() == i_Tag) return true;
			return false;
		}

		public TriggerCheckBase()
		{
			ObjectsInsideTrigger = new List<GameObject>();
		}
	}
}
