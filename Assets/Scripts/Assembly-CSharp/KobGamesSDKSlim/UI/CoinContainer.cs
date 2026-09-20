using TMPro;
using UnityEngine;

namespace KobGamesSDKSlim.UI
{
	public class CoinContainer : MonoBehaviour
	{
		[SerializeField]
		protected TextMeshProUGUI m_CoinText;

		[SerializeField]
		protected RectTransform m_CoinIcon;

		[SerializeField]
		protected RectTransform m_CoinMoveTarget;

		[SerializeField]
		protected Scaler m_TextScaler;

		[SerializeField]
		protected Scaler m_IconScaler;

		[SerializeField]
		private bool m_IsUseTextFormat;

		private string m_TextFormat;

		public RectTransform CoinMoveTarget => m_CoinMoveTarget;

		protected virtual void SetRefs()
		{
			m_CoinText = transform.FindDeepChild<TextMeshProUGUI>("CoinText");
			m_CoinIcon = transform.FindDeepChild<RectTransform>("CoinIcon");
			m_CoinMoveTarget = transform.FindDeepChild<RectTransform>("CoinMoveTarget");
			m_TextScaler = m_CoinText.GetComponent<Scaler>();
			m_IconScaler = m_CoinIcon.GetComponent<Scaler>();
		}

		protected virtual void OnEnable()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			return;
		}

		private void updateCoinsText(int i_CoinsAmount)
		{
			return;
		}

		public virtual void SetCoins(int i_Value, bool i_PopAnim)
		{
			if (m_IsUseTextFormat)
			{
				if (m_TextFormat == null) m_TextFormat = m_CoinText.text;
				m_CoinText.SetText(string.Format(m_TextFormat, i_Value));
			}
			else m_CoinText.SetText(i_Value.ToString());
			if (i_PopAnim) PopAnim();
		}

		public void PopAnim()
		{
			m_IconScaler.StopAnimation();
			m_IconScaler.StartAnimation();
		}
	}
}
