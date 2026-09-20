using System;

namespace Wayne.Game.Tutorials
{
	public class SelectWeaponCard : TMaskedTutorialStep
	{
		private TutorialManager m_TutorialManager => TutorialManager.Instance;

		protected override void onActivate()
		{
			TutorialManager manager = m_TutorialManager;
			if (manager == null || manager.ShopItems == null) return;
			manager.WeaponShopScrollRect.enabled = false;
			int count = Math.Min(7, manager.ShopItems.Length);
			for (int i = 0; i < count; i++)
				if (manager.ShopItems[i] != null) manager.ShopItems[i].gameObject.SetActive(false);
			if (manager.ShopItems.Length > 8 && manager.ShopItems[8] != null)
			{
				setMaskTarget(manager.ShopItems[8].Body);
			}
		}

		protected override void onDeactivate()
		{
			TutorialManager manager = m_TutorialManager;
			if (manager == null || manager.ShopItems == null) return;
			manager.WeaponShopScrollRect.enabled = true;
			int count = Math.Min(7, manager.ShopItems.Length);
			for (int i = 0; i < count; i++)
				if (manager.ShopItems[i] != null) manager.ShopItems[i].gameObject.SetActive(true);
		}
	}
}
