using UnityEngine;

namespace KobGamesSDKSlim
{
	[ExecutionOrder(-1000)]
	public class CultureInfoManager : MonoBehaviour
	{
		public CultureInfoManager() { }

		private void Awake()
		{
			if (!GameSettings.Instance.General.SetSpecificCulture)
				return;
			var culture = System.Globalization.CultureInfo.CreateSpecificCulture(GameSettings.Instance.General.CultureName);
			System.Threading.Thread.CurrentThread.CurrentCulture = culture;
			System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
		}
	}
}
