using UnityEngine;
using UnityEngine.UI;

namespace UCM
{
	public class hindiGlyph : MonoBehaviour
	{
		public Text hindiText;

		private void Start()
		{
			if (hindiText != null)
			{
				hindiText.text = UnicodeToKruti.Convert(hindiText.text);
			}
		}
	}
}
