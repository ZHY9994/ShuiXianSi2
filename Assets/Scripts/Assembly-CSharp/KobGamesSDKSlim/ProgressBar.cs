using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	public class ProgressBar : MonoBehaviour
	{
		[SerializeField]
		private bool InvertProgress;

		[SerializeField]
		private Image ProgressBarImage;

		public float Value => ProgressBarImage.fillAmount;

		public virtual void Open()
		{
			gameObject.SetActive(true);
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
		}

		public virtual void SetProgress(float i_Progress)
		{
			ProgressBarImage.fillAmount = InvertProgress ? 1f - i_Progress : i_Progress;
		}
	}
}
