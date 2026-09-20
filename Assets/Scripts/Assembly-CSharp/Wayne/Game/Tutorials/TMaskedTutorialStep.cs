using Coffee.UIExtensions;
using UnityEngine;

namespace Wayne.Game.Tutorials
{
	public abstract class TMaskedTutorialStep : TTutorialStep
	{
		[SerializeField]
		private Unmask m_Unmask;

		protected void setMaskTarget(RectTransform i_Target)
		{
			if (m_Unmask != null)
				m_Unmask.fitTarget = i_Target;
		}
	}
}
