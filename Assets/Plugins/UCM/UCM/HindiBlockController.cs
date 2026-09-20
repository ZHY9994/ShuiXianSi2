using UnityEngine;
using UnityEngine.UI;

namespace UCM
{
	public class HindiBlockController : MonoBehaviour
	{
		[SerializeField]
		private GameObject shortCompanyNameBlock;

		[SerializeField]
		private GameObject longCompanyNameBlock;

		[SerializeField]
		private Text shortCompanyNameTextView;

		[SerializeField]
		private Text longCompanyNameTextView;

		public void SetCompanyName(string companyName)
		{
			bool longName = !string.IsNullOrEmpty(companyName) && companyName.Length > 20;
			if (shortCompanyNameBlock != null) shortCompanyNameBlock.SetActive(!longName);
			if (longCompanyNameBlock != null) longCompanyNameBlock.SetActive(longName);
			if (shortCompanyNameTextView != null) shortCompanyNameTextView.text = companyName;
			if (longCompanyNameTextView != null) longCompanyNameTextView.text = companyName;
		}
	}
}
