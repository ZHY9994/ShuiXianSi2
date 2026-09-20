using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KobGamesSDKSlim
{
	public class ExtendedButtonModuleBehaviour : MonoBehaviour
	{
		private eButtonState m_CurrState;

		[SerializeField]
		private eExtendedButtonModuleType m_Type;

		[SerializeField]
		private bool m_CopyColorStatesFromParent;

		[SerializeField]
		private ColorBlock m_ColorStates;

		[SerializeField]
		private SpriteState m_SpriteState;

		[SerializeField]
		private bool m_CopyImageMaterialFromParent;

		[Header("Refs")]
		[SerializeField]
		private Image m_Image;

		[SerializeField]
		private Color m_ImageOriginalColor;

		[SerializeField]
		private Sprite m_ImageOriginalSprite;

		[SerializeField]
		private Text m_Text;

		[SerializeField]
		private Color m_TextOriginalColor;

		[SerializeField]
		private TextMeshProUGUI m_TextsTMP;

		[SerializeField]
		private Color m_TextsTMPOriginalColor;

		[SerializeField]
		private Color m_TextsTMPOutlineOriginalColor;

		private bool m_ShowColorStates => !m_CopyColorStatesFromParent && m_Type != eExtendedButtonModuleType.SpriteSwap;

		private bool setOriginalColorData()
		{
			bool changed = false;
			if (m_Image != null && m_Image.color != m_ImageOriginalColor) { m_ImageOriginalColor = m_Image.color; changed = true; }
			if (m_Text != null && m_Text.color != m_TextOriginalColor) { m_TextOriginalColor = m_Text.color; changed = true; }
			if (m_TextsTMP != null)
			{
				if (m_TextsTMP.color != m_TextsTMPOriginalColor) { m_TextsTMPOriginalColor = m_TextsTMP.color; changed = true; }
				Material material = m_TextsTMP.fontMaterial;
				if (material != null)
				{
					Color outline = material.GetColor(TMPro.ShaderUtilities.ID_OutlineColor);
					if (outline != m_TextsTMPOutlineOriginalColor) { m_TextsTMPOutlineOriginalColor = outline; changed = true; }
				}
			}
			return changed;
		}

		public bool SetData(Material i_Material, ColorBlock i_ColorBlock)
		{
			bool changed = false;
			if (m_Image != null && m_CopyImageMaterialFromParent && m_Image.material != i_Material) { m_Image.material = i_Material; changed = true; }
			if (m_CopyColorStatesFromParent && m_ColorStates != i_ColorBlock) { m_ColorStates = i_ColorBlock; changed = true; }
			return changed;
		}

		public void SetState(eButtonState i_ButtonState)
		{
			if (m_CurrState == eButtonState.Normal)
				setOriginalColorData();
			switch (m_Type)
			{
				case eExtendedButtonModuleType.ColorMultiply: setColorMultiplyState(i_ButtonState); break;
				case eExtendedButtonModuleType.ColorSet: setColorState(i_ButtonState); break;
				case eExtendedButtonModuleType.SpriteSwap: setSpriteState(i_ButtonState); break;
			}
			m_CurrState = i_ButtonState;
		}

		private void setColorMultiplyState(eButtonState i_ButtonState)
		{
			Color c = i_ButtonState == eButtonState.Disabled ? m_ColorStates.disabledColor : Color.white;
			setItemsColor(c);
		}

		private void setColorState(eButtonState i_ButtonState)
		{
			if (i_ButtonState != eButtonState.Normal && i_ButtonState != eButtonState.Disabled) return;
			setItemsColor(i_ButtonState == eButtonState.Disabled ? m_ColorStates.disabledColor : m_ColorStates.normalColor);
		}

		private void setSpriteState(eButtonState i_ButtonState)
		{
			if (m_Image == null) return;
			if (i_ButtonState == eButtonState.Disabled) m_Image.sprite = m_SpriteState.disabledSprite;
			else if (i_ButtonState == eButtonState.Normal) m_Image.sprite = m_ImageOriginalSprite;
		}

		private void setItemsColor(Color i_Color)
		{
			if (m_Image != null) m_Image.color = i_Color;
			if (m_Text != null) m_Text.color = i_Color;
			if (m_TextsTMP != null)
			{
				m_TextsTMP.color = i_Color;
				Material material = m_TextsTMP.fontMaterial;
				if (material != null) material.SetColor(TMPro.ShaderUtilities.ID_OutlineColor, i_Color);
			}
		}

		public ExtendedButtonModuleBehaviour()
		{
			m_CopyColorStatesFromParent = true;
			m_CopyImageMaterialFromParent = true;
		}
	}
}
