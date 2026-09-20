using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

namespace KobGamesSDKSlim.UI
{
	public class ToggleButton2 : MonoBehaviour
	{
		public delegate void ToggleCallback(bool i_State);

		[SerializeField]
		private ExtendedButton m_Button;

		[SerializeField]
		private TextMeshProUGUI m_Title;

		[SerializeField]
		private ImageToggle m_ToggleImage;

		private bool m_isOn;

		public event ToggleCallback OnToggle = delegate { };

		protected void SetRefs()
		{
			m_Button = GetComponentInChildren<ExtendedButton>();
			m_Title = transform.FindDeepChild<TMPro.TextMeshProUGUI>("Title");
			m_ToggleImage = GetComponentInChildren<ImageToggle>();
		}

		protected virtual void OnEnable()
		{
			m_Button.Setup(OnButtonDown);
			SetState(m_isOn);
		}

		public virtual void Set(string i_Title, bool i_State, ToggleCallback i_Callback)
		{
			m_Title.text = i_Title;
			SetState(i_State);
			OnToggle = i_Callback;
		}

		protected virtual void SetState(bool i_SetTrue)
		{
			m_ToggleImage.Set(i_SetTrue);
			m_isOn = i_SetTrue;
			OnToggle?.Invoke(i_SetTrue);
		}

		protected virtual void OnButtonDown()
		{
			SetState(!m_isOn);
		}
	}
}
