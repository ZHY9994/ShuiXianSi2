using UnityEngine;

namespace Wayne.Game.Shops
{
	public abstract class TShopRarityItem : TRarityItem
	{
		[SerializeField]
		private GameObject m_InUse;

		protected abstract bool m_IsInUse { get; }

		protected override void updateVisual()
		{
			if (m_InUse != null) m_InUse.SetActive(m_IsInUse);
			base.updateVisual();
		}

		protected override void SetRefs()
		{
			Transform inUse = transform.FindDeepChild<Transform>("InUse");
			m_InUse = inUse == null ? null : inUse.gameObject;
			base.SetRefs();
		}
	}
}
