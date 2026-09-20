using TMPro;
using UnityEngine;

namespace KobGamesSDKSlim.Collectable
{
	public class CollectableContainer : MonoBehaviour
	{
		[SerializeField]
		protected TextMeshProUGUI m_CollectableText;

		[SerializeField]
		protected RectTransform m_CollectableIcon;

		[SerializeField]
		protected RectTransform m_MoveTarget;

		[SerializeField]
		protected Scaler m_TextScaler;

		[SerializeField]
		protected Scaler m_IconScaler;

		[SerializeField]
		private bool m_IsUseTextFormat;

		[SerializeField]
		private bool m_IsHideBigNumbers = true;

		private string m_TextFormat;

		public RectTransform MoveTarget => m_MoveTarget;

		protected virtual void SetRefs()
		{
			Transform root = transform;
			m_CollectableText = root.FindDeepChild<TextMeshProUGUI>("Collectable Text") ?? root.FindDeepChild<TextMeshProUGUI>("CoinText");
			m_CollectableIcon = root.FindDeepChild<RectTransform>("Collectable Icon") ?? root.FindDeepChild<RectTransform>("CoinIcon");
			m_MoveTarget = root.FindDeepChild<RectTransform>("Collectable Move Target") ?? root.FindDeepChild<RectTransform>("CoinMoveTarget");
			if (m_CollectableText != null) m_TextScaler = m_CollectableText.GetComponent<Scaler>();
			if (m_CollectableIcon != null) m_IconScaler = m_CollectableIcon.GetComponent<Scaler>();
		}

		protected virtual void OnEnable()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			return;
		}

		protected virtual void SetCollectableValue(int i_Value, bool i_PopAnim)
		{
			string text = m_IsUseTextFormat ? (m_TextFormat ??= m_CollectableText.text) : i_Value.ToString();
			if (m_IsUseTextFormat)
				text = string.Format(text, m_IsHideBigNumbers ? hideBigNumber(i_Value) : i_Value);
			m_CollectableText.SetText(text);
			if (i_PopAnim) popAnim();
		}

		private string hideBigNumber(int i_Num)
		{
			if (i_Num < 10000) return i_Num.ToString();
			if (i_Num >= 100000000) return (i_Num / 1000000d).ToString() + "M";
			if (i_Num >= 1000000) return (i_Num / 1000000d).ToString() + "M";
			return (i_Num / 1000d).ToString() + "K";
		}

		private void popAnim()
		{
			m_IconScaler.StopAnimation();
			m_IconScaler.StartAnimation();
		}
	}
}
