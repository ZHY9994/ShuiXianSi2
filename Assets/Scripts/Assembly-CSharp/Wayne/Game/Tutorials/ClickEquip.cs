using Coffee.UIExtensions;
using UnityEngine;

namespace Wayne.Game.Tutorials
{
	public class ClickEquip : TMaskedTutorialStep
	{
		[SerializeField]
		private Unmask m_ContainerUnmask;

		protected override void onActivate()
		{
			TutorialManager manager = TutorialManager.Instance;
			if (manager == null) return;
			manager.CardInfoRVUpgradeButton.gameObject.SetActive(false);
			manager.CardInfoCloseButton.gameObject.SetActive(false);
			m_ContainerUnmask.fitTarget = manager.CardInfoContainer;
			setMaskTarget(manager.ShopCardEquipButton);
		}

		protected override void onDeactivate()
		{
			TutorialManager manager = TutorialManager.Instance;
			if (manager == null) return;
			manager.CardInfoRVUpgradeButton.gameObject.SetActive(true);
			manager.CardInfoCloseButton.gameObject.SetActive(true);
		}
	}
}
