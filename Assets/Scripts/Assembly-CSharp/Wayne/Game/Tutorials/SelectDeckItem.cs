using Coffee.UIExtensions;
using System.Linq;
using UnityEngine;

namespace Wayne.Game.Tutorials
{
	public class SelectDeckItem : TMaskedTutorialStep
	{
		[SerializeField]
		private Unmask m_BodyContainer;

		[SerializeField]
		private Unmask m_TopContainer;

		protected override void onActivate()
		{
			DeckItem deckItem = TutorialManager.Instance?.DeckItems?.FirstOrDefault(x => x.ItemData != null && x.ItemData.WeaponType == (eWeaponType)9);
			if (deckItem != null)
				setMaskTarget(deckItem.TutorialTarget);

			if (m_BodyContainer != null)
				m_BodyContainer.fitTarget = TutorialManager.Instance.DeckBodyRect;
		}

		public SelectDeckItem()
		{
		}
	}
}
