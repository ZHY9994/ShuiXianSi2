using UnityEngine;

namespace KobGamesSDKSlim
{
	public class InteractionManager : MonoBehaviour
	{
		public RateUsPopUp RateUsPopup;

		public RateUsPopupSchema SelectedSchema;

		public void ShowRateUsPopup()
		{
			RateUsPopup.ShowRateUs();
		}

		public void CloseRateUsPopup()
		{
			RateUsPopup.CloseRateUs();
		}

		private RateUsPopupSchema[] getSchemas()
		{
			return RateUsPopup.Schemas;
		}

		private void onSelectedSchema()
		{
			RateUsPopup.SelectedSchema = SelectedSchema;
		}
	}
}
