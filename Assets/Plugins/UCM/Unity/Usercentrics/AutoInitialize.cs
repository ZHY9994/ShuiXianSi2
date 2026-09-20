using UnityEngine;

namespace Unity.Usercentrics
{
	public class AutoInitialize : MonoBehaviour
	{
		[SerializeField]
		public bool Enabled;

		private void Awake()
		{
			if (!Enabled)
			{
				return;
			}

			Usercentrics.Instance.Initialize(
				status =>
				{
					if (status != null && status.shouldCollectConsent)
					{
						ShowFirstLayer();
					}
				},
				errorMessage => Debug.LogError("[USERCENTRICS] Error on AutoInitialize " + errorMessage));
		}

		private void ShowFirstLayer()
		{
			Usercentrics.Instance.ShowFirstLayer(response =>
			{
				Debug.Log("[USERCENTRICS][DEBUG] First layer dismissed");
			});
		}
	}
}
